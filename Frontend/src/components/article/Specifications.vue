<script setup>
import DetailRow from './DetailRow.vue';

defineProps({
  article: { type: Object, required: true },
  velo: { type: Object, required: true }
});
</script>

<template>
  <main id="detail">
    <section>
      <h1 class="detail_title">Cadre & Tailles</h1>
      
      <DetailRow label="Cadre">
        <div v-for="c in velo.cadres" :key="c.idMateriau">
          <p>{{ c.nomMat }}</p>
          <p class="sub-spec">Forme: {{ c.formeCadre }}</p>
        </div>
      </DetailRow>

      <DetailRow label="Taille">
        <p class="detail_taille_text">Classic:</p>
        <p v-for="(c,i) in velo.tailles" :key="c.idTaille" class="detail_taille_text">
          &nbsp;{{ c.libelleTaille }}<span v-if="i < velo.tailles.length - 1">,</span>
        </p>
      </DetailRow>

      <DetailRow label="Géométrie">
        <div v-for="geo in velo.geometries" :key="geo.idGeometrie">
          <p>{{ geo.nomPiece }}: {{ geo.taillePiece }}mm</p>
        </div>
      </DetailRow>
    </section>

    <section>
      <h1 class="detail_title">Informations de l'article</h1>
      
      <DetailRow label="Couleur">
        <p>{{ velo.couleurs[0].nomCouleur }}</p>
        <p class="sub-spec">Finition: {{ velo.couleurs[0].effetPeinture }}</p>
      </DetailRow>

      <DetailRow label="Poids">
        <p>{{ article.poids }} KG</p>
      </DetailRow>

      <DetailRow label="Référence">
        <p>{{ article.reference }}</p>
      </DetailRow>

      <DetailRow label="PVC*">
        <p>{{ article.prix }} EUR</p>
      </DetailRow>
    </section>

    <p class="sub-spec">*Prix de vente conseillé</p>
  </main>
</template>

<style scoped>
#detail {
  display: flex;
  flex-direction: column;
  gap: 4rem;
  width: 70vw;
  margin: 0 auto;
}

section {
  width: 100%;
}

.detail_title {
  font-size: 32px;
  text-transform: uppercase;
  text-align: left;
  padding-bottom: 1rem;
  border-bottom: 1px solid lightgray;
}

p {
  margin: 0;
}

.sub-spec {
  color: #666;
  font-size: 14px;
  margin-top: 4px;
  text-align: left;
  width: 100%;
}

.detail_taille_text {
  text-transform: none;
  display: inline-block;
}

</style>