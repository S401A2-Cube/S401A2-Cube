<script setup>
import { computed, ref } from 'vue';
import Input from '@/components/Input.vue';
import SelectInput from '@/components/SelectInput.vue';
import TextAreaInput from '@/components/TextAreaInput.vue';
import RedButton from '@/components/RedButton.vue';

const props = defineProps({
  article: {
    type: Object,
    default: null
  },
  modeles: {
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
const veloForm = ref({
  idModele: '',
  lienVue360: ''
});
const accessoireForm = ref({
  typeAccessoire: '',
  tailleUnique: false,
  materiaux: '',
  dimensions: '',
  caracteristiques: ''
});
const feedback = ref({ type: '', message: '' });

const articleReference = computed(() => props.article?.reference || 'Aucun article');
const articleId = computed(() => props.article?.articleId || null);

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

const createVelo = async () => {
  feedback.value = { type: '', message: '' };

  if (!articleId.value) {
    feedback.value = {
      type: 'error',
      message: 'Creez dabord un article.'
    };
    return;
  }

  try {
    const response = await fetch(props.utilsUrl + 'Velos', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...props.authHeaders
      },
      body: JSON.stringify({
        idArticle: articleId.value,
        lienVue360: veloForm.value.lienVue360.trim(),
        idModele: Number(veloForm.value.idModele)
      })
    }).then((result) => {
      if (!result.ok) {
        throw result;
      }
      return result.json();
    });

    feedback.value = {
      type: 'success',
      message: `Velo cree avec succes (IdVelo: ${response?.idVelo ?? 'n/a'}).`
    };
    emit('created', { kind: 'velo', payload: response });
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: error?.message || getApiErrorMessage(error)
    };
  }
};

const createAccessoire = async () => {
  feedback.value = { type: '', message: '' };

  if (!articleId.value) {
    feedback.value = {
      type: 'error',
      message: 'Creez dabord un article.'
    };
    return;
  }

  try {
    const response = await fetch(props.utilsUrl + 'Accessoires', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...props.authHeaders
      },
      body: JSON.stringify({
        articleId: articleId.value,
        reference: props.article.reference,
        nom: props.article.nom,
        description: props.article.description,
        prix: props.article.prix,
        poids: props.article.poids,
        qteStock: props.article.qteStock,
        annee: props.article.annee,
        dispoEnLigne: props.article.dispoEnLigne,
        categorieId: props.article.categorieId,
        typeAccessoire: accessoireForm.value.typeAccessoire.trim(),
        tailleUnique: Boolean(accessoireForm.value.tailleUnique),
        materiaux: accessoireForm.value.materiaux.trim(),
        dimensions: accessoireForm.value.dimensions.trim() || null,
        caracteristiques: accessoireForm.value.caracteristiques.trim()
      })
    }).then((result) => {
      if (!result.ok) {
        throw result;
      }
      return result.json();
    });

    feedback.value = {
      type: 'success',
      message: `Accessoire cree avec succes (ArticleId: ${response?.articleId ?? articleId.value}).`
    };
    emit('created', { kind: 'accessoire', payload: response });
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: error?.message || getApiErrorMessage(error)
    };
  }
};
</script>

<template>
  <section class="panel type-panel">
    <div class="panel-heading">
      <div>
        <h2>Velo ou Accessoire</h2>
      </div>
      <div class="reference-pill">
        Article actif
        <strong>{{ articleReference }}</strong>
      </div>
    </div>

    <div class="choice-grid">
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
      <form class="form-grid" @submit.prevent="createVelo">
        <Input :model-value="articleReference" label="Reference article" :required="false" readonly />
        <SelectInput
          v-model="veloForm.idModele"
          label="Modele"
          :required="true"
          :options="modeles.map((modele) => ({ label: modele.nomModele, value: modele.idModele }))"
        />
        <Input v-model="veloForm.lienVue360" label="Lien vue 360" type="url" required />
        <RedButton type="submit">Creer le velo</RedButton>
      </form>
    </div>

    <div class="subform-panel" v-else>
      <form class="form-grid" @submit.prevent="createAccessoire">
        <Input :model-value="articleReference" label="Reference article" :required="false" readonly />
        <Input v-model="accessoireForm.typeAccessoire" label="Type d'accessoire" required />
        <Input v-model="accessoireForm.materiaux" label="Materiaux" required />
        <Input v-model="accessoireForm.dimensions" label="Dimensions" />
        <TextAreaInput v-model="accessoireForm.caracteristiques" label="Caracteristiques" required :rows="4" />
        <label class="toggle full">
          <input v-model="accessoireForm.tailleUnique" type="checkbox" />
          Taille unique
        </label>
        <RedButton type="submit">Creer l'accessoire</RedButton>
      </form>
    </div>

    <p v-if="feedback.message" :class="['feedback', feedback.type]">{{ feedback.message }}</p>
  </section>
</template>

<style scoped>
.type-panel {
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

.choice-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 1rem;
}

.choice-card {
  border: 1px solid #111;
  background: #fff;
  box-shadow: var(--card-shadow);
  min-height: 180px;
  padding: 1.2rem;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  gap: 0.55rem;
  text-align: left;
  cursor: pointer;
  transition: transform 0.2s ease, background-color 0.2s ease, color 0.2s ease;
}

.choice-card strong {
  font-size: 2.1rem;
  line-height: 1;
}

.choice-card small,
.choice-kicker {
  text-align: left;
}

.choice-kicker {
  text-transform: uppercase;
  letter-spacing: 0.1em;
  color: #666;
  font-size: 0.75rem;
}

.choice-card.active,
.choice-card:hover {
  background: #111;
  color: #fff;
  transform: translateY(-2px);
}

.choice-card.active .choice-kicker,
.choice-card:hover .choice-kicker {
  color: #d3d3d3;
}

.subform-panel {
  margin-top: 1rem;
  padding: 1rem;
  border: 1px solid #e3e3e3;
  background: #f5f5f5;
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
  .choice-grid,
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
