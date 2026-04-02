<script setup>
import { ref } from 'vue';
import Input from '@/components/Input.vue';
import RedButton from '@/components/RedButton.vue';
import axios from 'axios';

const props = defineProps({
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

const categorieForm = ref({ nom: '' });
const modeleForm = ref({ nomModele: '' });
const couleurForm = ref({ nomCouleur: '', effetPeinture: '' });
const feedback = ref({ type: '', message: '' });

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

const createCategorie = async () => {
  feedback.value = { type: '', message: '' };

  try {
    const response = await axios.post(props.utilsUrl + 'Categories', {
      nom: categorieForm.value.nom.trim()
    }, {
      headers: props.authHeaders
    });

    categorieForm.value.nom = '';
    feedback.value = {
      type: 'success',
      message: `Categorie creee (#${response.data?.categorieId ?? 'n/a'}).`
    };
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: getApiErrorMessage(error)
    };
  }
};

const createModele = async () => {
  feedback.value = { type: '', message: '' };

  try {
    const response = await axios.post(props.utilsUrl + 'Modeles', {
      nomModele: modeleForm.value.nomModele.trim()
    }, {
      headers: props.authHeaders
    });

    modeleForm.value.nomModele = '';
    feedback.value = {
      type: 'success',
      message: `Modele cree (#${response.data?.idModele ?? 'n/a'}).`
    };
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: getApiErrorMessage(error)
    };
  }
};

const createCouleur = async () => {
  feedback.value = { type: '', message: '' };

  try {
    const response = await axios.post(props.utilsUrl + 'Couleurs', {
      nomCouleur: couleurForm.value.nomCouleur.trim(),
      effetPeinture: couleurForm.value.effetPeinture.trim() || null
    }, {
      headers: props.authHeaders
    });

    couleurForm.value.nomCouleur = '';
    couleurForm.value.effetPeinture = '';
    feedback.value = {
      type: 'success',
      message: `Couleur creee (#${response.data?.idCouleur ?? 'n/a'}).`
    };
  } catch (error) {
    feedback.value = {
      type: 'error',
      message: getApiErrorMessage(error)
    };
  }
};
</script>

<template>
  <section class="panel catalog-panel">
    <div class="panel-heading">
      <div>
        <h2>Catalogue</h2>
      </div>
      <div class="summary-pill">
        <span>{{ colors.length }} couleurs disponibles</span>
      </div>
    </div>

    <div class="catalog-grid">
      <form class="mini-card" @submit.prevent="createCategorie">
        <h3>Nouvelle categorie</h3>
        <Input v-model="categorieForm.nom" label="Nom" required />
        <RedButton type="submit">Ajouter</RedButton>
      </form>

      <form class="mini-card" @submit.prevent="createModele">
        <h3>Nouveau modele</h3>
        <Input v-model="modeleForm.nomModele" label="Nom" required />
        <RedButton type="submit">Ajouter</RedButton>
      </form>

      <form class="mini-card" @submit.prevent="createCouleur">
        <h3>Nouvelle couleur</h3>
        <Input v-model="couleurForm.nomCouleur" label="Nom" required />
        <Input v-model="couleurForm.effetPeinture" label="Effet peinture" />
        <RedButton type="submit">Ajouter</RedButton>
      </form>
    </div>

    <p v-if="feedback.message" :class="['feedback', feedback.type]">{{ feedback.message }}</p>
  </section>
</template>

<style scoped>
.catalog-panel {
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

.summary-pill {
  border: 1px solid #111;
  padding: 0.5rem 0.8rem;
  text-align: right;
}

.catalog-grid {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 1rem;
}

.mini-card {
  border: 1px solid #ddd;
  background: #f5f5f5;
  padding: 1rem;
  gap: 1rem;
  display: flex;
  align-items: center;
  justify-content: start;
  flex-direction: column;
  text-align: left;
}

.mini-card > *:last-child {
    margin-top: auto;
}

.mini-card h3 {
  text-transform: uppercase;
  margin-bottom: 0.75rem;
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
  .catalog-grid {
    grid-template-columns: 1fr;
  }

  .panel-heading {
    flex-direction: column;
  }
}
</style>
