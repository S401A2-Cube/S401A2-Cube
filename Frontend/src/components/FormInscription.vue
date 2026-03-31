<script setup>
import { ref } from 'vue';
import Input from './Input.vue';
import RedButton from './RedButton.vue';

const client = ref({
    civilite: 'Mr',
    prenom: '',
    nom: '',
    email: '',
    naissance: '',
    password: ''

})

// TODO : méthode création de compte
</script>

<template>
    <div id="inscription">
        <h2>Connexion / Inscription</h2>
        <div id="se-connecter">
            <p>Vous avez déjà un compte ?</p>
            <RouterLink class="router-link" to="/connexion">Connectez-vous !</RouterLink>
        </div>

        <form>
            <h2>Mes informations personnelles</h2>
            <div id="civil">
                <div>
                    <input type="radio" id="mr" name="civilite" value="Mr" v-model="client.civilite">
                    <label for="mr">M</label>
                </div>
                <div>
                    <input type="radio" id="mme" name="civilite" value="Mme" v-model="client.civilite">
                    <label for="mme">Mme</label>
                </div>
            </div>

            <Input label="Prénom" v-model="client.prenom" required/>
            <Input label="Nom" v-model="client.nom" required/>
            <Input label="Email" v-model="client.email" required/>
            <Input type="date" label="Date de naissance" v-model="client.naissance"/>
            <Input type="password" label="Mot de passe" v-model="client.password" required/>

            <RedButton type="submit">S'inscrire</RedButton>
        </form>
    </div>
</template>


<style scoped>
h2 {
    text-transform: uppercase;
    margin-bottom: 1rem;
}

#inscription {
    width: 800px;
    display: flex;
    flex-direction: column;
}

#se-connecter {
    display: flex;
    flex-direction: column;
    margin-bottom: 0.5rem;
}

#se-connecter > *{
    align-self: flex-start;
    line-height: normal;
}

form {
    display: flex;
    flex-direction: column;
    background-color: #fff;
    padding: 1.5rem;
    align-items: center;
    box-shadow: var(--card-shadow);
}

#civil {
    display: flex;
    gap: 5rem
}

#civil div {
    display: flex;
    gap: 10px;
}

.router-link {
    text-decoration: underline;
    transition: all 0.4s;
    color: var(--blue-cube);
}

.router-link:hover {
    color: var(--blue-muted);
    text-decoration: none;
}
</style>