<script setup>
import { ref } from 'vue';
import Input from './Input.vue';
import RedButton from './RedButton.vue';

const client = ref({
    email: '',
    password: ''
})

// TODO : méthode connexion compte
</script>

<template>
    <div id="connexion">
        <h2>Connexion / Inscription</h2>
        <form>
            <Input label="Email" v-model="client.email" required/>
            <Input type="password" label="Mot de passe" v-model="client.password" required/>
            <RedButton type="submit">Se connecter</RedButton>
            <RouterLink class="router-link" to="/inscription">Pas de compte ? Créez-en un</RouterLink>
        </form>
    </div>
</template>


<style scoped>
h2 {
    text-transform: uppercase;
    margin-bottom: 1rem;
}

#connexion {
    width: 800px;
    display: flex;
    flex-direction: column;
}

form {
    display: flex;
    flex-direction: column;
    background-color: #fff;
    padding: 1.5rem;
    align-items: center;
    box-shadow: var(--card-shadow);
}

.router-link {
    text-decoration: underline;
    transition: all 0.4s;
    color: var(--blue-cube);
    margin-top: 1.5rem;
}

.router-link:hover {
    color: var(--blue-muted);
    text-decoration: none;
}

</style>