<script setup>
import LineShopCart from '@/components/LineShopCart.vue';
import RedButton from '@/components/RedButton.vue';
import { ref, computed } from 'vue';

const fakeLignes = ref([
    {
        id: 10, 
        name: "Aim Race", 
        prix: 799,
        lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-Race_grey_n_red.png",
        couleur: "bleu",
        taille: "XS",
        ref: "GCEHDG159",
        qte: 2
    },
    {
        id: 11, 
        name: "Tour EXC", 
        prix: 649,
        lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Tour-EXC_flashgrey_n_black.png",
        couleur: "bleu",
        taille: "XS",
        ref: "HZHH7892144",
        qte: 3
    },
    {
        id: 12, 
        name: "Nuroad Race", 
        prix: 1099,
        lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Nuroad-Race_grey_n_metal.png",
        couleur: "bleu",
        taille: "XS",
        ref: "GCEDYHSZ8726",
        qte: 1
    },
    {
        id: 13, 
        name: "Travel SLX", 
        prix: 1199,
        lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Travel-SLX_grey_n_black.png",
        couleur: "bleu",
        taille: "XS",
        ref: "AONFV973641",
        qte: 1
    },
]);

const nbArticles = computed(() => {
  return fakeLignes.value.reduce((total, ligne) => {
    return total + ligne.qte;
  }, 0); 
});

</script>

<template>
    <main>
        <div class="container-panier">
            <div>
                <h2>PANIER ({{ nbArticles }})</h2>
                <LineShopCart v-for="ligne in fakeLignes"
                    :key="ligne.id"
                    v-model:quantite="ligne.qte"
                    :id="ligne.id"
                    :articleName="ligne.name"
                    :articleRef="ligne.ref"
                    :price="ligne.prix"
                    :taille="ligne.taille"
                    :couleur="ligne.couleur"
                />
            </div>
            <RedButton type="submit">Valider le panier</RedButton>
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