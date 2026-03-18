
<script setup>
import { ref } from 'vue'

const BikesCategories = {
    "Mountainbike":  ["Vue d'ensemble", "Hardtail", "Fullsuspension"],
    "Road": ["Vue d'ensemble", "Road race", "Cyclocross", "Triathlon & time trial"],
    "Gravel": ["Vue d'ensemble", "Nuroad", "Cross race", "Nulane"],
    "Trekking": ["Vue d'ensemble", "Tour", "City", "Variantes de cadres"]
};
const filtres = ['Variante Cadre', 'Millésime', 'Couleurs'];
// const chevron = document.getElementById('chevron');
// chevron.addEventListener('click',() =>{chevron.ClassList="rotated"});

const menuItems = [
  { name: 'Bike', cats: BikesCategories },

];


var selected = ref(null);


const selectedIndex = ref(null);

const toggleRotation = (index) => {
  selectedIndex.value = selectedIndex.value === index ? null : index;
}
</script>

<template>
    <h1>Découvrez nore sélection</h1>
    <div id="Side_Filtre">
        <div v-for="titre in filtres">
            <div class="side_filtre"  >
               <h2>{{titre}}</h2>
               <svg xmlns="http://www.w3.org/2000/svg" @click="toggleRotation(index)" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="chevron" :class="{ 'rotated': selectedIndex === index }" ><path d="m18 15-6-6-6 6"/></svg>
            </div>
        </div>
                
      

        
    </div>


    <div id="articleContainer">
    <Product 
      v-for="product in BikesCategories"
      :info="product"

      />
  </div>

</template>

<style scoped>
#Side_Filtre {
    outline: 0.5px solid black;
    position: absolute;
    left: 50px;
    
    
}

.side_filtre {
    display: flex;         
    align-items: center;    
    justify-content: space-between; 
    cursor: pointer;
    padding: 5px 0;
    
}

#Side_Filtre h2 {
   font-weight: bold;
   
    
}

.chevron {
    transition: transform 0.5s ease;
}
.rotated{transform: rotate(180deg)}
</style>
