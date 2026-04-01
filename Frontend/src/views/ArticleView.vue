<script setup>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import Landing from '../components/article/Landing.vue';
import Specifications from '../components/article/Specifications.vue';

import axios from 'axios';
import { useUtilsStore } from '@/stores/utils';
import RedButton from '@/components/RedButton.vue';

const route = useRoute();

const utils = useUtilsStore();

const resVelo = ref(null);
const selectedColor = ref(null);
const selectedSize = ref(null);
const resArticle = ref(null);
const notFound = ref(false);

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

  if (resVelo.value.couleurs && resVelo.value.couleurs.length > 0) {
    selectedColor.value = resVelo.value.couleurs[0].idCouleur;
  } else {
    selectedColor.value = null; 
  }

  if (resVelo.value.tailles && resVelo.value.tailles.length > 0) {
    selectedSize.value = resVelo.value.tailles[0].idTaille;
  } else {
    selectedSize.value = null; 
  }
}).catch(_=>notFound.value=true);

const addInShopCart = async () => {
  const token = localStorage.getItem('user_token');
  const payload = {
      articleId: resArticle.value.articleId,
      clientId: 1, // currentId
      qtePanier: 1,
      commandeId: null, 
      couleurId: selectedColor.value.couleurId, 
      tailleId: selectedSize.value.tailleId    
    };

  if (token) {
    try {
      await axios.post(utils.url + "LignePaniers/", payload, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      alert("Ajouté à votre panier en ligne !");
    } catch (error) {
      console.error("Erreur API :", error);
      alert("Erreur lors de l'ajout au panier distant.");
    }
  }
  else {
    saveToLocalStorage(payload);
    alert("Ajouté au panier !");
  }  
};

const saveToLocalStorage = (payload) => {
  const STORAGE_KEY = 'cube_shop_cart';
  const cart = JSON.parse(localStorage.getItem(STORAGE_KEY) || '[]');

  const existingProduct = cart.find(item => 
    item.articleId === payload.articleId && 
    item.couleurId === payload.couleurId && 
    item.tailleId === payload.tailleId
  );

  if (existingProduct) {
    existingProduct.qtePanier += 1;
  } else {
    cart.push({
      ...payload,
      nom: resArticle.value.nom,
      prix: resArticle.value.prix,
      ref: resArticle.value.reference,
      taille: selectedSize.value,
      couleur: selectedColor.value
    });

    const nouvelleLigne = {
      articleId: resArticle.value.articleId,
      nom: resArticle.value.nom,
      prix: resArticle.value.prix,
      reference: resArticle.value.reference,
      qtePanier: payload.qtePanier,
      idCouleur: payload.couleurId,
      nomCouleur: couleurObj?.nomCouleur || null, 
      idTaille: payload.tailleId,
      libelleTaille: tailleObj?.libelleTaille || null 
  };
  }
  localStorage.setItem(STORAGE_KEY, JSON.stringify(cart));
};

// const selectedColorObj = computed(() => {
//   return resVelo.value?.couleurs.find(c => c.idCouleur === selectedColor.value);
// });

// const selectedSizeObj = computed(() => {
//   return resVelo.value?.tailles.find(t => t.idTaille === selectedSize.value);
// });
</script>

<template>
  <div v-if="notFound" class="not-found-state">
    <h1>404</h1>
    <p>Oups, ce vélo est introuvable.</p>
  </div>
  <div class="page-container" v-else-if="resArticle && resVelo">
    <Landing :article="resArticle" :velo="resVelo" />
    
    <div class="selection-group" v-if="resVelo">
      <div class="option">
        <span>Couleur</span>
        <div class="btn-option">
          <button 
            v-for="c in resVelo.couleurs" 
            :key="c.idCouleur"
            :class="{ active: selectedColor === c.idCouleur }"
            @click="selectedColor = c.idCouleur"
          >
            {{ c.nomCouleur }}
          </button>
        </div>
      </div>

      <div class="option">
        <span>Taille</span>
        <div class="btn-option">
          <button 
            v-for="t in resVelo.tailles" 
            :key="t.idTaille"
            :class="{ active: selectedSize === t.idTaille }"
            @click="selectedSize = t.idTaille"
          >
            {{ t.libelleTaille }}
          </button>
        </div>
      </div>
    </div>
    <RedButton style="margin-bottom: 1rem;" @click="addInShopCart">Ajouter au panier</RedButton>
    
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
.not-found-state {
  height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
.not-found-state h1 {
  font-size: 4rem;
  margin-bottom: 1rem;
}

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

.btn-option {
  display: flex;
  gap: 1rem;
}

.option {
  display: flex;
  flex-direction: column;
  align-items: center;
  background-color: #f5f5f5;
  margin-bottom: 1rem;
  box-shadow: var(--card-shadow);
  padding: 1rem;
}

.option span {
  margin-bottom: 1rem;
  font-weight: bold;
}

.selection-group {
  display: flex;
  gap: 1rem;
}

.btn-option button {
  padding: 0.5rem 1rem;
  background-color: #fff;
  border-radius: 20px;
  border: 1px solid transparent;
  box-shadow: var(--card-shadow);
  transition: border-color 0.4s ease; 
  cursor: pointer;
}

.btn-option button:hover,
.btn-option button.active
 {
  border: 1px solid black;
}

</style>