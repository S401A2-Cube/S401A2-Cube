<script setup>
import { ref } from 'vue';

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
        <h2>Créez votre compte</h2>
        <div id="se-connecter">
            <p>Vous avez déjà un compte ?</p>
            <p>Connectez-vous !</p>
        </div>

        <form>
            <h2>Mes informations personnelles</h2>
            <div id="civil">
                <input type="radio" id="mr" name="civilite" value="Mr" v-model="client.civilite">
                <label for="mr">M</label>
                <input type="radio" id="mme" name="civilite" value="Mme" v-model="client.civilite">
                <label for="mme">Mme</label>
            </div>
            <div class="input-container">
                <input class="input-effect" type="text" placeholder=" " required v-model="client.prenom">
                <label>Prénom *</label>
                <span class="focus-border"></span>
            </div>
            <div class="input-container">
                <input class="input-effect" type="text" placeholder=" " required v-model="client.nom">
                <label>Nom *</label>
                <span class="focus-border"></span>
            </div>
            <div class="input-container">
                <input class="input-effect" type="text" placeholder=" " required v-model="client.email">
                <label>Email *</label>
                <span class="focus-border"></span>
            </div>
            <div class="input-container">
                <input class="input-effect" type="text" placeholder=" " required v-model="client.naissance">
                <label>Date de naissance</label>
                <span class="focus-border"></span>
            </div>
            <div class="input-container">
                <input class="input-effect" type="password" placeholder=" " required v-model="client.password">
                <label>Mot de passe *</label>
                <span class="focus-border"></span>
            </div>
            <button type="submit" class="submit-btn">S'incscrire</button>
        </form>
    </div>
</template>


<style scoped>
h2 {
    text-transform: uppercase;
    margin-bottom: 1rem;
}

#inscription {
    margin-top: 5rem;
    width: 800px;
    display: flex;
    flex-direction: column;
}

#se-connecter {
    display: flex;
    flex-direction: column;
    margin-bottom: 0.5rem;
}

#se-connecter p{
    align-self: flex-start;
    line-height: normal;
}

form {
    display: flex;
    flex-direction: column;
    background-color: #fff;
    padding: 1.5rem;
    align-items: center;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
}

</style>