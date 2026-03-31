
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
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-search-icon lucide-search"><path d="m21 21-4.34-4.34"/><circle cx="11" cy="11" r="8"/></svg>
                </router-link>
            </div>
            <div class="navbar_right">
                <RouterLink to="/connexion">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" viewBox="0 0 24 24" fill="none" stroke="#000" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-user-icon lucide-user"><path d="M19 21v-2a4 4 0 0 0-4-4H9a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
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

.navbar_left {
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
