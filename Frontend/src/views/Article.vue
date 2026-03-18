
<script setup>
import { ref } from 'vue'



const Bikes = [
  { nom: "Aim Race",        categorie: "Mountainbike", prix: 799,  img: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-Race_grey_n_red.png" },
  { nom: "Aim SL",          categorie: "Mountainbike", prix: 999,  img: "https://images.cube.eu/2024/bikes/2024_CUBE_Aim-SL_red_n_red.png" },
  { nom: "Attain GTC Pro",  categorie: "Road",         prix: 1299, img: "https://images.cube.eu/2024/bikes/2024_CUBE_Attain-GTC-Pro_carbon_n_white.png" },
  { nom: "Agree C:62",      categorie: "Road",         prix: 2499, img: "https://images.cube.eu/2024/bikes/2024_CUBE_Agree-C62-Race_carbon_n_white.png" },
  { nom: "Nuroad Race",     categorie: "Gravel",       prix: 1099, img: "https://images.cube.eu/2024/bikes/2024_CUBE_Nuroad-Race_grey_n_metal.png" },
  { nom: "Cross Race",      categorie: "Gravel",       prix: 899,  img: "https://images.cube.eu/2024/bikes/2024_CUBE_Cross-Race_grey_n_red.png" },
  { nom: "Tour EXC",        categorie: "Trekking",     prix: 649,  img: "https://images.cube.eu/2024/bikes/2024_CUBE_Tour-EXC_flashgrey_n_black.png" },
  { nom: "Travel SLX",      categorie: "Trekking",     prix: 1199, img: "https://images.cube.eu/2024/bikes/2024_CUBE_Travel-SLX_grey_n_black.png" },
]
// const bikesFiltres = computed(() => {
//   if (filtreActif.value === null) return Bikes 
//   return Bikes.filter(bike => bike.categorie === filtreActif.value)
// })

const filtres = ['Variante Cadre', 'Millésime', 'Couleurs'];
// const chevron = document.getElementById('chevron');
// chevron.addEventListener('click',() =>{chevron.ClassList="rotated"});

const menuItems = [
  { name: 'Bike', cats: Bikes },

];


var selected = ref(null);


const selectedIndex = ref(null);

const toggleRotation = (index) => {
  selectedIndex.value = selectedIndex.value === index ? null : index;
}
</script>

<template>
    <div id="Side_Filtre">
        <div v-for="(titre,index) in filtres">
            <div class="side_filtre"  >
               <h2>{{titre}}</h2>
               <svg xmlns="http://www.w3.org/2000/svg" @click="toggleRotation(index)" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="chevron" :class="{ 'rotated': selectedIndex === index }" ><path d="m18 15-6-6-6 6"/></svg>
            </div>
        </div>
        
    </div>


   <div id="articleContainer">
        <h1>Découvrez nore sélection</h1>
        <div class="grid">

        <div v-for="bike in Bikes" :key="bike.nom" class="article">
            <div class="article-img">
                <img :src="bike.img" :alt="bike.nom"/>

            </div>
            <div class="article-body">
            <h1>{{ bike.categorie }}</h1>
            <h2>{{ bike.nom }}</h2>
            <p class="prix">{{ bike.prix }} €</p>
          </div>
        </div>
    </div>
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


/*  Oue là c'est gemini qui carry */
#articleContainer {
  margin-left: 250px;
  padding: 32px;
}

#articleContainer h1 {
  font-size: 28px;
  font-weight: 800;
  margin-bottom: 32px;
  color: #111;
}


.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(260px, 1fr));
  gap: 24px;
  
}



.article {
  background: #fff;
  border-radius: 6px;
  overflow: hidden;
  transition: box-shadow 0.2s, transform 0.2s;
  cursor: pointer;
}

.article:hover {
  box-shadow: 0 8px 24px rgba(0,0,0,0.12);
  transform: translateY(-4px);
}

.article-img {
  height: 200px;
  background: #f0f0f0;
  overflow: hidden;
}

.article-img img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.4s ease;
}

.article:hover .article-img img {
  transform: scale(1.05);
}

.article-body {
  padding: 16px 20px 20px;
}

.article-body h1 { 
  font-size: 11px;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 2px;
  color: #e63000;
  margin-bottom: 6px;
}

.article-body h2 {  
  font-size: 18px;
  font-weight: 700;
  color: #111;
  margin-bottom: 10px;
}

.prix {
  font-size: 16px;
  font-weight: 600;
  color: #333;
}

</style>
