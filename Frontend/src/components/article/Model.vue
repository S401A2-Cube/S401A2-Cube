<script setup>
import { ref, shallowRef, watch, onBeforeUnmount } from 'vue';
import * as THREE from 'three/webgpu';
import { OrbitControls } from 'three/addons/controls/OrbitControls.js';

import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { DRACOLoader } from 'three/addons/loaders/DRACOLoader.js';
import { KTX2Loader } from 'three/addons/loaders/KTX2Loader.js';

const props = defineProps({
  modelUrl: {
    type: String,
    required: true
  },
  selectedColor: {
    type: Object,
    default: null
  }
});

const isLoading = ref(true);
const loadProgress = ref(0);
const showHint = ref(false);

const getMaterialTextureBindings = (material) => {
  const textureEntries = [];

  for (const [key, value] of Object.entries(material)) {
    if (value && value.isTexture) {
      textureEntries.push({ slot: key, texture: value });
    }
  }

  return textureEntries;
};

const containerRef = ref(null);
const renderer = shallowRef(null); 
let scene, camera, controls, resizeObserver;
let frameMaterial = null;
let frameGrayTexture = null;
let barsSeatPedalsMaterial = null;
let barsSeatPedalsGrayTexture = null;

const NAMED_COLOR_MAP = {
  noir: "#050505",
  blanc: "#FFFFFF",
  bleu: "#0051FF",
  rouge: "#FF2A2A",
  vert: "#00FF41",
  gris: "#A9B4C2",
  violet: "#B026FF",
  rose: "#FF1493",
  jaune: "#FFF100",
  orange: "#FF5E00",
  marron: "#C85A17",
  or: "#FFC400",
  turquoise: "#15E6CD"
};

const getTintHexFromSelectedColor = (selectedColor) => {
  if (!selectedColor) return '#ffffff';

  const candidateFields = [
    selectedColor.hex,
    selectedColor.hexa,
    selectedColor.codeHex,
    selectedColor.couleurHex,
    selectedColor.colorHex,
    selectedColor.hexColor,
    selectedColor.rgb,
    selectedColor.rgba
  ];

  const directColorValue = candidateFields.find((value) => typeof value === 'string' && value.trim().length > 0);
  if (directColorValue) {
    return directColorValue.trim();
  }

  const normalizedName = (selectedColor.nomCouleur || selectedColor.name || '')
    .toString()
    .trim()
    .toLowerCase();

  return NAMED_COLOR_MAP[normalizedName] || '#ffffff';
};

const buildGrayscaleTextureFromSource = (sourceTexture) => {
  const sourceImage = sourceTexture?.source?.data;
  if (!sourceImage || typeof document === 'undefined') return null;

  const width = sourceImage.width;
  const height = sourceImage.height;
  if (!width || !height) return null;

  const canvas = document.createElement('canvas');
  canvas.width = width;
  canvas.height = height;
  const ctx = canvas.getContext('2d', { willReadFrequently: true });
  if (!ctx) return null;

  ctx.drawImage(sourceImage, 0, 0, width, height);
  const imageData = ctx.getImageData(0, 0, width, height);
  const data = imageData.data;

  for (let i = 0; i < data.length; i += 4) {
    const r = data[i];
    const g = data[i + 1];
    const b = data[i + 2];
    const luminance = Math.round((0.2126 * r) + (0.7152 * g) + (0.0722 * b));
    data[i] = luminance;
    data[i + 1] = luminance;
    data[i + 2] = luminance;
  }

  ctx.putImageData(imageData, 0, 0);

  const grayscaleTexture = new THREE.CanvasTexture(canvas);
  grayscaleTexture.colorSpace = sourceTexture.colorSpace;
  grayscaleTexture.flipY = sourceTexture.flipY;
  grayscaleTexture.wrapS = sourceTexture.wrapS;
  grayscaleTexture.wrapT = sourceTexture.wrapT;
  grayscaleTexture.magFilter = sourceTexture.magFilter;
  grayscaleTexture.minFilter = sourceTexture.minFilter;
  grayscaleTexture.generateMipmaps = sourceTexture.generateMipmaps;
  grayscaleTexture.needsUpdate = true;

  return grayscaleTexture;
};

const updateFrameTint = (selectedColor) => {
  if (!frameMaterial) return;

  frameMaterial.color.set(getTintHexFromSelectedColor(selectedColor));
  frameMaterial.needsUpdate = true;
};

const updateBarsSeatPedalsTint = (selectedColor) => {
  if (!barsSeatPedalsMaterial) return;

  barsSeatPedalsMaterial.color.set(getTintHexFromSelectedColor(selectedColor));
  barsSeatPedalsMaterial.needsUpdate = true;
};

const configureFrameMaterial = (material) => {
  if (!material || !material.map || (material.map.name !== 'Frame_Diffuse')) return;

  frameMaterial = material;

  if (!frameGrayTexture) {
    frameGrayTexture = buildGrayscaleTextureFromSource(material.map);
  }

  if (frameGrayTexture) {
    frameMaterial.map = frameGrayTexture;
  }

  updateFrameTint(props.selectedColor);
};

const configureBarsSeatPedalsMaterial = (material) => {
  if (!material || !material.map || (material.map.name !== 'BarsSeatPedals_Diffuse')) return;

  barsSeatPedalsMaterial = material;

  if (!barsSeatPedalsGrayTexture) {
    barsSeatPedalsGrayTexture = buildGrayscaleTextureFromSource(material.map);
  }

  if (barsSeatPedalsGrayTexture) {
    barsSeatPedalsMaterial.map = barsSeatPedalsGrayTexture;
  }

  updateBarsSeatPedalsTint(props.selectedColor);
};

const initThreeJS = async (container) => {
  const curRenderer = new THREE.WebGPURenderer({ 
    antialias: true,
    alpha: true 
  });

  curRenderer.setPixelRatio(window.devicePixelRatio);
  
  curRenderer.toneMapping = THREE.ACESFilmicToneMapping;
  curRenderer.toneMappingExposure = 1.0;
  
  const initialWidth = container.clientWidth || window.innerWidth;
  const initialHeight = container.clientHeight || 500;
  curRenderer.setSize(initialWidth, initialHeight);
  
  container.appendChild(curRenderer.domElement);
  renderer.value = curRenderer;

  await curRenderer.init();

  scene = new THREE.Scene();

  camera = new THREE.PerspectiveCamera(45, initialWidth / initialHeight, 0.1, 100);

  controls = new OrbitControls(camera, curRenderer.domElement);
  controls.enableDamping = true;

  const hemiLight = new THREE.HemisphereLight(0xffffff, 0x444444, 0.6);
  hemiLight.position.set(0, 20, 0);
  scene.add(hemiLight);

  const keyLight = new THREE.DirectionalLight(0xffffff, 2.5);
  keyLight.position.set(5, 5, 5);
  scene.add(keyLight);

  const fillLight = new THREE.DirectionalLight(0xddeeff, 1.2);
  fillLight.position.set(-5, 3, 5);
  scene.add(fillLight);

  const rimLight = new THREE.DirectionalLight(0xffffff, 3.0);
  rimLight.position.set(0, 5, -5);
  scene.add(rimLight);

  const leftSideLight = new THREE.DirectionalLight(0xffffff, 0.5);
  leftSideLight.position.set(-10, 0, 0); 
  scene.add(leftSideLight);

  const rightSideLight = new THREE.DirectionalLight(0xffffff, 0.5);
  rightSideLight.position.set(10, 0, 0); 
  scene.add(rightSideLight);

  const ktx2Loader = new KTX2Loader().setTranscoderPath('/jsm/libs/basis/').detectSupport(curRenderer);
  const dracoLoader = new DRACOLoader().setDecoderPath('/jsm/libs/draco/gltf/');

  const loader = new GLTFLoader();
  loader.setKTX2Loader(ktx2Loader);
  loader.setDRACOLoader(dracoLoader);

  loader.load(
    props.modelUrl,
    (gltf) => {
      scene.add(gltf.scene);

      console.groupCollapsed('[Model] Mesh and texture bindings');
      
      gltf.scene.traverse((child) => {
        if (child.isMesh && child.material) {
          child.material.depthWrite = true;
          child.material.side = THREE.DoubleSide; 

          const materials = Array.isArray(child.material) ? child.material : [child.material];
          const meshName = child.name || '(unnamed mesh)';

          console.group(`Mesh: ${meshName}`);

          materials.forEach((material, index) => {
            const materialName = material.name || `(material ${index})`;
            const textureBindings = getMaterialTextureBindings(material);

            configureFrameMaterial(material);
            configureBarsSeatPedalsMaterial(material);

            console.group(`Material: ${materialName}`);

            if (textureBindings.length === 0) {
              console.log('No textures bound');
            } else {
              textureBindings.forEach(({ slot, texture }) => {
                const source = texture?.source?.data?.currentSrc || texture?.source?.data?.src || '(embedded or generated)';
                console.log(`${slot}:`, source, texture);
              });
            }

            console.groupEnd();
          });

          console.groupEnd();
        }
      });
      console.groupEnd();
      
      const box = new THREE.Box3().setFromObject(gltf.scene);
      const center = box.getCenter(new THREE.Vector3());
      const size = box.getSize(new THREE.Vector3());

      gltf.scene.position.sub(center);
      
      const maxDim = Math.max(size.x, size.y, size.z);
      const fov = camera.fov * (Math.PI / 180);
      let cameraZ = Math.abs(maxDim / 2 / Math.tan(fov / 2));
      
      cameraZ *= 0.75; 
      
      camera.position.set(cameraZ, size.y / 3.5, 0);
      camera.lookAt(0, 0, 0);
      controls.target.set(0, 0, 0);
      controls.update();

      isLoading.value = false;
      showHint.value = true;
      
      setTimeout(() => {
        showHint.value = false;
      }, 4000);
    },
    (xhr) => {
      if (xhr.total > 0) {
        loadProgress.value = Math.round((xhr.loaded / xhr.total) * 100);
      }
    },
    (error) => console.error('Failed to load GLTF model:', error)
  );

  const updateSize = () => {
    if (!container || container.clientWidth === 0) return;
    const width = container.clientWidth;
    const height = container.clientHeight || 500;
    
    curRenderer.setSize(width, height);
    camera.aspect = width / height;
    camera.updateProjectionMatrix();
  };

  resizeObserver = new ResizeObserver(updateSize);
  resizeObserver.observe(container);

  curRenderer.setAnimationLoop(() => {
    controls.update(); 
    curRenderer.render(scene, camera);
  });
};

watch(containerRef, (newContainer) => {
  if (newContainer && !renderer.value) {
    initThreeJS(newContainer);
  }
});

watch(
  () => props.selectedColor,
  (newColor) => {
    updateFrameTint(newColor);
    updateBarsSeatPedalsTint(newColor);
  },
  { deep: true }
);

onBeforeUnmount(() => {
  if (resizeObserver && containerRef.value) {
    resizeObserver.unobserve(containerRef.value);
    resizeObserver.disconnect();
  }
  
  if (renderer.value) {
    renderer.value.dispose();
    renderer.value.setAnimationLoop(null);
    if (containerRef.value && renderer.value.domElement) {
      containerRef.value.removeChild(renderer.value.domElement);
    }
  }

  if (frameGrayTexture) {
    frameGrayTexture.dispose();
    frameGrayTexture = null;
  }

  if (barsSeatPedalsGrayTexture) {
    barsSeatPedalsGrayTexture.dispose();
    barsSeatPedalsGrayTexture = null;
  }

  frameMaterial = null;
  barsSeatPedalsMaterial = null;
});
</script>

<template>
  <div class="model-wrapper">
    <div v-if="isLoading" class="ui-overlay loading-screen">
      <div class="spinner"></div>
      <p class="loading-text">Chargement... {{ loadProgress }}%</p>
    </div>

    <div v-if="showHint" class="ui-overlay hint-screen">
      <div class="hint-pill">
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-mouse"><rect x="5" y="2" width="14" height="20" rx="7"/><path d="M12 6v4"/></svg>
        <p>Cliquez et glissez pour tourner, scrollez pour zoomer</p>
      </div>
    </div>

    <div class="canvas-container" ref="containerRef"></div>
  </div>
</template>

<style scoped>
.model-wrapper {
  position: relative;
  width: 70vw;
  height: 80vh;
  margin: 0 auto;
}

.canvas-container {
  width: 100%;
  height: 100%;
  background: transparent; 
}

.canvas-container :deep(canvas) {
  width: 100% !important;
  height: 100% !important;
  display: block;
  outline: none;
  cursor: grab;
}

.canvas-container :deep(canvas:active) {
  cursor: grabbing;
}

.ui-overlay {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  pointer-events: none;
  z-index: 10;
}

.loading-screen {
  background-color: rgba(255, 255, 255, 0.9);
  pointer-events: all;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #f3f3f3;
  border-top: 4px solid #000000;
  border-radius: 50%;
  animation: spin 1s linear infinite;
  margin-bottom: 1rem;
}

.loading-text {
  font-family: monospace;
  font-size: 14px;
  color: #333;
  text-transform: uppercase;
  letter-spacing: 1px;
}

.hint-screen {
  justify-content: flex-end;
  padding-bottom: 2rem;
  animation: fadeInOut 4s forwards;
}

.hint-pill {
  background-color: rgba(0, 0, 0, 0.8);
  color: white;
  padding: 10px 20px;
  border-radius: 30px;
  display: flex;
  align-items: center;
  gap: 10px;
  font-size: 14px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.hint-pill p {
  margin: 0;
  text-transform: none;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

@keyframes fadeInOut {
  0% { opacity: 0; transform: translateY(10px); }
  10% { opacity: 1; transform: translateY(0); }
  80% { opacity: 1; transform: translateY(0); }
  100% { opacity: 0; transform: translateY(-10px); }
}
</style>