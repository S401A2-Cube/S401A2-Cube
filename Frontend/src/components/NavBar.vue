
<script setup>
import { ref } from 'vue'

import NavSubMenu from './NavSubMenu.vue';
import '../assets/css/global.css';

const BikesCategories = {
    "Mountainbike":  ["Vue d'ensemble", "Hardtail", "Fullsuspension"],
    "Road": ["Vue d'ensemble", "Road race", "Cyclocross", "Triathlon & time trial"],
    "Gravel": ["Vue d'ensemble", "Nuroad", "Cross race", "Nulane"],
    "Trekking": ["Vue d'ensemble", "Tour", "City", "Variantes de cadres"]
};

const EBikesCategories = {
    "Mountainbike":  ["Vue d'ensemble", "Hardtail", "Fullsuspension"],
    "Trekking": ["Vue d'ensemble", "Tour", "City", "S-Pedelec", "Compact & folding", "Variantes de cadres"],
    "Gravel": ["Vue d'ensemble", "Nuroad hybrid", "Nulane hybrid"],
    "Transport": ["Vue d'ensemble", "Trike hybrid", "Cargo Hybrid", "Guide"],
    "Technology": ["Bosch"]
};

const EnfantsCategories = {
    "EN FONCTION DE LA TAILLE DU CORPS": [
        "À PARTIR DE 85CM",
        "À PARTIR DE 97CM",
        "À PARTIR DE 107CM",
        "À PARTIR DE 111CM",
        "À PARTIR DE 118CM",
        "À PARTIR DE 124CM",
        "À PARTIR DE 130CM"
    ],
    "SELON L’ÂGE": [
        "1.5-3 YEARS",
        "3 à 4 ANS",
        "3 à 5 ANS",
        "5 à 7 ANS",
        "7 à 9 ANS",
        "9 à 11 ANS",
        "À PARTIR DE 10 ANS"
    ],
    "SELON LA TAILLE DE LA DRAISIENNE": [
        "12\'\'",
        "14\'\'",
        "16\'\'",
        "18\'\'",
        "20\'\'",
        "24\'\'",
        "26\'\'",
        "27.5\'\'",
        "29\'\'"
    ],

    "SELON LA SÉRIE": [
        "NUMOVE",
        "ARUBA",
        "ACID",
        "REACTION",
        "ELITE",
        "PHENIX",
        "STEREO",
        "NUROAD",
        "FLYING CIRCUS"
    ],
    "E-BIKES": [
        "VUE D'ENSEMBLE",
        "ACID 240 HYBRID ROOKIE"
    ],
    "GUIDE": [

    ],
    "GEAR": [
        "CASQUES",
        "HABILLEMENT",
        "CASQUETTES & BASIQUES",
        "GANTS",
        "CHAUSSURES",
        "SACS À DOS",
    ],
    "EQUIPMENT": [
        "GARDE-BOUE",
        "PORTE-BAGAGES",
        "BÉQUILLES",
        "SELLES",
        "POIGNÉES",
        "PÉDALES",
        "POTENCES",
        "CINTRES",
        "PÉDALIERS",
        "PNEUS",
    ]
}

const menuItems = [
  { name: 'Bike', cats: BikesCategories },
  { name: 'E-Bike', cats: EBikesCategories },
  { name: 'Enfants', cats: EnfantsCategories },
//   { name: 'Gear', cats: EBikesCategories },
//   { name: 'Equipement', cats: BikesCategories },
//   { name: 'Cube World', cats: EBikesCategories }
];

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
                    @click="selected = item.name" 
                    :opened="selected === item.name" 
                    :name="item.name" 
                    :categories="item.cats"
                />
                <router-link to="/Article">
                    <img src="../img/loupe.png" width="40" height="40" alt="loupe" @click="console.log('test')">
                </router-link>
            </div>
            <!-- Gap -->
            <div class="navbar_right">
                <!-- Revendeur -->
                <!-- Search -->
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
    
    position: fixed;
    position: sticky;
    top: 0;
    left: 0;
    background-color: white;
    padding-left: 2rem;
    border-bottom: 1px solid lightgray;
    z-index: 100;
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
