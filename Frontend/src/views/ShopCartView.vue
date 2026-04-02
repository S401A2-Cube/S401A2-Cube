<script setup>
import LineShopCart from '@/components/LineShopCart.vue';
import RedButton from '@/components/RedButton.vue';
import { ref, computed, watch, onMounted } from 'vue';
import { useUtilsStore } from '@/stores/utils';
import { useCartStore } from '@/stores/counterArticles';
import { useRouter } from 'vue-router';

const utils = useUtilsStore();
const STORAGE_KEY = 'cube_shop_cart';
const cartStore = useCartStore();
const router = useRouter();

const getAssetUrl = (path) => {
  const cleanPath = path.replace('@/', '../'); 
  return new URL(cleanPath, import.meta.url).href;
};

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
      cartStore.lignes = response.data
        .filter(ligne => ligne.clientId === 1)
        .map(ligne => {
          const images = ligne.articleLignePanier?.images;
          const chemin = (images && images.length > 0) ? images[0].chemin : "@/assets/image/fallback_bike.png";
          
          return {
            ...ligne,
            articleId: ligne.articleLignePanier.articleId,
            clientId: 1,
            qtePanier: ligne.articleLignePanier.qtePanier,
            commandeId: ligne.articleLignePanier.commandeId,
            nom: ligne.articleLignePanier.nom,
            reference: ligne.articleLignePanier.reference,
            prix: ligne.articleLignePanier.prix,
            idCouleur: ligne.articleLignePanier.couleurId,
            idTaille: ligne.articleLignePanier.tailleId,
            nomCouleur: ligne.couleurChoisie?.nomCouleur,
            libelleTaille: ligne.tailleChoisie?.libelleTaille,
            imageURL: getAssetUrl(chemin) 
          };
        });
    } 
    catch (error) {
      console.error("Erreur récuparation du panier :", error);
      cartStore.lignes = localCart;
    }
  } 
  else {
    cartStore.lignes = localCart;
  }
});

watch(() => cartStore.lignes, (newCart) => {
  const token = localStorage.getItem('user_token');
  if (!token) {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(newCart));
  }
}, { deep: true });

const validShopCart = async () => {
  const token = localStorage.getItem('user_token');
  
  if (!token) {
    localStorage.setItem('redirect_after_login', '/panier');
    router.push('/connexion');
    return;
  }

  try {
    for (const ligne of cartStore.lignes) {
      await axios.put(utils.url + "LignePaniers/" + ligne.id, {
        qtePanier: ligne.qtePanier,
      }, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
    }
    router.push('/commande');
  } catch (error) {
    console.error("Erreur mise à jour quantités :", error);
  }
};

const deleteArticle = async (infos) => {
  const token = localStorage.getItem('user_token');

  if (token && infos.ligneId) {
    try {
      await axios.delete(utils.url + "LignePaniers/" + infos.ligneId, {
        headers: { 'Authorization': `Bearer ${token}` }
      });
      cartStore.lignes = cartStore.lignes.filter(l => l.id !== infos.ligneId);
    } catch (error) {
      console.error("Erreur de suppression API", error);
    }
  } 
  
  else {
    cartStore.lignes = cartStore.lignes.filter(l => 
      !(l.articleId === infos.articleId && 
        l.idCouleur === infos.idCouleur && 
        l.idTaille === infos.idTaille)
    );
    localStorage.setItem(STORAGE_KEY, JSON.stringify(cartStore.lignes));
  }
};
</script>

<template>
    <main>
        <div class="container-panier">
            <div v-if="cartStore.lignes.length > 0">
              <h2>PANIER ({{ cartStore.nbArticles }})</h2>
              <LineShopCart v-for="ligne in cartStore.lignes"
                  :key="ligne.articleId + '-' + ligne.idCouleur + '-' + ligne.idTaille"
                  v-model:quantite="ligne.qtePanier"
                  :ligne="ligne"
                  @supprimer="deleteArticle"
              />
              <RedButton @click="validShopCart">
                Valider le panier
              </RedButton>
            </div>
            <div v-else>
                <h2>Votre panier est vide</h2>
                <router-link to="/">Retourner à la boutique</router-link>
            </div>

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