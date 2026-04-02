<script setup>
import { computed, onBeforeUnmount, ref } from 'vue';
import axios from 'axios';
import Input from '@/components/Input.vue';
import SelectInput from '@/components/SelectInput.vue';
import TextAreaInput from '@/components/TextAreaInput.vue';
import FileInput from '@/components/FileInput.vue';
import RedButton from '@/components/RedButton.vue';

const props = defineProps({
  categories: {
    type: Array,
    default: () => []
  },
  modeles: {
    type: Array,
    default: () => []
  },
  tailles: {
    type: Array,
    default: () => []
  },
  cadres: {
    type: Array,
    default: () => []
  },
  geometries: {
    type: Array,
    default: () => []
  },
  colors: {
    type: Array,
    default: () => []
  },
  utilsUrl: {
    type: String,
    required: true
  },
  authHeaders: {
    type: Object,
    default: () => ({})
  }
});

const emit = defineEmits(['created']);

const selectedType = ref('velo');

const articleForm = ref({
  nom: '',
  description: '',
  prix: '',
  poids: '',
  qteStock: '',
  annee: new Date().getFullYear(),
  dispoEnLigne: true,
  categorieId: ''
});

const veloForm = ref({
  idModele: '',
  lienVue360: '',
  couleurIds: [],
  tailleIds: [],
  cadreIds: [],
  geometryIds: []
});

const accessoireForm = ref({
  typeAccessoire: '',
  tailleUnique: false,
  materiaux: '',
  dimensions: '',
  caracteristiques: ''
});

const imageFiles = ref([]);
const imagePreviews = ref([]);
const feedback = ref({ type: '', message: '' });

const selectedCategoryName = computed(() => {
  const category = props.categories.find((item) => item.categorieId === Number(articleForm.value.categorieId));
  return category?.nom || '';
});

const generatedReference = computed(() => {
  const year = String(articleForm.value.annee || new Date().getFullYear()).slice(-2);
  const parts = [
    'ART',
    year,
    articleForm.value.nom,
    selectedCategoryName.value
  ]
    .filter(Boolean)
    .map((part) => String(part)
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '')
      .toUpperCase()
      .replace(/[^A-Z0-9]+/g, '')
    )
    .filter(Boolean);

  return (parts.join('-') || 'ART').slice(0, 20);
});

const getApiErrorMessage = (error) => {
  const status = error?.response?.status;
  const data = error?.response?.data;

  if (!status) {
    return 'Le serveur ne repond pas. Verifiez que le backend est demarre.';
  }

  if (typeof data === 'string' && data.trim().length > 0) {
    return `Erreur ${status}: ${data}`;
  }

  if (data?.title) {
    if (data?.errors && typeof data.errors === 'object') {
      const validationMessages = Object.values(data.errors)
        .flat()
        .filter(Boolean);

      if (validationMessages.length) {
        return `Erreur ${status}: ${validationMessages.join(' | ')}`;
      }
    }

    return `Erreur ${status}: ${data.title}`;
  }

  return `Erreur ${status}: la requete a echoue.`;
};

const clearImageSelection = () => {
  imagePreviews.value.forEach((preview) => URL.revokeObjectURL(preview.previewUrl));
  imagePreviews.value = [];
  imageFiles.value = [];
};

const handleImageSelection = (event) => {
  clearImageSelection();

  const files = Array.from(event.target.files || []);
  imageFiles.value = files;
  imagePreviews.value = files.map((file) => ({
    file,
    previewUrl: URL.createObjectURL(file)
  }));
};

const readFileAsDataUrl = (file) => new Promise((resolve, reject) => {
  const reader = new FileReader();
  reader.onload = () => resolve(reader.result);
  reader.onerror = () => reject(new Error(`Impossible de lire ${file.name}`));
  reader.readAsDataURL(file);
});

const normalizeArticlePayload = () => ({
  reference: generatedReference.value,
  nom: articleForm.value.nom.trim(),
  description: articleForm.value.description.trim(),
  prix: Number(articleForm.value.prix),
  poids: Number(articleForm.value.poids),
  qteStock: Number(articleForm.value.qteStock),
  annee: Number(articleForm.value.annee),
  dispoEnLigne: Boolean(articleForm.value.dispoEnLigne),
  categorieId: Number(articleForm.value.categorieId)
});

const uploadImagesForArticle = async (articleId) => {
  let uploadedCount = 0;

  for (const file of imageFiles.value) {
    await axios.post(props.utilsUrl + 'Images', {
      chemin: await readFileAsDataUrl(file),
      articleId
    }, {
      headers: props.authHeaders
    });

    uploadedCount += 1;
  }

  return uploadedCount;
};

const submitProduct = async () => {
  feedback.value = { type: '', message: '' };

  if (!articleForm.value.nom || !articleForm.value.description || !articleForm.value.categorieId) {
    feedback.value = {
      type: 'error',
      message: 'Remplissez au moins le nom, la description et la categorie de l\'article.'
    };
    return;
  }

  if (selectedType.value === 'velo' && (!veloForm.value.idModele || !veloForm.value.lienVue360 || !veloForm.value.couleurIds.length || !veloForm.value.tailleIds.length || !veloForm.value.cadreIds.length || !veloForm.value.geometryIds.length)) {
    feedback.value = {
      type: 'error',
      message: 'Pour un velo, selectionnez le modele, renseignez le lien vue 360, et selectionnez au moins une couleur, une taille, un cadre et une geometrie.'
    };
    return;
  }

  if (selectedType.value === 'accessoire' && (!accessoireForm.value.typeAccessoire || !accessoireForm.value.materiaux || !accessoireForm.value.caracteristiques)) {
    feedback.value = {
      type: 'error',
      message: 'Pour un accessoire, renseignez type, materiaux et caracteristiques.'
    };
    return;
  }

  try {
    const articleResponse = await axios.post(props.utilsUrl + 'Articles', normalizeArticlePayload(), {
      headers: props.authHeaders
    });

    const createdArticle = articleResponse.data;
    const articleId = createdArticle?.articleId;

    const uploadedCount = await uploadImagesForArticle(articleId);

    if (selectedType.value === 'velo') {
      const veloResponse = await axios.post(props.utilsUrl + 'Velos', {
        idArticle: articleId,
        lienVue360: veloForm.value.lienVue360.trim(),
        idModele: Number(veloForm.value.idModele)
      }, {
        headers: props.authHeaders
      });

      const veloId = veloResponse.data?.idVelo;

      const associationEndpoints = [
        { ids: veloForm.value.couleurIds, endpoint: 'Couleurs' },
        { ids: veloForm.value.tailleIds, endpoint: 'Tailles' },
        { ids: veloForm.value.cadreIds, endpoint: 'Cadres' },
        { ids: veloForm.value.geometryIds, endpoint: 'Geometries' }
      ];

      for (const assoc of associationEndpoints) {
        for (const id of assoc.ids) {
          try {
            await axios.post(props.utilsUrl + `Velos/${veloId}/${assoc.endpoint}`, {
              id: id
            }, {
              headers: props.authHeaders
            });
          } catch (e) {
            console.warn(`Impossible d'associer ${assoc.endpoint} ID ${id}:`, e);
          }
        }
      }
    } else {
      await axios.post(props.utilsUrl + 'Accessoires', {
        ...normalizeArticlePayload(),
        articleId,
        typeAccessoire: accessoireForm.value.typeAccessoire.trim(),
        tailleUnique: Boolean(accessoireForm.value.tailleUnique),
        materiaux: accessoireForm.value.materiaux.trim(),
        dimensions: accessoireForm.value.dimensions.trim() || null,
        caracteristiques: accessoireForm.value.caracteristiques.trim()
      }, {
        headers: props.authHeaders
      });
    }

    feedback.value = {
      type: 'success',
      message: `Produit cree: article ${createdArticle.reference} + ${selectedType.value}. ${uploadedCount} image${uploadedCount > 1 ? 's' : ''} ajoutee${uploadedCount > 1 ? 's' : ''}.`
    };

    emit('created', createdArticle);
    clearImageSelection();
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: getApiErrorMessage(error)
    };
  }
};

onBeforeUnmount(() => {
  clearImageSelection();
});
</script>

<template>
  <section class="panel product-panel">
    <div class="panel-heading">
      <div>
        <h2>Ajoutee un article</h2>
      </div>
      <div class="reference-pill">
        Reference auto
        <strong>{{ generatedReference }}</strong>
      </div>
    </div>

    <form @submit.prevent="submitProduct">
      <div class="form-flow">
        <div class="field field-medium">
          <Input v-model="articleForm.nom" label="Nom" required />
        </div>
        <div class="field field-medium">
          <SelectInput
            v-model="articleForm.categorieId"
            label="Categorie"
            :required="true"
            :options="categories.map((category) => ({ label: category.nom, value: category.categorieId }))"
          />
        </div>
        <div class="field field-full">
          <TextAreaInput v-model="articleForm.description" label="Description" required :rows="4" />
        </div>
        <div class="field field-small">
          <Input v-model="articleForm.prix" label="Prix" type="number" step="0.01" required />
        </div>
        <div class="field field-small">
          <Input v-model="articleForm.poids" label="Poids" type="number" step="0.1" required />
        </div>
        <div class="field field-small">
          <Input v-model="articleForm.qteStock" label="Quantite en stock" type="number" required />
        </div>
        <div class="field field-small">
          <Input v-model="articleForm.annee" label="Annee" type="number" required />
        </div>
        <label class="toggle full">
          <input v-model="articleForm.dispoEnLigne" type="checkbox" />
          Disponible en ligne
        </label>
      </div>

      <div class="upload-block">
        <h3>Images</h3>
        <FileInput label="Selectionner des images" accept="image/*" multiple @change="handleImageSelection" />

        <div v-if="imagePreviews.length" class="image-preview-list">
          <figure v-for="preview in imagePreviews" :key="preview.previewUrl" class="image-preview">
            <img :src="preview.previewUrl" :alt="preview.file.name" />
            <figcaption>{{ preview.file.name }}</figcaption>
          </figure>
        </div>
      </div>

      <div class="choice-row">
        <button
          type="button"
          :class="['choice-card', { active: selectedType === 'velo' }]"
          @click="selectedType = 'velo'"
        >
          <span class="choice-kicker">Produit roulant</span>
          <strong>Velo</strong>
          <small>Associer un modele et une vue 360.</small>
        </button>

        <button
          type="button"
          :class="['choice-card', { active: selectedType === 'accessoire' }]"
          @click="selectedType = 'accessoire'"
        >
          <span class="choice-kicker">Produit complementaire</span>
          <strong>Accessoire</strong>
          <small>Ajouter les caracteristiques de l'accessoire.</small>
        </button>
      </div>

      <div class="subform-panel" v-if="selectedType === 'velo'">
        <div class="form-flow">
          <div class="field field-medium">
            <SelectInput
              v-model="veloForm.idModele"
              label="Modele"
              :required="true"
              :options="modeles.map((modele) => ({ label: modele.nomModele, value: modele.idModele }))"
            />
          </div>
          <div class="field field-large">
            <Input v-model="veloForm.lienVue360" label="Lien vue 360" type="url" required />
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Couleurs</label>
              <div class="pill-list">
                <label v-for="color in colors" :key="color.idCouleur" class="pill-item" :class="{ 'is-active': veloForm.couleurIds.includes(color.idCouleur) }">
                  <input
                    type="checkbox"
                    class="sr-only"
                    :value="color.idCouleur"
                    v-model="veloForm.couleurIds"
                  />
                  {{ color.nomCouleur }}
                </label>
              </div>
            </div>
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Tailles</label>
              <div class="pill-list">
                <label v-for="size in tailles" :key="size.idTaille" class="pill-item" :class="{ 'is-active': veloForm.tailleIds.includes(size.idTaille) }">
                  <input
                    type="checkbox"
                    class="sr-only"
                    :value="size.idTaille"
                    v-model="veloForm.tailleIds"
                  />
                  {{ size.libelleTaille }}
                </label>
              </div>
            </div>
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Cadres</label>
              <div class="pill-list">
                <label v-for="cadre in cadres" :key="cadre.idCadre" class="pill-item" :class="{ 'is-active': veloForm.cadreIds.includes(cadre.idCadre) }">
                  <input
                    type="checkbox"
                    class="sr-only"
                    :value="cadre.idCadre"
                    v-model="veloForm.cadreIds"
                  />
                  {{ cadre.nomMat }}
                </label>
              </div>
            </div>
          </div>
          
          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Géométries</label>
              <div class="pill-list">
                <label v-for="geo in geometries" :key="geo.idGeometry" class="pill-item" :class="{ 'is-active': veloForm.geometryIds.includes(geo.idGeometry) }">
                  <input
                    type="checkbox"
                    class="sr-only"
                    :value="geo.idGeometry"
                    v-model="veloForm.geometryIds"
                  />
                  {{ geo.nomGeometrie || 'Geo ' + geo.idGeometry }}
                </label>
              </div>
            </div>
          </div>

        </div>
      </div>

      <RedButton type="submit">Creer le produit</RedButton>

      <p v-if="feedback.message" :class="['feedback', feedback.type]">{{ feedback.message }}</p>
    </form>
  </section>
</template>

<style scoped>
.product-panel {
  background: #fff;
  border: 1px solid #ddd;
  box-shadow: var(--card-shadow);
  padding: 1.2rem;
}

.panel-heading {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  align-items: flex-start;
  margin-bottom: 1rem;
}

.section-label {
  font-size: 0.8rem;
  text-transform: uppercase;
  letter-spacing: 0.12em;
  color: #777;
  margin-bottom: 0.35rem;
  text-align: left;
}

.panel-heading h2 {
  font-size: 1.8rem;
  text-transform: uppercase;
  text-align: left;
}

.reference-pill {
  border: 1px solid #111;
  padding: 0.5rem 0.8rem;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 0.15rem;
  text-align: right;
  min-width: 180px;
}

.form-flow {
  display: flex;
  flex-wrap: wrap;
  gap: 0.8rem 1rem;
  align-items: flex-start;
}

.field {
  flex: 1 1 260px;
  min-width: 220px;
  max-width: 420px;
}

.field-small {
  flex: 1 1 180px;
  min-width: 160px;
  max-width: 260px;
}

.field-medium {
  flex: 1 1 280px;
  min-width: 240px;
  max-width: 420px;
}

.field-large {
  flex: 2 1 420px;
  min-width: 280px;
  max-width: 640px;
}

.field-full {
  flex: 1 1 100%;
  max-width: 100%;
}

.full {
  grid-column: 1 / -1;
}

.toggle {
  display: flex;
  align-items: center;
  gap: 0.55rem;
  font-weight: 600;
}

.toggle input {
  width: 18px;
  height: 18px;
}

.upload-block {
  margin: 1rem 0;
  padding: 0.95rem;
  background: #f5f5f5;
  border: 1px solid #e3e3e3;
}

.upload-block h3,
.upload-block p {
  text-align: left;
}

.image-preview-list {
  margin-top: 0.9rem;
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
}

.image-preview {
  margin: 0;
  background: #fff;
  border: 1px solid #d4d4d4;
  padding: 0.5rem;
  width: 140px;
}

.image-preview img {
  width: 100%;
  height: 110px;
  object-fit: cover;
  display: block;
}

.image-preview figcaption {
  font-size: 0.82rem;
  margin-top: 0.35rem;
  text-align: left;
  word-break: break-word;
}

.choice-row {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  margin-bottom: 1rem;
}

.choice-card {
  flex: 1 1 320px;
  border: 1px solid #111;
  background: #fff;
  min-height: 160px;
  padding: 1.1rem;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 0.55rem;
  text-align: left;
  cursor: pointer;
}

.choice-card strong {
  font-size: 1.9rem;
  line-height: 1;
}

.choice-kicker {
  text-transform: uppercase;
  letter-spacing: 0.1em;
  color: #666;
  font-size: 0.75rem;
}

.choice-card.active {
  background: #111;
  color: #fff;
}

.choice-card.active .choice-kicker {
  color: #d3d3d3;
}

.subform-panel {
  margin-bottom: 1rem;
  padding: 1rem;
  border: 1px solid #e3e3e3;
  background: #f5f5f5;
}

.feedback {
  margin-top: 0.8rem;
  padding: 0.65rem 0.75rem;
  font-weight: 600;
  width: fit-content;
  max-width: 100%;
  text-align: left;
}

.feedback.success {
  background: #dcf5e6;
  color: #0b6f2f;
}

.feedback.error {
  background: #ffe0e0;
  color: #991b1b;
}

.checkbox-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  text-align: left;
}

.checkbox-group-label {
  font-weight: 600;
  font-size: 0.95rem;
}

.sr-only {
  position: absolute;
  width: 1px;
  height: 1px;
  padding: 0;
  margin: -1px;
  overflow: hidden;
  clip: rect(0, 0, 0, 0);
  white-space: nowrap;
  border-width: 0;
}

.pill-list {
  display: flex;
  flex-wrap: wrap;
  gap: 0.6rem;
  padding: 0.2rem 0;
}

.pill-item {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 0.55rem 1.2rem;
  background: #fff;
  border: 1px solid #d4d4d4;
  font-size: 0.9rem;
  font-weight: 500;
  color: #444;
  cursor: pointer;
  transition: all 0.2s ease;
  user-select: none;
}

.pill-item:hover {
  border-color: #111;
  color: #111;
  background: #f9f9f9;
}

.pill-item.is-active {
  background: #111;
  border-color: #111;
  color: #fff;
}

@media (max-width: 760px) {
  .field,
  .field-small,
  .field-medium,
  .field-large,
  .field-full,
  .choice-card {
    min-width: 100%;
    max-width: 100%;
    flex-basis: 100%;
  }

  .panel-heading {
    flex-direction: column;
  }

  .reference-pill {
    align-items: flex-start;
    text-align: left;
  }
}
</style>
