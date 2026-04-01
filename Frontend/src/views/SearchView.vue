<script setup>
import { ref, reactive, computed, onMounted } from "vue";
import FiltrePane from "@/components/FiltrePane.vue";
import Article from '@/components/Article.vue';
import axios from 'axios';
import { useUtilsStore } from '@/stores/utils';

const getAssetUrl = (path) => {
  const cleanPath = path.replace('@/', '../'); 
  return new URL(cleanPath, import.meta.url).href;
};

const isLoading = ref(true);
const utils = useUtilsStore();

const couleurs = ref([]);
const millesimes = ref([]);
const tailles = ref([]);
const cadres = ref([]);
const velos = ref([]);

onMounted(() => {
  Promise.all([
    axios.get(utils.url + "Couleurs"),
    axios.get(utils.url + "Millesimes"),
    axios.get(utils.url + "Tailles"),
    axios.get(utils.url + "Cadres"),
    axios.get(utils.url + "Velos")
  ]).then(([resCouleurs, resMillesimes, resTailles, resCadres, resVelos]) => {
    
    couleurs.value = resCouleurs.data;
    millesimes.value = resMillesimes.data;
    tailles.value = resTailles.data;
    cadres.value = resCadres.data;
    
    velos.value = resVelos.data.map(velo => ({
      idVelo: velo.idVelo,
      idArticle: velo.idArticle,
      lienVue360: velo.lienVue360 || null,
      idModele: velo.idModele,
      
      modeleVelo: {
        idModele: velo.modeleVelo.idModele,
        nomModele: velo.article.nom, 
        categorieVelo: velo.article.categorieArticle.nom,
        prix: velo.article.prix,
        lienImage: velo.article.images && velo.article.images.length > 0 
          ? getAssetUrl(velo.article.images[0].chemin)
          : getAssetUrl("@/assets/image/fallback_bike.png")
      },
      couleurs: velo.couleurs.map(c => ({
        idCouleur: c.idCouleur,
        nomCouleur: c.nomCouleur,
        effetPeinture: c.effetPeinture
      })),
      tailles: velo.tailles.map(t => ({
        idTaille: t.idTaille,
        libelleTaille: t.libelleTaille
      })),
      cadres: velo.cadres.map(c => ({
        idMateriau: c.idMateriau,
        nomMat: c.nomMat,
        formeCadre: c.formeCadre
      })),
      millesimes: velo.millesimes.map(m => ({
        idMillesime: m.idMillesime,
        annee: m.annee
      })),
      geometries: velo.geometries.map(g => ({
        idGeometrie: g.idGeometrie,
        libelleGeometrie: g.nomPiece 
      }))
    }));

  }).catch(error => {
    console.error("Erreur lors du chargement des données:", error);
  }).finally(() => {
    isLoading.value = false;
  });
});

const getUnique = (arr, key) => {
  const map = new Map();
  arr.forEach(item => {
    if (!map.has(item[key])) map.set(item[key], item);
  });
  return Array.from(map.values());
};

const selectedFilters = reactive({
  tailles: [],
  cadres: [],
  formes: [],
  couleurs: [],
  millesimes: []
});

const filtersConfig = computed(() => [
  {
    id: 'tailles',
    name: 'Tailles',
    values: tailles.value,
    getLabel: (v) => v.libelleTaille,
    matchOption: (bike, option) => bike.tailles.some(t => t.idTaille === option.idTaille)
  },
  {
    id: 'cadres',
    name: 'Matériaux Cadre',
    values: getUnique(cadres.value, 'nomMat'),
    getLabel: (v) => v.nomMat,
    matchOption: (bike, option) => bike.cadres.some(c => c.nomMat === option.nomMat)
  },
  {
    id: 'formes',
    name: 'Forme Cadre',
    values: getUnique(cadres.value, 'formeCadre'),
    getLabel: (v) => v.formeCadre,
    matchOption: (bike, option) => bike.cadres.some(c => c.formeCadre === option.formeCadre)
  },
  {
    id: 'couleurs',
    name: 'Couleurs',
    values: couleurs.value,
    getLabel: (v) => v.nomCouleur,
    matchOption: (bike, option) => bike.couleurs.some(c => c.idCouleur === option.idCouleur)
  },
  {
    id: 'millesimes',
    name: 'Millésime',
    values: millesimes.value,
    getLabel: (v) => v.annee,
    matchOption: (bike, option) => bike.millesimes.some(m => m.idMillesime === option.idMillesime)
  }
]);

const bikesFiltres = computed(() => {
  return velos.value.filter((bike) => {
    return filtersConfig.value.every((filter) => {
      const selections = selectedFilters[filter.id];
      if (!selections || selections.length === 0) return true; 
      
      return selections.some((option) => filter.matchOption(bike, option));
    });
  });
});
</script>

<template>
  <div class="center">
    <h1 class="title">Découvrez notre sélection</h1>
    <div v-if="isLoading" class="loading-state">
      <p>Chargement des vélos en cours...</p>
    </div>
    <main>
        <div id="side-filter">
            <FiltrePane
                class="filter"
                v-for="filter in filtersConfig"
                :key="filter.id"
                :name="filter.name"
                :values="filter.values"
                :bikes="velos"
                :get-label="filter.getLabel"
                :match-option="filter.matchOption"
                v-model="selectedFilters[filter.id]"
            />
        </div>

        <div class="grid">
            <Article 
                v-for="velo in bikesFiltres" :key="velo.idVelo" class="article"
                :id=velo.idVelo
                :article-name=velo.modeleVelo.nomModele
                :available-online="true"
                :price=velo.modeleVelo.prix
                :image-u-r-l=velo.modeleVelo.lienImage
            />
        </div>
    </main>
  </div>
</template>

<style scoped>
main
{
  width: 80%;
  display: flex;
  flex-direction: row;
  justify-self: center;
  align-items: start;
  gap: 1rem;
}

.filter:not(:last-child) {
  border-bottom: 1px solid #d4d4d4;
}

.title
{
	font-size: 2.5rem;
	font-style: italic;
	font-weight: 700;
	text-transform: uppercase;
	margin: 6.25rem auto 4.375rem;
}

#side-filter
{
  width: 20vw;
  border: 1px solid #d7d7d7;
  -webkit-box-shadow: 0 4px 17px 0 rgba(0,0,0,.14);
  box-shadow: 0 4px 17px 0 rgba(0,0,0,.14);
  margin-bottom: 60px;
  margin-right: 30px;
  padding: 30px;
}

.grid
{
    width: 75%;
    display: flex;
    flex-wrap: wrap;
    gap: 1rem;
}

.article
{
  width: calc(33% - .5rem);
}

</style>