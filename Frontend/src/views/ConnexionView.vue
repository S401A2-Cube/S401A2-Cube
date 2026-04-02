<script setup>
import FormConnexion from '@/components/FormConnexion.vue';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import { useUtilsStore } from '@/stores/utils';
import { storeAuthToken } from '@/stores/utils';

const router = useRouter();
const utils = useUtilsStore();

const messageErreur = ref('');

const connecterCompte = async (donneesLogin) => {
    try {
        messageErreur.value = '';
        const response = await axios.post(utils.url + "/Login", donneesLogin);

        if (response.data.token) {
            storeAuthToken(response.data.token);
        }
        router.push('/');
    }
    catch (error) {
        messageErreur.value = "Email ou mot de passe incorrect.";
        console.error("Erreur de connexion :", error);
    }
};
</script>

<template>
    <main>
        <p v-if="messageErreur" class="erreur">{{ messageErreur }}</p>
        <FormConnexion @submit-login="connecterCompte"/>
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