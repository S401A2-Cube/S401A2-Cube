<script setup>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import Landing from '../components/article/Landing.vue';
import Specifications from '../components/article/Specifications.vue';

import axios from 'axios';
import { useUtilsStore } from '@/stores/utils';

const route = useRoute();

const utils = useUtilsStore();

const resVelo = ref(null);
const resArticle = ref(null);

axios.get(utils.url + "Velos/GetById/" + route.params.id).then(r => {
  const data = r.data;

  console.log(data);

  const articleData = data.article || {};

  resArticle.value = {
    articleId: articleData.articleId,
    reference: articleData.reference,
    nom: articleData.nom,
    description: articleData.description || "Description non disponible", 
    prix: articleData.prix,
    poids: articleData.poids,
    qteStock: articleData.qteStock,
    annee: articleData.annee,
    dispoEnLigne: articleData.dispoEnLigne,
    categorieId: articleData.categorieId,
    categorieArticle: articleData.categorieArticle || { 
      categorieId: articleData.categorieId, 
      nom: "Catégorie inconnue" 
    },
    motsCles: articleData.motsCles || []
  };

  resVelo.value = {
    idVelo: data.idVelo,
    idArticle: data.idArticle,
    lienVue360: data.lienVue360 || "TODO", 
    idModele: data.idModele,
    modeleVelo: data.modeleVelo,
    couleurs: data.couleurs || [],
    tailles: data.tailles || [],
    cadres: data.cadres || [],
    geometries: data.geometries || []
  };
});

</script>

<template>
  <div class="page-container" v-if="resArticle && resVelo">
    <Landing :article="resArticle" :velo="resVelo" />
    
    <main id="about">
      <section class="description_container">
        <div class="description_grid">
          <div class="description_text">
            <h2 class="section_label">Description</h2>
            <h3 class="bike_name">{{ resArticle.nom }}</h3>
            <p class="main_description">
              {{ resArticle.description }}
            </p>
            
            <div class="tags">
              <span v-for="tag in resArticle.motsCles" :key="tag.motCleId" class="tag">
                #{{ tag.nom }}
              </span>
            </div>
          </div>

          <div class="specs_highlight">
            <div class="spec_item">
              <span class="spec_value">{{ resArticle.poids }} KG</span>
              <span class="spec_label">Poids Plume</span>
            </div>
            <div class="spec_item">
              <span class="spec_value">{{ resVelo.cadres[0].nomMat }}</span>
              <span class="spec_label">Matériau de pointe</span>
            </div>
            <div class="spec_item">
              <span class="spec_value">{{ resArticle.annee }}</span>
              <span class="spec_label">Édition</span>
            </div>
          </div>
        </div>
      </section>
    </main>
    
    <Specifications :article="resArticle" :velo="resVelo" />
  </div>
  <div v-else class="loading-state">
    <p>Chargement des données du vélo...</p>
  </div>
</template>

<style scoped>
.loading-state {
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

.page-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
}

#about {
  padding: 100px 0;
  display: flex;
  justify-content: center;
  background-color: #f5f5f5;
  width: 100%;
  margin-bottom: 4rem;
}

.description_container {
  width: 70vw;
}

.description_grid {
  display: grid;
  grid-template-columns: 1.5fr 1fr;
  gap: 4rem;
  align-items: center;
}

.section_label {
  font-size: 14px;
  letter-spacing: 2px;
  color: #888;
  margin-bottom: 1rem;
  text-transform: uppercase;
}

.bike_name {
  font-size: 32px;
  font-weight: 800;
  margin-bottom: 1.5rem;
}

.main_description {
  font-size: 1.1rem;
  line-height: 1.6;
  color: #333;
  margin-bottom: 2rem;
  text-align: left;
}

.tags {
  display: flex;
  gap: 10px;
}

.tag {
  background: #000;
  color: #fff;
  padding: 5px 12px;
  font-size: 12px;
  font-weight: bold;
}

.specs_highlight {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  border-left: 1px solid #ddd;
  padding-left: 3rem;
}

.spec_item {
  display: flex;
  flex-direction: column;
  text-align: left;
}

.spec_value {
  font-size: 24px;
  font-weight: 700;
}

.spec_label {
  font-size: 12px;
  color: #666;
  text-transform: uppercase;
}
</style>