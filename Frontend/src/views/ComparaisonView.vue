<script setup>
import { useComparaisonStore } from '@/stores/comparaison'

const store = useComparaisonStore()

const specGroups = [
  {
    label: 'Général',
    spec: [
      { label: 'Catégorie ', func: (article, velo) => article.categorieArticle.nom },
      { label: 'Prix ',      func: (article, velo) => article.prix + ' EUR' },
      { label: 'Poids ',     func: (article, velo) => article.poids + ' kg' },
      { label: 'Mots-clés ', func: (article, velo) => article.motsCles.map(m => m.nom).join(', ') },
    ]
  },
  {
    label: 'Cadre',
    spec: [
      { label: 'Matériau ', func: (article, velo) => velo.cadres.map(c => c.nomMat).join(', ') },
      { label: 'Forme ',    func: (article, velo) => velo.cadres.map(c => c.formeCadre).join(', ') },
    ]
  },
  {
    label: 'Tailles ',
    spec: [
      { label: 'Disponibles ', func: (article, velo) => velo.tailles.map(t => t.libelleTaille).join(', ') },
    ]
  },
  {
    label: 'Couleurs ',
    spec: [
      { label: 'Couleur ', func: (article, velo) => velo.couleurs.map(c => c.nomCouleur).join(', ') },
      { label: 'Effet',   func: (article, velo) => velo.couleurs.map(c => c.effetPeinture).join(', ') },
    ]
  },
  {
    label: 'Géométrie ',
    spec: [
      { label: 'Pièce ',  func: (article, velo) => velo.geometries.map(g => g.nomPiece).join(', ') },
      { label: 'Taille ', func: (article, velo) => velo.geometries.map(g => g.taillePiece + 'mm').join(', ') },
    ]
  },
]
</script>

<template>
    <div class="comparaison-page">
        <h1>Comparer les modèles</h1>

        <div class="colonnes">
            <div v-for="item in store.items" :key="item.article.articleId" class="colonne">
                <p>{{ item.article.nom }}</p>
            </div>
        </div>

        <div v-for="group in specGroups" :key="group.label" class="group">
            <h2>{{ group.label }}</h2>

            <div v-for="spec in group.spec" :key="spec.label" class="spec-row">
                <span class="spec-label">{{ spec.label }}</span>

                <span v-for="item in store.items" :key="item.article.articleId">
                {{ spec.func(item.article, item.velo) }}
                </span>
            </div>
</div>
    </div>
</template>

<style scoped>
.comparaison-page {
  max-width: 1200px;
  margin: 120px auto 4rem auto;
  padding: 0 2rem;
}

h1 {
  font-size: 3rem;
  text-transform: uppercase;
  font-weight: 900;
  margin-bottom: 3rem;
}

.colonnes {
  display: grid;

  margin-bottom: 1rem;
}

.colonne {
  text-align: center;
  font-weight: bold;
  font-size: 1rem;
  text-transform: uppercase;
  padding: 1rem;
}

.group {
  border-top: 2px solid black;
  margin-bottom: 0;
}

h2 {
  font-size: 0.75rem;
  text-transform: uppercase;
  letter-spacing: 2px;
  padding: 1rem 0;
  font-weight: 700;
}



.spec-label {
  font-size: 0.8rem;
  text-transform: uppercase;
  font-weight: 700;
  color: #666;
  padding-right: 1rem;
}




</style>