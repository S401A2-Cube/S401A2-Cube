<script setup>
import { computed } from 'vue'
import { RouterView, useRoute } from 'vue-router'
import NavBar from './components/navbar.vue';

const route = useRoute()

</script>

<template>
  <header>
    <NavBar />
  </header>

  <section>
    <RouterView />
  </section>
</template>

<style scoped>
* {
  box-sizing: border-box;
  padding: 0;
  margin: 0;
  font-family: 'Roboto Condensed', sans-serif;
  text-align: center;
}

nav {
  display: flex;
  justify-content: center;
  flex-direction: row;
  gap: 1rem;
}

</style>
