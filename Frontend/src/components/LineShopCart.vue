<script setup>
import { defineModel } from 'vue';

const quantite = defineModel('quantite', { type: Number, default: 1 });

const incrementer = () => {
  quantite.value++;
};

const decrementer = () => {
  if (quantite.value > 1) {
    quantite.value--;
  }
};

const props = defineProps({
  id: {
    type:Number,
    required:true
  },
  articleName: {
    type: String,
    required: true
  },
  articleRef: {
    type: String,
    required: true
  },
  price: {
    type: Number,
    required: true
  },
  couleur: {
    type: String,
    required: false
  },
  taille: {
    type: String,
    required: false
  },
  imageURL: {
    type: String,
    required: false
  }
});

</script>

<template>
    <div id="ligne-panier">
      <div class="image-container">
        <img :src=" imageURL ? imageURL : 'https://cube-bikes.ca/wp-content/uploads/2024/05/Cube-Stereo-Hybrid-140-HPC-Actionteam-750-actionteam-695325_side-view_00.png'" />
      </div>

      <div class="container-div">
        <div class="info-ligne">
          <h2>{{ articleName }}</h2>
          <p class="article-price">{{ price }}€</p>
          <p class="article-ref">REFERENCE: {{ articleRef }}</p>
          <p>SIZE: {{ taille }}</p>
          <p>COLOR: {{ couleur }}</p>
        </div>

        <div class="action-ligne">
          <h3>{{ price }}€ TTC</h3>
          <div class="input-container">
            <button type="button" class="btn-qty" @click="decrementer">-</button>
            <input 
              type="number" 
              v-model.number="quantite" 
              min="1" 
              class="input-panier"
            >
            <button type="button" class="btn-qty" @click="incrementer">+</button>
          </div>
          <p class="supprimer-link">Supprimer</p>
        </div>
      </div>
    </div>
</template>


<style scoped>
#ligne-panier {
  display: flex;
  align-items: center;
  gap: 20px;
  padding: 1rem;
  background-color: #fff;
  box-shadow: var(--card-shadow);
  width: 800px;
  height: 200px;
}

.input-container {
  display: flex;
  align-items: center;
  border: 1px solid var(--very-light-grey); 
  border-radius: 4px;
  width: fit-content;
  transition: border-color 0.3s, box-shadow 0.3s;
}

.input-container:focus-within {
  border: 1px solid var(--blue-cube); 
  box-shadow: 0 0 5px var(--blue-muted);
}

.btn-qty {
  background: none;
  border: none;
  padding: 5px 12px;
  cursor: pointer;
  font-size: 1.2rem;
}

.input-panier {
  height: 30px;
  width: 40px;
  border: none; 
  text-align: center;
  outline: none; 
  -moz-appearance: textfield;
}

.input-panier::-webkit-outer-spin-button,
.input-panier::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.img-panier {
  max-width: 100px;
  max-height: 100px;
  object-fit: contain;
}

.image-container {
  width: 250px;  
  height: 200px; 
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden; 
  border-radius: 8px;
}

.image-container img {
  max-width: 100%;  
  max-height: 100%; 
  object-fit: contain;
}

.info-ligne {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: flex-start; 
  gap: 4px;
  line-height: 1.5;
}

.action-ligne {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 10px;
  min-width: 150px;
}

.container-div {
  display: flex;
  align-items: flex-start;
  width: 100%;
  padding-right: 1rem;
}

.supprimer-link {
    transition: all 0.4s;
    color: var(--light-grey);
}

.supprimer-link:hover {
    color: #000;
    cursor: pointer;
}

.article-ref {
  font-weight: 300;
  font-size: 12px;
}

.article-price {
  font-weight: bold;
  font-style: italic;
}
</style>