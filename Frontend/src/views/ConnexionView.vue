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
        console.log("Données reçues :", response.data);

        if (response.data.token) {
            utils.login(response.data.token, response.data);
            router.push('/');
            //storeAuthToken(response.data.token);
        }
        router.push('/');
    }
    catch (error) {
        messageErreur.value = "Email ou mot de passe incorrect.";
        console.error("Erreur de connexion :", error);
    }
};

const deconnecterCompte = () => {
    utils.logout();
};
</script>

<template>
    <main>
        <p v-if="messageErreur" class="erreur">{{ messageErreur }}</p>
        <div v-if="!utils.isConnected" class="form-container">
            <FormConnexion @submit-login="connecterCompte" />
        </div>

        <div v-else class="profile-container">
            <h2>Bienvenue</h2>
            <p>Vous êtes connecté</p>
            <button @click="deconnecterCompte" class="btn-logout">
                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m16 17 5-5-5-5"/><path d="M21 12H9"/><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/></svg>
                <span>Se déconnecter</span>
            </button>
        </div>
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

.profile-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    text-align: center;
    gap: 1rem;
}

.btn-logout {
    text-decoration: none;
    font-weight: 700;
    border: 1px solid #111;
    color: inherit;
    border-radius: 999px;
    padding: 0.3rem 0.75rem;
    transition: all 0.2s ease;
    display: flex;
    gap: 1rem;
    align-items: center;
}

.btn-logout:hover {
    background: #111;
    color: #fff;
}
</style>