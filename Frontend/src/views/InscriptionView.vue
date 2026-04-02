<script setup>
import FormInscription from '@/components/FormInscription.vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { useUtilsStore } from '@/stores/utils';


const router = useRouter();
const utils = useUtilsStore();
const createUser = ref(null);
const messageRetour = ref('');

const creerCompte = async (donneesClient) => {
    try {
        const response = await axios.post(utils.url + "Login/register", donneesClient);
        
        messageRetour.value = "Compte créé avec succès !";
        console.log("Réponse de l'API :", response.data);
        setTimeout(() => {
            router.push('/connexion');
        }, 2000);

    } catch (error) {
        messageRetour.value = "Erreur lors de la création du compte.";
        console.error("Erreur API :", error);
    }
};

</script>

<template>
    <main>
        <p v-if="messageRetour">{{ messageRetour }}</p>
        <FormInscription @submit-client="creerCompte"/>
    </main>
</template>

<style scoped>
main {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-top: 2rem;
}

.erreur {
    color: red;
    font-weight: bold;
    margin-bottom: 1rem;
}
</style>