<script setup>
defineProps({
  article: { type: Object, required: true },
  velo: { type: Object, required: true }
});
</script>

<template>
  <section id="landing">
    <div class="titlebar">
      <div class="titlebar_left">
        <p class="category">{{ article.categorieArticle.nom }}</p>
        <p class="title bold">{{ article.nom.split(" ").reverse().slice(1).reverse().join(" ") }}</p>
        <p class="title">{{ article.nom.split(" ").pop() }}</p>
        <p class="price">PVC* {{ article.prix }} EUR</p>
        
        <!-- <div class="availability">
          <span v-if="article.dispoEnLigne" class="status dot-green">Disponible en ligne</span>
          <span class="stock">({{ article.qteStock }} en stock)</span>
        </div> -->
        
        <p class="reference">Référence d'article {{ article.reference }} | {{ velo.couleurs[0].nomCouleur }}</p>
      </div>
      
      <div class="titlebar_right">
        <div class="action-btn">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M20 10c0 4.993-5.539 10.193-7.399 11.799a1 1 0 0 1-1.202 0C9.539 20.193 4 14.993 4 10a8 8 0 0 1 16 0"/><circle cx="12" cy="10" r="3"/></svg>
          <p class="link">Revendeur</p>
        </div>
        <div class="action-btn">
          <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M8 3 4 7l4 4"/><path d="M4 7h16"/><path d="m16 21 4-4-4-4"/><path d="M20 17H4"/></svg>
          <p class="link">Comparaison</p>
        </div>
      </div>  
    </div>
    
    <div class="canvas-container" :data-360-src="velo.lienVue360">
      <canvas></canvas>
    </div>
  </section>
</template>

<style scoped>
#landing {
  height: 100vh;
  margin-top: calc(80px + 1rem);
}

.titlebar {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: start;
}

.title {
  font-size: 50px;
  text-transform: uppercase;
  margin: 0;
}

.category {
  font-size: 14px;
  text-transform: uppercase;
  color: #666;
  letter-spacing: 1px;
}

.price {
  font-size: 22px;
  font-weight: bold;
  margin-top: 10px;
  color: #98989a;
}

.reference {
  color: #98989a;
  font-size: 0.875rem;
}

.titlebar_left * {
  text-align: left;
}

.titlebar_right {
  display: flex;
  flex-direction: row;
  gap: 2rem;
}

.action-btn {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
}

.link {
  text-transform: uppercase;
}

canvas {
  width: 100%;
  height: 60vh;
  background-color: black;
  margin-top: 2rem;
}
</style>