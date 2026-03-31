<script setup>
import { ref } from 'vue';

const quantite = ref(1);
const incrementer = () => {
  quantite.value++;
};

const decrementer = () => {
  if (quantite.value > 1) {
    quantite.value--;
  }
};

defineProps({
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
});

</script>

<template>
    <div id="ligne-panier">
      <div class="image-container">
        <img src="https://cube-bikes.ca/wp-content/uploads/2024/05/Cube-Stereo-Hybrid-140-HPC-Actionteam-750-actionteam-695325_side-view_00.png" />
      </div>
        <h2>{{ articleName }}</h2>
        <p>{{ price }}€</p>
        <p>REFERENCE: {{ articleRef }}</p>
        <p>SIZE: {{ taille }}</p>
        <p>COLOR: {{ couleur }}</p>

        <p>{{ price }} TTC</p>
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
        <p>supprimer</p>
    </div>
</template>


<style scoped>
#ligne-panier {
  display: flex;
  align-items: center;
  gap: 20px;
  padding: 1rem;
  border-bottom: 1px solid #eee;
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
  width: 420px;  
  height: 420px; 
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

</style>