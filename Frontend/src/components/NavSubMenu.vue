
<script setup>
import { ref } from 'vue'

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

var selectedCategory = ref(null);

</script>

<template>
    <div class="menu-container"> 
        <div class="navmenu_button" :class="{selectedMenu: opened}">{{ name }}</div>

        <div v-if="opened" class="submenu">
            <div class="submenu_title">
                <p v-for="(v, k) in categories" :key="k" @mouseenter="selectedCategory = v" class="submenu_title_text">{{ k }}</p>
            </div>
            <div class="submenu_subcategories">
                <p class="subcategories" v-for="c in selectedCategory">{{ c }}</p>
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
    position: absolute;
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
    padding-right: 1rem;
    border-right: 1px solid black;
}

.submenu_title_text {
    margin: 0;
    padding: 1rem;
    width: 100%;
    text-align: left;
    transform: translateX(0);
    transition: transform 0.2s ease-out;
    cursor: pointer;
    text-transform: uppercase;
}

.submenu_title_text_selected {
    transform: translateX(0.5rem);
}

.submenu_subcategories {
    padding-left: 2rem;
}

.subcategories {
    width: 100%;
    text-align: left;
    text-transform: uppercase;
}

.selectedMenu {
    border-bottom: 4px outset black;
}

.navbar_close_btn > * {
    padding: 0;
    margin: 0;
    cursor: pointer;
}
</style>
