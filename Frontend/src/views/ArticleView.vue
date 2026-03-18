<script setup>
import { ref } from 'vue';
import Landing from '../components/article/Landing.vue';
import Specifications from '../components/article/Specifications.vue';

// TODO: fetch article with id $route.params.id
const resVelo = ref({
  "idVelo": 1,
  "idArticle": 1,
  "lienVue360": "TODO",
  "idModele": 1,
  "modeleVelo": { "idModele": 1, "nomModele": "NUROAD C:62 SLT" },
  "couleurs": [
    { "idCouleur": 1, "nomCouleur": "greensmoothie´n´green", "effetPeinture": "Mat" }
  ],
  "tailles": [
    { "idTaille": 1, "libelleTaille": "50cm" },
    { "idTaille": 2, "libelleTaille": "53cm" },
    { "idTaille": 3, "libelleTaille": "56cm" },
    { "idTaille": 4, "libelleTaille": "59cm" },
    { "idTaille": 5, "libelleTaille": "61cm" }
  ],
  "cadres": [
    { "idMateriau": 1, "nomMat": "Carbone C:62", "formeCadre": "Classic / Advanced Twin Mold" }
  ],
  "geometries": [
    { "idGeometrie": 1, "nomPiece": "Espace libre pour les pneus", "taillePiece": 50 }
  ]
});

const resArticle = ref({
  "articleId": 1,
  "reference": "156500",
  "nom": "NUROAD C:62 SLT",
  "description": "Le Nuroad C:62 SLT combine la légèreté et le confort d’un cadre et d’une fourche tout en carbone C:62® à des composants élégants. Équipé de vitesses Apex Rival AXS 1x13, de freins à disques Magura MT4, et de roues Newmen Performance Gravel 25 avec pneus Schwalbe de 45 mm.",
  "prix": 2449,
  "poids": 8.8,
  "qteStock": 100,
  "annee": 2026,
  "dispoEnLigne": true,
  "categorieId": 2,
  "categorieArticle": { "categorieId": 2, "nom": "Gravel / Route" },
  "motsCles": [
    { "motCleId": 1, "nom": "Gravel" },
    { "motCleId": 2, "nom": "Carbone" },
    { "motCleId": 3, "nom": "Léger" }
  ]
});
</script>

<template>
  <div class="page-container">
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
</template>

<style scoped>
.page-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
}

/* About Section Global Styles */
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