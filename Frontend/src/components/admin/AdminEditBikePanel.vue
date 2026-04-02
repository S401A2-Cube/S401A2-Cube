<script setup>
import { computed, ref, watch } from 'vue';
import axios from 'axios';
import Input from '@/components/Input.vue';
import SelectInput from '@/components/SelectInput.vue';
import TextAreaInput from '@/components/TextAreaInput.vue';
import RedButton from '@/components/RedButton.vue';

const props = defineProps({
  selectedBike: {
    type: Object,
    default: null
  },
  velos: {
    type: Array,
    default: () => []
  },
  categories: {
    type: Array,
    default: () => []
  },
  modeles: {
    type: Array,
    default: () => []
  },
  colors: {
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
  utilsUrl: {
    type: String,
    required: true
  },
  authHeaders: {
    type: Object,
    default: () => ({})
  }
});

const emit = defineEmits(['updated', 'select-bike']);

const feedback = ref({ type: '', message: '' });
const isSubmitting = ref(false);
const searchQuery = ref('');

const form = ref({
  articleId: null,
  idVelo: null,
  reference: '',
  nom: '',
  description: '',
  prix: '',
  poids: '',
  qteStock: '',
  annee: '',
  dispoEnLigne: true,
  categorieId: '',
  idModele: '',
  lienVue360: '',
  couleurIds: [],
  tailleIds: [],
  cadreIds: [],
  geometrieIds: []
});

const filteredVelos = computed(() => {
  if (!searchQuery.value.trim()) {
    return [];
  }

  const query = searchQuery.value.toLowerCase();
  return props.velos.filter((velo) => {
    const nom = velo.article?.nom?.toLowerCase() || '';
    const reference = velo.article?.reference?.toLowerCase() || '';
    const modele = velo.modeleVelo?.nomModele?.toLowerCase() || '';

    return nom.includes(query) || reference.includes(query) || modele.includes(query);
  }).slice(0, 8);
});

const hydrateForm = (bike) => {
  const article = bike?.article || {};

  form.value = {
    articleId: article.articleId ?? null,
    idVelo: bike?.idVelo ?? null,
    reference: article.reference ?? '',
    nom: article.nom ?? '',
    description: article.description ?? '',
    prix: article.prix ?? '',
    poids: article.poids ?? '',
    qteStock: article.qteStock ?? '',
    annee: article.annee ?? '',
    dispoEnLigne: Boolean(article.dispoEnLigne),
    categorieId: article.categorieId ?? '',
    idModele: bike?.idModele ?? '',
    lienVue360: bike?.lienVue360 ?? '',
    couleurIds: (bike?.couleurs || []).map((item) => item.idCouleur),
    tailleIds: (bike?.tailles || []).map((item) => item.idTaille),
    cadreIds: (bike?.cadres || []).map((item) => item.idMateriau ?? item.idCadre),
    geometrieIds: (bike?.geometries || []).map((item) => item.idGeometrie ?? item.idGeometry)
  };
};

watch(
  () => props.selectedBike,
  (bike) => {
    feedback.value = { type: '', message: '' };

    if (!bike) {
      return;
    }

    hydrateForm(bike);
  },
  { immediate: true }
);

const selectBike = (bike) => {
  searchQuery.value = '';
  emit('select-bike', bike);
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

const saveBike = async () => {
  feedback.value = { type: '', message: '' };

  if (!form.value.articleId || !form.value.idVelo) {
    feedback.value = { type: 'error', message: 'Selectionnez un velo a modifier.' };
    return;
  }

  if (!form.value.nom.trim() || !form.value.description.trim() || !form.value.idModele || !form.value.lienVue360.trim()) {
    feedback.value = {
      type: 'error',
      message: 'Renseignez au moins le nom, la description, le modele et le lien vue 360.'
    };
    return;
  }

  isSubmitting.value = true;

  try {
    await axios.put(props.utilsUrl + `Articles/${form.value.articleId}`, {
      articleId: form.value.articleId,
      reference: form.value.reference.trim(),
      nom: form.value.nom.trim(),
      description: form.value.description.trim(),
      prix: Number(form.value.prix),
      poids: Number(form.value.poids),
      qteStock: Number(form.value.qteStock),
      annee: Number(form.value.annee),
      dispoEnLigne: Boolean(form.value.dispoEnLigne),
      categorieId: Number(form.value.categorieId)
    }, {
      headers: props.authHeaders
    });

    await axios.put(props.utilsUrl + `Velos/${form.value.idVelo}`, {
      idVelo: form.value.idVelo,
      idArticle: form.value.articleId,
      idModele: Number(form.value.idModele),
      lienVue360: form.value.lienVue360.trim(),
      couleurs: form.value.couleurIds.map((idCouleur) => ({ idCouleur: Number(idCouleur) })),
      tailles: form.value.tailleIds.map((idTaille) => ({ idTaille: Number(idTaille) })),
      cadres: form.value.cadreIds.map((idMateriau) => ({ idMateriau: Number(idMateriau) })),
      geometries: form.value.geometrieIds.map((idGeometrie) => ({ idGeometrie: Number(idGeometrie) }))
    }, {
      headers: props.authHeaders
    });

    feedback.value = {
      type: 'success',
      message: 'Le velo et son article ont ete mis a jour.'
    };

    emit('updated');
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: getApiErrorMessage(error)
    };
  } finally {
    isSubmitting.value = false;
  }
};
</script>

<template>
  <section class="panel edit-panel">
    <div class="panel-heading">
      <h2>Modifier un velo</h2>
    </div>

    <div class="search-block">
      <Input v-model="searchQuery" label="Rechercher un velo" :required="false" />
      <div v-if="filteredVelos.length" class="search-results">
        <button
          v-for="velo in filteredVelos"
          :key="velo.idVelo"
          type="button"
          class="search-result-item"
          @click="selectBike(velo)"
        >
          <strong>{{ velo.article?.nom }}</strong>
          <small>{{ velo.article?.reference }} - {{ velo.modeleVelo?.nomModele }}</small>
        </button>
      </div>
    </div>

    <p v-if="!selectedBike" class="empty-state">
      Choisissez un velo via la recherche pour modifier ses informations.
    </p>

    <form v-else class="edit-form" @submit.prevent="saveBike">
      <section class="edit-section">
        <h3>Informations article</h3>
        <div class="form-flow">
          <div class="field field-medium">
            <Input v-model="form.reference" label="Reference" required />
          </div>
          <div class="field field-medium">
            <SelectInput
              v-model="form.categorieId"
              label="Categorie"
              :required="true"
              :options="categories.map((category) => ({ label: category.nom, value: category.categorieId }))"
            />
          </div>
          <div class="field field-large">
            <Input v-model="form.nom" label="Nom" required />
          </div>
          <div class="field field-full">
            <TextAreaInput v-model="form.description" label="Description" required :rows="4" />
          </div>
          <div class="field field-small">
            <Input v-model="form.prix" label="Prix" type="number" required />
          </div>
          <div class="field field-small">
            <Input v-model="form.poids" label="Poids" type="number" required />
          </div>
          <div class="field field-small">
            <Input v-model="form.qteStock" label="Quantite en stock" type="number" required />
          </div>
          <div class="field field-small">
            <Input v-model="form.annee" label="Annee" type="number" required />
          </div>
          <label class="toggle full">
            <input v-model="form.dispoEnLigne" type="checkbox" />
            Disponible en ligne
          </label>
        </div>
      </section>

      <section class="edit-section">
        <h3>Informations velo</h3>
        <div class="form-flow">
          <div class="field field-medium">
            <SelectInput
              v-model="form.idModele"
              label="Modele"
              :required="true"
              :options="modeles.map((modele) => ({ label: modele.nomModele, value: modele.idModele }))"
            />
          </div>
          <div class="field field-large">
            <Input v-model="form.lienVue360" label="Lien vue 360" type="url" required />
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Couleurs</label>
              <div class="pill-list">
                <label v-for="color in colors" :key="color.idCouleur" class="pill-item" :class="{ 'is-active': form.couleurIds.includes(color.idCouleur) }">
                  <input type="checkbox" class="sr-only" :value="color.idCouleur" v-model="form.couleurIds" />
                  {{ color.nomCouleur }}
                </label>
              </div>
            </div>
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Tailles</label>
              <div class="pill-list">
                <label v-for="size in tailles" :key="size.idTaille" class="pill-item" :class="{ 'is-active': form.tailleIds.includes(size.idTaille) }">
                  <input type="checkbox" class="sr-only" :value="size.idTaille" v-model="form.tailleIds" />
                  {{ size.libelleTaille }}
                </label>
              </div>
            </div>
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Cadres</label>
              <div class="pill-list">
                <label v-for="cadre in cadres" :key="cadre.idMateriau" class="pill-item" :class="{ 'is-active': form.cadreIds.includes(cadre.idMateriau) }">
                  <input type="checkbox" class="sr-only" :value="cadre.idMateriau" v-model="form.cadreIds" />
                  {{ cadre.nomMat }} - {{ cadre.formeCadre }}
                </label>
              </div>
            </div>
          </div>

          <div class="field field-full">
            <div class="checkbox-group">
              <label class="checkbox-group-label">Geometries</label>
              <div class="pill-list">
                <label v-for="geo in geometries" :key="geo.idGeometrie" class="pill-item" :class="{ 'is-active': form.geometrieIds.includes(geo.idGeometrie) }">
                  <input type="checkbox" class="sr-only" :value="geo.idGeometrie" v-model="form.geometrieIds" />
                  {{ geo.nomPiece }}
                </label>
              </div>
            </div>
          </div>
        </div>
      </section>

      <RedButton type="submit" :disabled="isSubmitting">{{ isSubmitting ? 'Enregistrement...' : 'Enregistrer les modifications' }}</RedButton>
      <p v-if="feedback.message" :class="['feedback', feedback.type]">{{ feedback.message }}</p>
    </form>
  </section>
</template>

<style scoped>
.edit-panel {
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

.panel-heading h2 {
  font-size: 1.8rem;
  text-transform: uppercase;
  text-align: left;
}

.search-block {
  position: relative;
  margin-bottom: 1rem;
}

.search-results {
  position: absolute;
  top: calc(100% - 10px);
  left: 0;
  right: 0;
  border: 1px solid #d4d4d4;
  background: #fff;
  max-height: 260px;
  overflow-y: auto;
  z-index: 10;
}

.search-result-item {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  gap: 0.2rem;
  padding: 0.6rem 0.7rem;
  border: none;
  border-bottom: 1px solid #ececec;
  background: #fff;
  text-align: left;
  cursor: pointer;
}

.search-result-item:hover {
  background: #f7f7f7;
}

.search-result-item small {
  color: #666;
}

.empty-state {
  text-align: left;
  padding: 0.85rem;
  border: 1px solid #ddd;
  background: #f5f5f5;
}

.edit-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  align-items: end;
}

.edit-section {
  border: 1px solid #e3e3e3;
  background: #f5f5f5;
  padding: 1rem;
}

.edit-section h3 {
  text-align: left;
  margin-bottom: 0.55rem;
  text-transform: uppercase;
  font-size: 1rem;
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
  .field,
  .field-small,
  .field-medium,
  .field-large,
  .field-full {
    min-width: 100%;
    max-width: 100%;
    flex-basis: 100%;
  }
}
</style>
