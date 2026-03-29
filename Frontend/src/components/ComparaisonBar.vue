<script setup>
import { useComparaisonStore } from '@/stores/comparaison'
import { useRouter } from 'vue-router'

const store = useComparaisonStore()
const router = useRouter()
</script>

<template>
  <Transition name="slide-up">
    <div v-if="store.count > 0" class="comparison-bar" @click="router.push('/comparaison')">
      <div class="product-comparaison">
        <div class="text">Comparaison de vélos ({{store.count}})</div>
      </div>
    </div>
  </Transition>
</template>

<style>
.comparison-bar {
position: fixed;
bottom: 0;
left: 50%;
width: 20%;
background: rgb(66, 62, 62);
font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
font-weight: bold;
color: aliceblue;
padding: 1rem 2rem;
z-index: 100;
border: 1px solid #eee;
border-radius: 5px;
border-top: 1px solid #eee;
box-shadow: 0 4px 20px rgba(0,0,0,0.1);
cursor: pointer;

}
</style>