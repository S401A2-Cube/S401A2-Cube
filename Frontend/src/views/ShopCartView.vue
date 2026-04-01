<script setup>
import LineShopCart from '@/components/LineShopCart.vue';
import RedButton from '@/components/RedButton.vue';
import { ref, computed, watch, onMounted } from 'vue';

// const fakeLignes = ref([
//     {
//         id: 10, 
//         name: "Aim Race", 
//         prix: 799,
//         lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-Race_grey_n_red.png",
//         couleur: "bleu",
//         taille: "XS",
//         ref: "GCEHDG159",
//         qte: 2
//     },
//     {
//         id: 11, 
//         name: "Tour EXC", 
//         prix: 649,
//         lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Tour-EXC_flashgrey_n_black.png",
//         couleur: "bleu",
//         taille: "XS",
//         ref: "HZHH7892144",
//         qte: 3
//     },
//     {
//         id: 12, 
//         name: "Nuroad Race", 
//         prix: 1099,
//         lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Nuroad-Race_grey_n_metal.png",
//         couleur: "bleu",
//         taille: "XS",
//         ref: "GCEDYHSZ8726",
//         qte: 1
//     },
//     {
//         id: 13, 
//         name: "Travel SLX", 
//         prix: 1199,
//         lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Travel-SLX_grey_n_black.png",
//         couleur: "bleu",
//         taille: "XS",
//         ref: "AONFV973641",
//         qte: 1
//     },
// ]);

const STORAGE_KEY = 'cube_shop_cart';
const fakeLignes = ref(JSON.parse(localStorage.getItem(STORAGE_KEY) || '[]'));
const nbArticles = computed(() => {
  return fakeLignes.value.reduce((total, ligne) => total + (ligne.qtePanier), 0); 
});

watch(fakeLignes, (newCart) => {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(newCart));
}, { deep: true });

const validerPanier = () => {
    const token = localStorage.getItem('user_token');
    if (!token) {
        alert("Veuillez vous connecter pour valider votre commande.");
        // router.push('/login');
    } else {
        console.log("Envoi du panier à l'API...", fakeLignes.value);
    }
};

// const nbArticles = computed(() => {
//   return fakeLignes.value.reduce((total, ligne) => {
//     return total + ligne.qte;
//   }, 0); 
// });

</script>

<template>
    <main>
        <div class="container-panier">
            <div v-if="fakeLignes.length > 0">
                <h2>PANIER ({{ nbArticles }})</h2>
                <LineShopCart v-for="ligne in fakeLignes"
                    :key="ligne.id"
                    v-model:quantite="ligne.qtePanier"
                    :id="ligne.articleId"
                    :articleName="ligne.nom"
                    :articleRef="ligne.ref"
                    :price="ligne.prix"
                    :taille="ligne.libelleTaille"
                    :couleur="ligne.nomCouleur"
                />
            </div>
            <div v-else>
                <h2>Votre panier est vide</h2>
                <router-link to="/">Retourner à la boutique</router-link>
            </div>

            <RedButton v-if="fakeLignes.length > 0" @click="validerPanier">
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