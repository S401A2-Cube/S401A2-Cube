
<script setup>
import { ref } from 'vue'
import '../assets/css/global.css';

const props = defineProps({
  name: {
    required: true
  },
  categories:  {
    required: true
  },
  opened: {
    required: true
  }
})

const selectedCategoryKey = ref(null); 

</script>

<template>
    <div class="menu-container"> 
        <div class="navmenu_button" :class="{selectedMenu: opened}">{{ name }}</div>

        <div v-if="opened" class="submenu">
            <div class="submenu_title">
                <p v-for="(v, k) in categories" :key="k" @mouseenter="selectedCategoryKey = k" class="submenu_title_text">
                    <span :class="{submenu_title_text_selected: selectedCategoryKey === k}">
                        {{ k }}
                    </span>
                </p>
            </div>
            <div class="submenu_subcategories">
                <p class="subcategories" v-for="c in categories[selectedCategoryKey]" :key="c">
                    {{ c }}
                </p>
            </div>
        </div>
    </div>
</template>

<style scoped>
.navmenu_button {
    cursor: pointer;
    text-transform: uppercase;
    border-bottom: 4px outset transparent;
    font-weight: 600;
    font-size: 1.2rem;
}

.submenu {
    box-shadow: 0px 20px 20px 0px #00000022;
    position: fixed;
    top: 80px;
    left: 0;
    width: 100vw;
    height: calc(70vh - 80px);
    background-color: white;

    display: flex;
    justify-content: left;
    align-items: start;
    flex-direction: row;
}

.submenu_title {
    padding: 0 1rem 0 1rem;
    height: calc(100% - 4rem);
    border-right: 1px solid black;
}

.submenu_title_text {
    margin: 0;
    padding: 1rem 0 1rem 0;
    width: 100%;
    text-align: left;
    transform: translateX(0);
    transition: transform 0.2s ease-out;
    cursor: pointer;
    text-transform: uppercase;
    font-weight: bold;
}

.submenu_title_text > span {
    background: linear-gradient(currentColor 0 0) 
        bottom left/
        var(--underline-width, 0%) 0.2em
        no-repeat;
    color: gray;
    display: inline-block;
    padding: 0 0 0.2em;
    text-decoration: none;
    transition: all 0.5s;
}

.submenu_title_text_selected {
  --underline-width: 100%;
  color: black !important;
}

.submenu_subcategories {
    padding: 1rem 0 0 2rem;
    gap: 2rem;
    display: flex;
    flex-direction: column;
    font-weight: bold;
}

.subcategories {
    width: 100%;
    text-align: left;
    text-transform: uppercase;
    cursor: pointer;
    color: gray;
    transition: all 0.2s;
}

.subcategories:hover {
    color: black;
}

.navmenu_button {
  background: linear-gradient(currentColor 0 0) 
    bottom left/
    var(--underline-width, 0%) 0.2em
    no-repeat;
  color: #000;
  display: inline-block;
  padding: 0 0 0.2em;
  text-decoration: none;
  transition: background-size 0.5s, color 0.4s;
}

.selectedMenu {
  --underline-width: 100%;
}

.navbar_close_btn > * {
    padding: 0;
    margin: 0;
    cursor: pointer;
}

.navmenu_button:hover {
    color: gray;
}

</style>
