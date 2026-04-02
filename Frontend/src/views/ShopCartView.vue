<script setup>
import LineShopCart from '@/components/LineShopCart.vue';
import RedButton from '@/components/RedButton.vue';
import { ref, computed, watch, onMounted } from 'vue';
import { useUtilsStore } from '@/stores/utils';

const utils = useUtilsStore();
const STORAGE_KEY = 'cube_shop_cart';

const lignes = ref([]);
onMounted(async () => {
  const token = localStorage.getItem('user_token');
  const localCart = JSON.parse(localStorage.getItem(STORAGE_KEY) || '[]');
  
  if (token) {
    try {
      if (localCart.length > 0) {
        for (const item of localCart) {
          await axios.post(utils.url + "LignePaniers/", {
            articleId: item.articleId,
            qtePanier: item.qtePanier,
            couleurId: item.idCouleur || item.couleurId,
            tailleId: item.idTaille || item.tailleId,
            clientId: 1 
          }, {
            headers: { 'Authorization': `Bearer ${token}` }
          });
        }
        localStorage.removeItem(STORAGE_KEY);
      }

      const response = await axios.get(utils.url + "LignePaniers/", {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      lignes.value = response.data.filter(ligne => ligne.clientId === 1);

    } 
    catch (error) {
      console.error("Erreur récuparation du panier :", error);
      lignes.value = localCart;
    }
  } 
  else {
    lignes.value = localCart;
  }
});

const nbArticles = computed(() => {
  return lignes.value.reduce((total, ligne) => total + (ligne.qtePanier || 0), 0); 
});

watch(lignes, (newCart) => {
  const token = localStorage.getItem('user_token');
  if (!token) {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(newCart));
  }
}, { deep: true });

const validerPanier = () => {
    const token = localStorage.getItem('user_token');
    if (!token) {
        alert("Veuillez vous connecter pour valider votre commande.");
    }
};
</script>

<template>
    <main>
        <div class="container-panier">
            <div v-if="lignes.length > 0">
                <h2>PANIER ({{ nbArticles }})</h2>
                <LineShopCart v-for="ligne in lignes"
                    :key="ligne.articleId + '-' + ligne.idCouleur + '-' + ligne.idTaille"
                    v-model:quantite="ligne.qtePanier"
                    :id="ligne.articleId"
                    :articleName="ligne.nom"
                    :articleRef="ligne.reference"
                    :price="ligne.prix"
                    :taille="ligne.libelleTaille"
                    :couleur="ligne.nomCouleur"
                    :image="ligne.images"
                />
            </div>
            <div v-else>
                <h2>Votre panier est vide</h2>
                <router-link to="/">Retourner à la boutique</router-link>
            </div>

            <RedButton v-if="lignes.length > 0" @click="validerPanier">
                Valider le panier
            </RedButton>
        </div> 
    </main>
</template>

<style scoped>

div > * {
    margin-bottom: 1rem;
}

h2 {
    font-weight: 400;
}

main > div > div {
    display: flex;
    flex-direction: column;
    align-items: flex-start; 
    width: 100%;
}

h2 {
    font-weight: 400;
    text-align: left; 
    width: 100%;      
    margin-top: 0;
}

.container-panier {
    margin-top: 2rem;
}
</style>