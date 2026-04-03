<script setup>
import { useRouter } from 'vue-router'
import { client } from '@/data/client.js'
const router = useRouter()
</script>

<template>
  <div class="commandes-page">



      <div class="carte">
        <h2>Mes Commandes</h2>
        <div 
            v-for="commande in client.clientCommande" 
            :key="commande.id"
            class="commande-ligne"

            
          >
          <p>Commande n° {{ commande.id }}</p>
          <p>Date de Livraison : {{ commande.livraison }}</p>
          <p> {{ commande.adresseCommandeFact.rue }}</p> <p>{{ commande.adresseCommandeFact.codePostale }}</p> <p>{{ commande.adresseCommandeFact.ville }}</p> <p>{{ commande.adresseCommandeFact.pays }}</p> 
          <div v-for="article in commande.articleCommande" :key="article.articleId">
            <p>{{ article.nom }}</p>
          </div>
      </div>
    </div>

        

      
  

  </div>
</template>


<style scoped>
.commandes-page {
  font-family: sans-serif;
  padding: 20px;
  background-color: #f9f9f9;
  min-height: 100vh;
  display: flex;
  justify-content: center;
}

.carte {
  background: white;
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  width: 100%;
  max-width: 600px;
}

h2 {
  color: #2c3e50;
  border-bottom: 2px solid #eee;
  padding-bottom: 10px;
  margin-bottom: 20px;
}

.commande-ligne {
  border: 1px solid #edf2f7;
  border-radius: 8px;
  padding: 15px;
  margin-bottom: 15px;
  transition: transform 0.2s;
}

.commande-ligne:hover {
  background-color: #8b7b7b;
  
}

.commande-ligne p {
  margin: 5px 0;
  font-size: 0.95rem;
  color: #4a5568;
}

.commande-ligne div {
  margin-top: 10px;
  padding-top: 10px;
}

.commande-ligne div p {
  font-weight: bold;
  color: #3182ce; 
}
</style>

