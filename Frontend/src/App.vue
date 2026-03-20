<script setup>
import { computed } from 'vue'
import { RouterView, useRoute } from 'vue-router'
import NavBar from './components/NavBar.vue';
import Footer from './components/Footer.vue';
import './assets/css/global.css';

const route = useRoute()

</script>

<template>
  <header>
    <NavBar />
  </header>

  <section>
    <RouterView />
  </section>

  <Footer />
</template>

<style scoped>

nav {
  display: flex;
  justify-content: center;
  flex-direction: row;
  gap: 1rem;
}

img {
    height: 100%;
    width: 100%;
    object-fit: cover;
    display: block;
}

</style>
