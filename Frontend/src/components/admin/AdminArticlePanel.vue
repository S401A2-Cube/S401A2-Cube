<script setup>
import { computed, onBeforeUnmount, ref, watch } from 'vue';
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
  articleReference: {
    type: String,
    default: ''
  },
  authHeaders: {
    type: Object,
    default: () => ({})
  },
  utilsUrl: {
    type: String,
    required: true
  }
});

const emit = defineEmits(['created']);

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

const articleFeedback = ref({ type: '', message: '' });
const imageFeedback = ref({ type: '', message: '' });
const imageFiles = ref([]);
const imagePreviews = ref([]);

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

const displayReference = computed(() => props.articleReference || generatedReference.value);

const readFileAsDataUrl = (file) => new Promise((resolve, reject) => {
  const reader = new FileReader();
  reader.onload = () => resolve(reader.result);
  reader.onerror = () => reject(new Error(`Impossible de lire ${file.name}`));
  reader.readAsDataURL(file);
});

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
    return `Erreur ${status}: ${data.title}`;
  }

  return `Erreur ${status}: la requete a echoue.`;
};

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
  if (!imageFiles.value.length) {
    return 0;
  }

  let uploadedCount = 0;

  for (const file of imageFiles.value) {
    await fetch(props.utilsUrl + 'Images', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...props.authHeaders
      },
      body: JSON.stringify({
        chemin: await readFileAsDataUrl(file),
        articleId
      })
    }).then((response) => {
      if (!response.ok) {
        throw new Error('Upload image failed');
      }
      return response.json();
    });
    uploadedCount += 1;
  }

  return uploadedCount;
};

const createArticle = async () => {
  articleFeedback.value = { type: '', message: '' };
  imageFeedback.value = { type: '', message: '' };

  if (!articleForm.value.nom || !articleForm.value.description || !articleForm.value.categorieId) {
    articleFeedback.value = {
      type: 'error',
      message: 'Remplissez au moins le nom, la description et la categorie.'
    };
    return;
  }

  try {
    const payloadToSend = normalizeArticlePayload();
    delete payloadToSend.categorieArticle;

    const response = await fetch(props.utilsUrl + 'Articles', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...props.authHeaders
      },
      body: JSON.stringify(payloadToSend)
    }).then((result) => {
      if (!result.ok) {
        throw result;
      }
      return result.json();
    });

    articleFeedback.value = {
      type: 'success',
      message: `Article cree avec la reference ${response?.reference || generatedReference.value}.`
    };

    emit('created', response);

    const uploadedCount = await uploadImagesForArticle(response?.articleId);
    if (uploadedCount > 0) {
      imageFeedback.value = {
        type: 'success',
        message: `${uploadedCount} image${uploadedCount > 1 ? 's' : ''} ajoutee${uploadedCount > 1 ? 's' : ''} a l'article.`
      };
    }

    clearImageSelection();
  } catch (error) {
    articleFeedback.value = {
      type: 'error',
      message: error?.message || getApiErrorMessage(error)
    };
  }
};

watch(
  () => props.articleReference,
  (value) => {
    if (value && !articleForm.value.nom) {
      articleFeedback.value = {
        type: '',
        message: `Reference auto generee: ${displayReference.value}`
      };
    }
  },
  { immediate: true }
);

onBeforeUnmount(() => {
  clearImageSelection();
});
</script>

<template>
  <section class="panel article-panel">
    <div class="panel-heading">
      <div>
        <h2>Article</h2>
      </div>
      <div class="reference-pill">
        Reference auto
        <strong>{{ displayReference }}</strong>
      </div>
    </div>

    <form class="article-form" @submit.prevent="createArticle">
      <div class="form-grid">
        <Input v-model="articleForm.nom" label="Nom" required />
        <SelectInput
          v-model="articleForm.categorieId"
          label="Categorie"
          :required="true"
          :options="categories.map((category) => ({ label: category.nom, value: category.categorieId }))"
        />
        <TextAreaInput v-model="articleForm.description" label="Description" required :rows="5" />
        <Input v-model="articleForm.prix" label="Prix" type="number" step="0.01" required />
        <Input v-model="articleForm.poids" label="Poids" type="number" step="0.1" required />
        <Input v-model="articleForm.qteStock" label="Quantite en stock" type="number" required />
        <Input v-model="articleForm.annee" label="Annee" type="number" required />
        <label class="toggle full">
          <input v-model="articleForm.dispoEnLigne" type="checkbox" />
          Disponible en ligne
        </label>
      </div>

      <div class="upload-block">
        <div>
          <h3>Images</h3>
          <p>Selectionnez des images. Elles seront ajoutees a l'article apres creation.</p>
        </div>

        <FileInput label="Selectionner des images" accept="image/*" multiple @change="handleImageSelection" />

        <div v-if="imagePreviews.length" class="image-preview-grid">
          <figure v-for="preview in imagePreviews" :key="preview.previewUrl" class="image-preview">
            <img :src="preview.previewUrl" :alt="preview.file.name" />
            <figcaption>{{ preview.file.name }}</figcaption>
          </figure>
        </div>
      </div>

      <RedButton type="submit">Creer l'article</RedButton>

      <p v-if="articleFeedback.message" :class="['feedback', articleFeedback.type]">{{ articleFeedback.message }}</p>
      <p v-if="imageFeedback.message" :class="['feedback', imageFeedback.type]">{{ imageFeedback.message }}</p>
    </form>
  </section>
</template>

<style scoped>
.article-panel {
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

.reference-pill strong {
  font-size: 1rem;
}

.article-form {
  text-align: left;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 1rem;
}

label {
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
  font-weight: 600;
}

input,
select,
textarea {
  width: 100%;
  border: 1px solid #cfcfcf;
  padding: 0.55rem 0.65rem;
  text-align: left;
}

textarea {
  min-height: 92px;
  resize: vertical;
}

.full {
  grid-column: 1 / -1;
}

.toggle {
  flex-direction: row;
  align-items: center;
  gap: 0.55rem;
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

.file-input {
  margin-top: 0.75rem;
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  border: 1px dashed #111;
  background: #fff;
  padding: 0.65rem 0.85rem;
  cursor: pointer;
  width: fit-content;
  position: relative;
}

.file-input input {
  width: 0.1px;
  height: 0.1px;
  opacity: 0;
  position: absolute;
  pointer-events: none;
}

.image-preview-grid {
  margin-top: 0.9rem;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
  gap: 0.75rem;
}

.image-preview {
  margin: 0;
  background: #fff;
  border: 1px solid #d4d4d4;
  padding: 0.5rem;
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

@media (max-width: 760px) {
  .form-grid {
    grid-template-columns: 1fr;
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
