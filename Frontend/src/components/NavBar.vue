
<script setup>
import { ref } from 'vue'

import NavSubMenu from './NavSubMenu.vue';
import '../assets/css/global.css';
import data from '../assets/json/data.json';

const menuItems = data.categories.map(cat => {
  return {
    name: cat.name,
    cats: cat.content.reduce((acc, item) => {
      const subItems = item.overview || item.information || [];
      
      acc[item.title] = subItems.map(sub => sub.title);
      return acc;
    }, {})
  };
});

var selected = ref(null);

</script>

<template>
    <div id="navbar">
        <div>
            <div class="navbar_left">

                <a href="/">
                    <img src="/logo.svg" width="100" height="80" alt="Logo" />
                </a>
                <NavSubMenu
                    v-for="item in menuItems" 
                    :key="item.name"
                    @toggleMenu="selected = item.name" 
                    @closeMenu="selected = null"
                    :opened="selected === item.name" 
                    :name="item.name" 
                    :categories="item.cats"
                />
                <router-link to="/Article">
                    <img src="../img/loupe.png" width="40" height="40" alt="loupe" @click="console.log('test')">
                </router-link>
            </div>
            <div class="navbar_right">
                <RouterLink to="/connexion">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-user-icon lucide-user"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                </RouterLink>
                <RouterLink to="/panier">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-shopping-cart-icon lucide-shopping-cart"><circle cx="8" cy="21" r="1"/><circle cx="19" cy="21" r="1"/><path d="M2.05 2.05h2l2.66 12.42a2 2 0 0 0 2 1.58h9.78a2 2 0 0 0 1.95-1.57l1.65-7.43H5.12"/></svg>                
                </RouterLink>
            </div>
        </div>

        <div @click="selected=null" v-if="selected!=null" class="navbar_close_btn">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-x-icon lucide-x"><path d="M18 6 6 18"/><path d="m6 6 12 12"/></svg>
            <p>Fermer</p>
        </div>
    </div>

</template>

<style scoped>

.nomDeMaClass {
    background-color: red !important;
}

#navbar {
    width: 100vw;
    overflow: hidden;
    display: flex;
    gap: 1rem;
    height: 80px;
    align-items: center;
    
    left: 0;
    background-color: white;
    padding-left: 2rem;
    border-bottom: 1px solid lightgray;
}

#navbar > div:first-child {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100vw;
    padding-right: 3rem;
}

.navbar_left,
.navbar_right {
    display: flex;
    gap: 2rem;
    align-items: center;
}

.navbar_close_btn {
    position: fixed;
    left: 0;
    width: 100vw;
    bottom: calc(15vh + 30px);
    cursor: pointer;
}

.navbar_close_btn > * {
    padding: 0;
    margin: 0;
}

img {
    background-position: center;
}

.navbar_left:hover {
    color: gray;
}

.navmenu_button:hover {
    color: #000;
}
</style>
