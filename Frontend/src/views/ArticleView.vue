<script setup>

// TODO:
// fetch article with id $route.params.id

const resVelo = {
  "idVelo": 1,
  "idArticle": 1,
  "lienVue360": "TODO",
  "idModele": 1,
  "modeleVelo": {
    "idModele": 1,
    "nomModele": "NULANE C:62 SLT",
  },
  "couleurs": [
    {
      "idCouleur": 1,
      "nomCouleur": "greensmoothie´n´green",
      "effetPeinture": "Non spécifié"
    }
  ],
  "tailles": [
    { "idTaille": 1, "libelleTaille": "50cm" },
    { "idTaille": 2, "libelleTaille": "53cm" },
    { "idTaille": 3, "libelleTaille": "56cm" },
    { "idTaille": 4, "libelleTaille": "59cm" },
    { "idTaille": 5, "libelleTaille": "61cm" }
  ],
  "cadres": [
    {
      "idMateriau": 1,
      "nomMat": "Carbone C:62",
      "formeCadre": "Classic / Advanced Twin Mold",
    }
  ],
  "geometries": [
    {
      "idGeometrie": 1,
      "nomPiece": "Espace libre pour les pneus",
      "taillePiece": 50,
    }
  ]
};

const resArticle = {
  "articleId": 1,
  "reference": "156500",
  "nom": "NULANE C:62 SLT",
  "description": "Le Nulane C:62 SLT combine la légèreté et le confort d’un cadre et d’une fourche tout en carbone C:62® à des composants élégants. Équipé de vitesses Apex Rival AXS 1x13, de freins à disques Magura MT4, et de roues Newmen Performance Gravel 25 avec pneus Schwalbe de 45 mm.",
  "prix": 2449,
  "poids": 8.8,
  "qteStock": 100,
  "annee": 2026,
  "dispoEnLigne": true,
  "categorieId": 2,
  "categorieArticle": {
    "categorieId": 2,
    "nom": "Gravel / Route"
  },
  "motsCles": [
    { "motCleId": 1, "nom": "Gravel" },
    { "motCleId": 2, "nom": "Carbone" },
    { "motCleId": 3, "nom": "Léger" }
  ],
};

</script>

<template>
    <main>
      <section id="landing">
        <div class="titlebar">
          <div class="titlebar_left">
            <p class="title bold">NULANE C:62</p>
            <p class="title">SLT</p>
            <p class="">PVC* 2449 EUR</p>
            <p>Référence d'article 156500 | greensmoothie´n´green</p>
          </div>
          
          <div class="titlebar_right">
            <div>
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-map-pin-icon lucide-map-pin"><path d="M20 10c0 4.993-5.539 10.193-7.399 11.799a1 1 0 0 1-1.202 0C9.539 20.193 4 14.993 4 10a8 8 0 0 1 16 0"/><circle cx="12" cy="10" r="3"/></svg>
              <p class="link">Revendeur</p>
            </div>

            <div>
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-arrow-left-right-icon lucide-arrow-left-right"><path d="M8 3 4 7l4 4"/><path d="M4 7h16"/><path d="m16 21 4-4-4-4"/><path d="M20 17H4"/></svg>
              <p class="link">Comparaison</p>
            </div>
          </div>  
        </div>
        
        <canvas>
        </canvas>
      </section>
    </main>
    
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
    
    <main id="detail">

      <section>
        <h1 class="detail_title">carde</h1>
        <li class="detail_table_row"> <!-- TODO: move this to a component -->
          <div class="aside detail_table_element">
            <p class="bold">cadre</p>
            <p v-for="c in resVelo.cadres" :key="c.idMateriau">
              {{ c.nomMat }}
              <br>
            </p>
          </div>
        </li>
        <li class="detail_table_row"> <!-- TODO: move this to a component -->
          <div class="aside detail_table_element">
            <p class="bold">taille</p>
            <div class="aside">
              <p>Classic:</p>
              <p v-for="(c,i) in resVelo.tailles" :key="c.idTaille" class="detail_taille_text">
                {{ c.libelleTaille }}
              </p>
            </div>
          </div>
        </li>
      </section>
      <section>
        <h1 class="detail_title">informations d'article</h1>
        <li class="detail_table_row"> <!-- TODO: move this to a component -->
          <div class="aside detail_table_element">
            <p class="bold">COULEUR</p>
            <p>{{ resVelo.couleurs[0].nomCouleur }}</p>
          </div>
        </li>
        <li class="detail_table_row">
          <div class="aside detail_table_element">
            <p class="bold">POIDS</p>
            <p>{{ resArticle.poids }}</p>
          </div>
        </li>
        <li class="detail_table_row">
          <div class="aside detail_table_element">
            <p class="bold">référence</p>
            <p>{{ resArticle.reference }}</p>
          </div>
        </li>
        <li class="detail_table_row">
          <div class="aside detail_table_element">
            <p class="bold">pvc*</p>
            <p>{{ resArticle.prix }} EUR</p>
          </div>
        </li>
      </section>
    </main>

</template>

<style scoped>
  section {
    width: 90vw;
  }

  p {
    text-transform: uppercase;
  }

  #landing {
    height: 100vh;

    margin-top: calc(80px + 1rem);
  }

  .titlebar{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: start;
  }

  .title {
      font-size: 50px;
  }

  .detail_title {
    font-size: 32px;
    text-transform: uppercase;
    text-align: left;
    padding-bottom: 1rem;
    border-bottom: 1px solid lightgray;
  }

  .titlebar_left * {
    text-align: left;
  }

  .titlebar_right {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: row;
    gap: 2rem;
  }

  .titlebar_right > div {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: row;
    gap: 0.5rem;
  }

  canvas {
    width: 100%;
  }

  li {
    list-style: none;
  }

  .aside {
    display: flex;
    gap: 1rem;
    flex-direction: row;
  }

  .detail_table_element > *:nth-child(1) {
    width: 20%;
  }
  
  .detail_table_element > *:nth-child(2) {
    width: 80%;
  }
  
  .detail_table_element > * {
    text-align: left;
  }

  .detail_table_row {
    padding: 1rem;
    border-bottom: 1px solid lightgray;
  }

  #detail {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    gap: 4rem;
  }
  .detail_taille_text {
    text-transform: none;
  }

/* --- About Section Styles --- */
#about {
    padding: 100px 0;
    display: flex;
    justify-content: center;
    background-color: #f5f5f5; /* Light grey contrast */
    width: 100%;
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
    text-transform: none; /* Descriptions shouldn't be all caps */
    margin-bottom: 2rem;
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

  canvas {
    background-color: black;
  }
</style>