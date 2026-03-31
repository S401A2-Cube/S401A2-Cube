<script setup>
import { useRoute } from 'vue-router';
import { computed, watchEffect } from 'vue';
import data from '../assets/json/data.json';

const route = useRoute();

const getAssetUrl = (path) => {
  const cleanPath = path.replace('@/', '../'); 
  return new URL(cleanPath, import.meta.url).href;
};

const pageData = computed(() => {
  const mainParam = route.params.mainCategory?.toLowerCase();
  const typeParam = route.params.type?.toLowerCase();
  const catParam = route.params.category?.toUpperCase().replace(/-/g, ' ');

  const categoryContainer = data.categories.find(cat => 
    cat.name.toLowerCase() === mainParam
  );
  if (!categoryContainer) return null;

  const contentGroup = categoryContainer.content.find(item => 
    item.title.toLowerCase() === typeParam
  );
  if (!contentGroup) return null;

  const details = contentGroup.information?.find(info => 
    info.title.toUpperCase() === catParam
  );

  return {
    parent: contentGroup,
    details: details
  };
});

</script>

<template>
  <main v-if="pageData" class="category-page">
    
    <header class="hero" :style="{ backgroundImage: `url(${getAssetUrl(pageData.parent.background)})` }">
      <div class="hero-overlay">
        <h1 class="hero-title">{{ pageData.parent.title }}</h1>
      </div>
    </header>

    <nav class="anchor-nav">
      <ul>
        <li><a href="#video">Video</a></li>
        <li><a href="#overview">Overview</a></li>
        <li v-for="info in pageData.parent.information" :key="info.title">
          <a :href="'#' + info.title.replace(/\s+/g, '-')">{{ info.title }}</a>
        </li>
      </ul>
    </nav>

    <div class="container">
      <section>
        <p class="page-intro">{{ pageData.parent.introduction }}</p>
        <video 
            v-if="pageData.parent.video && !pageData.parent.video.includes('.none')"
            id="video" 
            :key="pageData.parent.video" 
            :src="getAssetUrl(pageData.parent.video)"
            controls 
            class="category-video"
          >
          No video support on this browser.
        </video>
      </section>
      <section id="overview" class="preview-section">
        <div class="preview-grid">
          <div v-for="info in pageData.parent.overview" :key="info.title" class="preview-card">
            <img :src="getAssetUrl(info.image)" :alt="info.title" />
            <h3>{{ info.title }}</h3>
            <p>{{ info.description }}</p>
            <button class="btn-primary">Show all bikes</button>
          </div>
        </div>
      </section>

      <section 
      v-for="(info, index) in pageData.parent.information" 
      :key="info.title" 
      :id="info.title.replace(/\s+/g, '-').toLowerCase()"
      class="detail-block"
      >
      <div class="detail-sticky-image">
          <img :src="getAssetUrl(info.image)" :alt="info.title" />
      </div>
      
      <div class="detail-content">
          <div class="text-wrapper">
            <h2 class="detail-title">{{ info.title }}</h2>
            <span class="detail-subtitle">{{ info.subtitle }}</span>
            <p class="detail-desc">{{ info.description }}</p>

            <div class="list-section" v-if="info.applications">
                <h3 class="section-label">APPLICATIONS</h3>
                <div v-for="app in info.applications" :key="app.name" class="list-row">
                <div class="row-img">
                    <img :src="getAssetUrl(app.image)" :alt="app.name" />
                </div>
                <div class="row-text">
                    <h4>{{ app.name }}</h4>
                    <p>{{ app.description }}</p>
                </div>
                </div>
            </div>

            <div class="list-section" v-if="info.series">
                <h3 class="section-label">SERIES</h3>
                <div v-for="s in info.series" :key="s.name" class="list-row">
                <div class="row-img">
                    <img :src="getAssetUrl(s.image)" :alt="s.name" />
                </div>
                <div class="row-text">
                    <h4>{{ s.name }}</h4>
                    <p>{{ s.description }}</p>
                </div>
                </div>
            </div>
            <div class="list-section" v-if="info.products">
                <h3 class="section-label">PRODUCTS</h3>
                <div v-for="prod in info.products" :key="prod.name" class="list-row">
                    <div class="row-img">
                        <img :src="getAssetUrl(prod.image)" :alt="prod.name" />
                    </div>
                    <div class="row-text">
                        <h4>{{ prod.name }}</h4>
                        <p>{{ prod.description }}</p>
                    </div>
                </div>
            </div>
          </div>
      </div>
      </section>
    </div>
  </main>
</template>


<style scoped>
.category-page {
  font-family: 'Open Sans', sans-serif;
  color: #333;
  background-color: #fff;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.container {
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 2rem;
}

.hero {
  height: 500px;
  width: 100%;
  background-size: cover;
  background-position: center;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
}

.hero-overlay {
  background: rgba(0, 0, 0, 0.3);
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.hero-title {
  color: white;
  font-size: 5rem;
  font-weight: 900;
  text-transform: uppercase;
  letter-spacing: 2px;
}

.anchor-nav {
  position: sticky;
  top: 80px;
  width: 100%;
  max-width: 1400px;
  background: white;
  z-index: 50;
  border-bottom: 1px solid #eee;
  padding: 1rem 2rem;
  box-sizing: border-box;
}

.anchor-nav ul {
  display: flex;
  justify-content: left;
  list-style: none;
  gap: 3rem;
  margin: 0;
  padding: 0;
}

.anchor-nav a {
  text-decoration: none;
  color: #666;
  font-weight: bold;
  font-size: 0.9rem;
  text-transform: uppercase;
  transition: color 0.3s;
}

.anchor-nav a:hover {
  color: #000;
}

.page-intro {
  font-size: 1.2rem;
  color: #000;
  margin: 2rem 2rem 5rem 2rem;
}

.preview-section {
  padding: 5rem 0;
}

.preview-grid {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 2rem;
}

.preview-card {
  text-align: center;
  width: 25%;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.preview-card img {
  width: 100%;
  height: 250px;
  object-fit: cover;
  margin-bottom: 1.5rem;
}

.preview-card h3 {
  text-transform: uppercase;
  font-weight: 800;
  margin-bottom: 1rem;
}

.btn-primary {
  background: black;
  color: white;
  border: none;
  padding: 0.8rem 2rem;
  text-transform: uppercase;
  font-weight: bold;
  cursor: pointer;
  margin-top: 1rem;
}
.detail-block {
  display: flex;
  min-height: 100vh;
  border-bottom: 1px solid #eee;
}

.detail-block:nth-child(odd) {
  flex-direction: row-reverse;
}

.detail-sticky-image {
  flex: 1;
  height: 100vh;
  position: sticky;
  top: 0;
  overflow: hidden;
}

.detail-sticky-image img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}

.detail-content {
  flex: 1;
  padding: 5% 8%;
  background: white;
}

.text-wrapper {
  max-width: 600px;
}
.text-wrapper * {
  text-align: left;
}

.detail-title {
  font-size: 3.5rem;
  font-weight: bolder;
  margin: 0;
  text-transform: uppercase;
}

.detail-subtitle {
  display: block;
  font-weight: bold;
  text-transform: uppercase;
  font-size: 1.3rem;
  width: fit-content;
  margin-bottom: 1rem;
}

.section-label {
  font-size: 2.5rem;
  font-weight: 900;
  margin: 4rem 0 2rem 0;
  text-transform: uppercase;
}

.list-row {
  display: flex;
  gap: 1.5rem;
  align-items: flex-start;
  min-height: 178px;
}

.row-img {
    width: 250px; 
    height: 178px; 
    flex-shrink: 0; 
    overflow: hidden; 
    display: block;
}

.row-img img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    display: block;
}

.row-text {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.row-text * {
  text-align: left;
}

.row-text h4 {
  width: 100%;
  margin: 0 0 0.5rem 0;
  text-transform: uppercase;
  text-decoration: none;
  font-weight: bolder;
  font-size: 1.6rem;
}

.row-text p {
  width: 100%;
  margin: 0;
  font-size: 0.9rem;
  color: #444;
  line-height: 1.4;
}

.apps-container {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 2rem;
}

.app-chip {
  border: 1px solid #ddd;
  padding: 0.5rem 1rem;
  font-size: 0.8rem;
  text-transform: uppercase;
}

.series-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
  margin-top: 2rem;
}

.series-mini-card {
  background: #f9f9f9;
  padding: 1.5rem;
}

.series-mini-card h4 {
  margin: 0 0 0.5rem 0;
  text-transform: uppercase;
}

.series-mini-card p {
  font-size: 0.85rem;
  margin: 0;
  color: #666;
}

.preview-section,
.detail-block {
  scroll-margin-top: 140px;
}

#video {
  width: 100%;
  border: none;
}
</style>