<script setup>
import { ref, reactive, computed, onMounted } from "vue";
import FiltrePane from "@/components/FiltrePane.vue";
import Article from '@/components/Article.vue';

const fakeCouleurs = [
  { idCouleur: 1, nomCouleur: "Gris / Rouge",     effetPeinture: "Mat",      velos: null },
  { idCouleur: 2, nomCouleur: "Rouge / Rouge",     effetPeinture: "Brillant", velos: null },
  { idCouleur: 3, nomCouleur: "Carbone / Blanc",   effetPeinture: "Mat",      velos: null },
  { idCouleur: 4, nomCouleur: "Gris / Métal",      effetPeinture: "Métal",    velos: null },
  { idCouleur: 5, nomCouleur: "Gris Flash / Noir", effetPeinture: "Brillant", velos: null },
  { idCouleur: 6, nomCouleur: "Gris / Noir",       effetPeinture: "Mat",      velos: null },
]

const fakeMillesimes = [
  { idMillesime: 1, annee: 2022, description: "Édition 2022" },
  { idMillesime: 2, annee: 2023, description: "Édition 2023" },
  { idMillesime: 3, annee: 2024, description: "Édition 2024" },
  { idMillesime: 4, annee: 2025, description: "Édition 2025" },
]

const fakeTailles = [
  { idTaille: 1, libelleTaille: "XS", velos: null },
  { idTaille: 2, libelleTaille: "S", velos: null },
  { idTaille: 3, libelleTaille: "M", velos: null },
  { idTaille: 4, libelleTaille: "L", velos: null },
  { idTaille: 5, libelleTaille: "XL", velos: null },
  { idTaille: 6, libelleTaille: "XXL", velos: null },
];

const fakeCadres = [
  { idMateriau: 1, nomMat: "Aluminium", formeCadre: "VTT", velos: null },
  { idMateriau: 2, nomMat: "Aluminium", formeCadre: "Diamant", velos: null },
  { idMateriau: 3, nomMat: "Aluminium", formeCadre: "Trapèze", velos: null },
  { idMateriau: 4, nomMat: "Aluminium", formeCadre: "Wave", velos: null },
  { idMateriau: 5, nomMat: "Carbone", formeCadre: "Diamant", velos: null },
];

const fakeVelos = [
  {
    idVelo: 10, idArticle: 101, lienVue360: null, idModele: 1,
    modeleVelo: {
      idModele: 1, nomModele: "Aim Race", categorieVelo: "Mountainbike", prix: 799,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-Race_grey_n_red.png"
    },
    couleurs:   [{ idCouleur: 1, nomCouleur: "Gris / Rouge",    effetPeinture: "Mat" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 1, nomMat: "Aluminium", formeCadre: "VTT" }],
    millesimes: [{ idMillesime: 2, annee: 2023 }, { idMillesime: 3, annee: 2024 }],
    geometries: [{ idGeometrie: 1, libelleGeometrie: "Standard" }]
  },
  {
    idVelo: 11, idArticle: 102, lienVue360: null, idModele: 2,
    modeleVelo: {
      idModele: 2, nomModele: "Aim SL", categorieVelo: "Mountainbike", prix: 999,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-SL_red_n_red.png"
    },
    couleurs:   [{ idCouleur: 2, nomCouleur: "Rouge / Rouge",   effetPeinture: "Brillant" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 1, nomMat: "Aluminium", formeCadre: "VTT" }],
    millesimes: [{ idMillesime: 2, annee: 2023 }, { idMillesime: 3, annee: 2024 }],
    geometries: [{ idGeometrie: 1, libelleGeometrie: "Standard" }]
  },
  {
    idVelo: 12, idArticle: 103, lienVue360: null, idModele: 3,
    modeleVelo: {
      idModele: 3, nomModele: "Attain GTC Pro", categorieVelo: "Road", prix: 1299,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Attain-GTC-Pro_carbon_n_white.png"
    },
    couleurs:   [{ idCouleur: 3, nomCouleur: "Carbone / Blanc", effetPeinture: "Mat" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 5, nomMat: "Carbone", formeCadre: "Diamant" }],
    millesimes: [{ idMillesime: 3, annee: 2024 }, { idMillesime: 4, annee: 2025 }],
    geometries: [{ idGeometrie: 2, libelleGeometrie: "Compétition" }]
  },
  {
    idVelo: 14, idArticle: 104, lienVue360: null, idModele: 4,
    modeleVelo: {
      idModele: 4, nomModele: "Agree C:62", categorieVelo: "Road", prix: 2499,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Agree-C62-Race_carbon_n_white.png"
    },
    couleurs:   [{ idCouleur: 3, nomCouleur: "Carbone / Blanc", effetPeinture: "Mat" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 5, nomMat: "Carbone", formeCadre: "Diamant" }],
    millesimes: [{ idMillesime: 3, annee: 2024 }, { idMillesime: 4, annee: 2025 }],
    geometries: [{ idGeometrie: 2, libelleGeometrie: "Compétition" }]
  },
  {
    idVelo: 15, idArticle: 105, lienVue360: null, idModele: 5,
    modeleVelo: {
      idModele: 5, nomModele: "Nuroad Race", categorieVelo: "Gravel", prix: 1099,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Nuroad-Race_grey_n_metal.png"
    },
    couleurs:   [{ idCouleur: 4, nomCouleur: "Gris / Métal",    effetPeinture: "Métal" }],
    tailles:    [{ idTaille: 2, libelleTaille: "S"  }, { idTaille: 3, libelleTaille: "M" },
                 { idTaille: 4, libelleTaille: "L"  }, { idTaille: 5, libelleTaille: "XL" },
                 { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 2, nomMat: "Aluminium", formeCadre: "Diamant" }],
    millesimes: [{ idMillesime: 2, annee: 2023 }, { idMillesime: 3, annee: 2024 }],
    geometries: [{ idGeometrie: 1, libelleGeometrie: "Standard" }]
  },
  {
    idVelo: 16, idArticle: 106, lienVue360: null, idModele: 6,
    modeleVelo: {
      idModele: 6, nomModele: "Cross Race", categorieVelo: "Gravel", prix: 899,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Cross-Race_grey_n_red.png"
    },
    couleurs:   [{ idCouleur: 1, nomCouleur: "Gris / Rouge",    effetPeinture: "Mat" }],
    tailles:    [{ idTaille: 2, libelleTaille: "S"  }, { idTaille: 3, libelleTaille: "M" },
                 { idTaille: 4, libelleTaille: "L"  }, { idTaille: 5, libelleTaille: "XL" },
                 { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 2, nomMat: "Aluminium", formeCadre: "Diamant" }],
    millesimes: [{ idMillesime: 2, annee: 2023 }, { idMillesime: 3, annee: 2024 }],
    geometries: [{ idGeometrie: 1, libelleGeometrie: "Standard" }]
  },
  {
    idVelo: 17, idArticle: 107, lienVue360: null, idModele: 7,
    modeleVelo: {
      idModele: 7, nomModele: "Tour EXC", categorieVelo: "Trekking", prix: 649,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Tour-EXC_flashgrey_n_black.png"
    },
    couleurs:   [{ idCouleur: 5, nomCouleur: "Gris Flash / Noir", effetPeinture: "Brillant" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 3, nomMat: "Aluminium", formeCadre: "Trapèze" }],
    millesimes: [{ idMillesime: 1, annee: 2022 }, { idMillesime: 2, annee: 2023 }],
    geometries: [{ idGeometrie: 3, libelleGeometrie: "Confort" }]
  },
  {
    idVelo: 18, idArticle: 108, lienVue360: null, idModele: 8,
    modeleVelo: {
      idModele: 8, nomModele: "Travel SLX", categorieVelo: "Trekking", prix: 1199,
      lienImage: "https://images.cube.eu/2024/bikes/2024_CUBE_Travel-SLX_grey_n_black.png"
    },
    couleurs:   [{ idCouleur: 6, nomCouleur: "Gris / Noir",     effetPeinture: "Mat" }],
    tailles:    [{ idTaille: 1, libelleTaille: "XS" }, { idTaille: 2, libelleTaille: "S" },
                 { idTaille: 3, libelleTaille: "M"  }, { idTaille: 4, libelleTaille: "L" },
                 { idTaille: 5, libelleTaille: "XL" }, { idTaille: 6, libelleTaille: "XXL" }],
    cadres:     [{ idMateriau: 4, nomMat: "Aluminium", formeCadre: "Wave" }],
    millesimes: [{ idMillesime: 1, annee: 2022 }, { idMillesime: 2, annee: 2023 }],
    geometries: [{ idGeometrie: 3, libelleGeometrie: "Confort" }]
  },
]

const bikes = ref([]);

onMounted(() => {
  bikes.value = fakeVelos;
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
    values: fakeTailles,
    getLabel: (v) => v.libelleTaille,
    matchOption: (bike, option) => bike.tailles.some(t => t.idTaille === option.idTaille)
  },
  {
    id: 'cadres',
    name: 'Matériaux Cadre',
    values: getUnique(fakeCadres, 'nomMat'),
    getLabel: (v) => v.nomMat,
    matchOption: (bike, option) => bike.cadres.some(c => c.nomMat === option.nomMat)
  },
  {
    id: 'formes',
    name: 'Forme Cadre',
    values: getUnique(fakeCadres, 'formeCadre'),
    getLabel: (v) => v.formeCadre,
    matchOption: (bike, option) => bike.cadres.some(c => c.formeCadre === option.formeCadre)
  },
  {
    id: 'couleurs',
    name: 'Couleurs',
    values: fakeCouleurs,
    getLabel: (v) => v.nomCouleur,
    matchOption: (bike, option) => bike.couleurs.some(c => c.idCouleur === option.idCouleur)
  },
  {
    id: 'millesimes',
    name: 'Millésime',
    values: fakeMillesimes,
    getLabel: (v) => v.annee,
    matchOption: (bike, option) => bike.millesimes.some(m => m.idMillesime === option.idMillesime)
  }
]);

const bikesFiltres = computed(() => {
  return bikes.value.filter((bike) => {
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
    <main>
        <div id="side-filter">
            <FiltrePane
                class="filter"
                v-for="filter in filtersConfig"
                :key="filter.id"
                :name="filter.name"
                :values="filter.values"
                :bikes="bikes"
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