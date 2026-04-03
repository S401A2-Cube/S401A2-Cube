<script setup>
import { useRouter } from 'vue-router'
import { client } from '@/data/client.js'



const router = useRouter()
</script>

<template>
  <div class="compte-page">

    <p class="principal">ACCUEIL > VOTRE COMPTE</p>

    <div class="compte-grid">

      <div class="carte">
        <h2>Mon Profil</h2>

        <div class="profil-info">
          <p class="profil-nom">{{ client.prenom }} {{ client.nom }}</p>
          <p class="profil-email">{{ client.email }}</p>
        </div>

        <div class="profil-adresse">
          <p>{{ client.adresseClient.rue }}</p>
          <p>{{ client.adresseClient.codePostale }} {{ client.adresseClient.ville }}</p>
          <p>{{ client.adresseClient.pays }}</p>
        </div>

        <button class="lien-action">Modifier mon profil</button>
      </div>

      <div class="carte">
        <h2>Mes Commandes</h2>

        <!-- fonction ternaire en gros c'est comme un if else  -->
        <p class="commandes-count"> 
          {{ client.clientCommande.length }} commande{{ client.clientCommande.length > 1 ? 's' : '' }} passée{{ client.clientCommande.length > 1 ? 's' : '' }}
        </p>

        <div class="commandes-apercu">
          <div 
            v-for="commande in client.clientCommande.slice(0, 2)" 
            :key="commande.id"
            class="commande-ligne"
          >
            <span class="commande-id">N°{{ commande.id }}</span>
            <span class="commande-articles-count">
              {{ commande.articleCommande.length }} article
            </span>
          </div>
        </div>

        <button class="lien-action" @click="router.push('/compte/commandes')">
           Voir toutes mes commandes
        </button>
      </div>

    </div>
  </div>
</template>

<style scoped>
.compte-page {
  max-width: 1200px;
  margin: 120px auto 4rem auto;
  padding: 0 2rem;
}

.principal {
  font-size: 0.75rem;
  text-transform: uppercase;
  color: #999;
  letter-spacing: 1px;
  margin-bottom: 2rem;
}

.compte-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1.5rem;
}

.carte {
  background: white;
  border: 1px solid #e5e5e5;
  padding: 2.5rem;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

h2 {
  font-size: 1.1rem;
  font-weight: 900;
  text-transform: uppercase;
  font-style: italic;
  letter-spacing: 1px;
  margin: 0;
}

.profil-nom {
  font-weight: 700;
  font-size: 1rem;
  margin: 0;
}

.profil-email {
  color: #666;
  font-size: 0.875rem;
  margin: 0.25rem 0 0 0;
}

.profil-adresse {
  border-top: 1px solid #f0f0f0;
  padding-top: 1rem;
}

.profil-adresse p {
  margin: 0.2rem 0;
  font-size: 0.9rem;
  color: #444;
}

.commandes-count {
  font-size: 0.875rem;
  color: #666;
  margin: 0;
}

.commandes-apercu {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  border-top: 1px solid #f0f0f0;
  padding-top: 1rem;
}

.commande-ligne {
  display: flex;
  justify-content: space-between;
  font-size: 0.875rem;
  padding: 0.4rem 0;
  border-bottom: 1px solid #f5f5f5;
  color: #444;
}

.commande-id {
  font-weight: 700;
}

.commande-articles-count {
  color: #999;
}

.lien-action {
  background: none;
  border: none;
  padding: 0;
  font-size: 0.875rem;
  color: #c0392b;
  cursor: pointer;
  text-align: left;
  font-family: inherit;
  font-weight: 600;
  margin-top: auto;
}

.lien-action:hover {
  text-decoration: underline;
} 
</style> 