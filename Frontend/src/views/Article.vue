<script setup>
import { ref, computed, onMounted } from "vue";


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
    idVelo: 1, idArticle: 101, lienVue360: null, idModele: 1,
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
    idVelo: 2, idArticle: 102, lienVue360: null, idModele: 2,
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
    idVelo: 3, idArticle: 103, lienVue360: null, idModele: 3,
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
    idVelo: 4, idArticle: 104, lienVue360: null, idModele: 4,
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
    idVelo: 5, idArticle: 105, lienVue360: null, idModele: 5,
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
    idVelo: 6, idArticle: 106, lienVue360: null, idModele: 6,
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
    idVelo: 7, idArticle: 107, lienVue360: null, idModele: 7,
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
    idVelo: 8, idArticle: 108, lienVue360: null, idModele: 8,
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
const tailles = ref([]);
const cadres = ref([]);
const couleurs = ref([]);
const millesimes = ref([]);



onMounted(() => {
  bikes.value = fakeVelos;
  tailles.value = fakeTailles;
  cadres.value = fakeCadres;
  couleurs.value = fakeCouleurs;
  millesimes.value = fakeMillesimes;

});

const taillesSelectionnees = ref([]);

const selectTaille = (taille) => {
  if (!taillesSelectionnees.value.includes(taille)) {
    taillesSelectionnees.value.push(taille);
  } else {
    taillesSelectionnees.value.splice(taillesSelectionnees.value.indexOf(taille), 1);
  }
};

const MatsSelectionnees = ref([]);

const selectMat = (mat) => {
  if (!MatsSelectionnees.value.includes(mat)) {
    MatsSelectionnees.value.push(mat);
  } else {
    MatsSelectionnees.value.splice(MatsSelectionnees.value.indexOf(mat), 1);
  }
};

const FormesSelectionnees = ref([]);

const selectForme = (forme) => {
  if (!FormesSelectionnees.value.includes(forme)) {
    FormesSelectionnees.value.push(forme);
  } else {
    FormesSelectionnees.value.splice(FormesSelectionnees.value.indexOf(forme), 1);
  }
};

const couleursSelectionnees   = ref([])
const millesimesSelectionnes  = ref([])

const selectCouleur = (couleur) => {
  if (!couleursSelectionnees.value.includes(couleur))
    couleursSelectionnees.value.push(couleur)
  else
    couleursSelectionnees.value.splice(couleursSelectionnees.value.indexOf(couleur), 1)
}

const selectMillesime = (mil) => {
  if (!millesimesSelectionnes.value.includes(mil))
    millesimesSelectionnes.value.push(mil)
  else
    millesimesSelectionnes.value.splice(millesimesSelectionnes.value.indexOf(mil), 1)
}

const bikesFiltres = computed(() =>
  bikes.value.filter(
    (velo) =>
      (taillesSelectionnees.value.length == 0 ||
        taillesSelectionnees.value
          .map((t) => t.idTaille)
          .some((id) => velo.tailles.map((t) => t.idTaille).includes(id))) &&
      (MatsSelectionnees.value.length == 0 ||
        MatsSelectionnees.value
          .map((m) => m.nomMat)
          .some((nom) => velo.cadres.map((c) => c.nomMat).includes(nom))) &&
      (FormesSelectionnees.value.length == 0 ||
        FormesSelectionnees.value
          .map((f) => f.formeCadre)
          .some((nom) => velo.cadres.map((f) => f.formeCadre).includes(nom))) &&
      (
        couleursSelectionnees.value.length == 0 ||
        couleursSelectionnees.value
        .map(c => c.idCouleur)
        .some(id => velo.couleurs.map(c => c.idCouleur).includes(id))) &&  
        (
      millesimesSelectionnes.value.length == 0
      ||
      millesimesSelectionnes.value
        .map(m => m.idMillesime)
        .some(id => velo.millesimes.map(m => m.idMillesime).includes(id)))

  )
);

const MatsDejaVu = computed(() => {
const NomDejaVue = [];

  return cadres.value.filter((cadres) => {
    if (NomDejaVue.includes(cadres.nomMat)) {
      return false;
    } else {
      NomDejaVue.push(cadres.nomMat);

      return true;
    }
  });
});

const filtres = ["Tailles", "Matériaux Cadre", "Forme Cadre", "Millésime", "Couleurs"];

const selectedIndex = ref(null);

const toggleRotation = (index) => {
  selectedIndex.value = selectedIndex.value === index ? null : index;
};
</script>

<template>
  <div id="Side_Filtre">
    <div v-for="(titre, index) in filtres" :key="titre">
      <div class="side_filtre" @click="toggleRotation(index)">
        <h2>{{ titre }}</h2>

        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
          class="chevron"
          :class="{ rotated: selectedIndex === index }"
        >
          <path d="m18 15-6-6-6 6" />
        </svg>
      </div>

      <div v-if="titre === 'Tailles' && selectedIndex === index" class="filtre-panel">
        <span
          v-for="taille in tailles"
          :key="taille.idTaille"
          class="taille-btn"
          :class="{ selected: taillesSelectionnees.includes(taille) }"
          @click.stop="selectTaille(taille)"
        >
          {{ taille.libelleTaille }}
        </span>
      </div>

      <div
        v-if="titre === 'Matériaux Cadre' && selectedIndex === index"
        class="filtre-panel"
      >
        <span
          v-for="mat in MatsDejaVu"
          :key="mat.idMateriau"
          class="mat-btn"
          :class="{ selected: MatsSelectionnees.includes(mat) }"
          @click.stop="selectMat(mat)"
        >
          {{ mat.nomMat }}
        </span>
      </div>

      <div v-if="titre === 'Forme Cadre' && selectedIndex === index" class="filtre-panel">
        <span
          v-for="forme in cadres"
          :key="cadres.idMateriau"
          class="mat-btn"
          :class="{ selected: FormesSelectionnees.includes(forme) }"
          @click.stop="selectForme(forme)"
        >
          {{ forme.formeCadre }}
        </span>
      </div>

    <div v-if="titre === 'Couleurs' && selectedIndex === index" class="filtre-panel">
      <span
        v-for="couleur in couleurs"
        :key="couleur.idCouleur"
        class="taille-btn"
        :class="{ selected: couleursSelectionnees.includes(couleur) }"
        @click.stop="selectCouleur(couleur)"
      >
        {{ couleur.nomCouleur }}
      </span>
    </div>

    <div v-if="titre === 'Millésime' && selectedIndex === index" class="filtre-panel">
      <span
        v-for="mil in millesimes"
        :key="mil.idMillesime"
        class="taille-btn"
        :class="{ selected: millesimesSelectionnes.includes(mil) }"
        @click.stop="selectMillesime(mil)"
      >
        {{ mil.annee }}
      </span>
    </div>
    </div>
  </div>

  <div id="articleContainer">
    <h1>Découvrez notre sélection</h1>
    <div class="grid">
      <div v-for="velo in bikesFiltres" :key="velo.idVelo" class="article">
        <div class="article-img">
          <img :src="velo.modeleVelo.lienImage" :alt="velo.modeleVelo.nomModele" />
        </div>
        <div class="article-body">
          <h1>{{ velo.modeleVelo.categorieVelo }}</h1>
          <h2>{{ velo.modeleVelo.nomModele }}</h2>
          <p class="prix">{{ velo.modeleVelo.prix }} €</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
#Side_Filtre {
  outline: 0.5px solid black;
  position: absolute;
  left: 50px;
}

.side_filtre {
  display: flex;
  align-items: center;
  justify-content: space-between;
  cursor: pointer;
  padding: 5px 0;
}

#Side_Filtre h2 {
  font-weight: bold;
}

.chevron {
  transition: transform 0.5s ease;
}
.rotated {
  transform: rotate(180deg);
}

.filtre-panel {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  padding: 10px 0 14px;
}

.taille-btn {
  border: 1.5px solid #ccc;
  border-radius: 4px;
  padding: 4px 10px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  user-select: none;
  transition: all 0.15s;
}

.taille-btn:hover {
  border-color: #e63000;
  color: #e63000;
}

.taille-btn.selected {
  background: #e63000;
  border-color: #e63000;
  color: #fff;
}

#articleContainer {
  margin-left: 250px;
  padding: 32px;
}

#articleContainer h1 {
  font-size: 28px;
  font-weight: 800;
  margin-bottom: 32px;
  color: #111;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
  gap: 24px;
}

.article {
  background: #fff;
  border-radius: 6px;
  overflow: hidden;
  transition: box-shadow 0.2s, transform 0.2s;
  cursor: pointer;
}

.article:hover {
  box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
  transform: translateY(-4px);
}

.article-img {
  height: 200px;
  background: #f0f0f0;
  overflow: hidden;
}

.article-img img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.4s ease;
}

.article:hover .article-img img {
  transform: scale(1.05);
}

.article-body {
  padding: 16px 20px 20px;
}

.article-body h1 {
  font-size: 11px;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 2px;
  color: #e63000;
  margin-bottom: 6px;
}

.article-body h2 {
  font-size: 18px;
  font-weight: 700;
  color: #111;
  margin-bottom: 10px;
}

.prix {
  font-size: 16px;
  font-weight: 600;
  color: #333;
}

.mat-btn {
  border: 1.5px solid #ccc;
  border-radius: 4px;
  padding: 4px 10px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  user-select: none;
  transition: all 0.15s;
}

.mat-btn:hover {
  border-color: #e63000;
  color: #e63000;
}

.mat-btn.selected {
  background: #e63000;
  border-color: #e63000;
  color: #fff;
}
</style>
