<script setup>
import { ref } from 'vue';
import Input from './Input.vue';
import RedButton from './RedButton.vue';

const emit = defineEmits(['submit-client'])
const client = ref({
    CiviliteId: 1,
    Prenom: '',
    Nom: '',
    Email: '',
    DateNaissance: null,
    Mdp: '',
    Role: 1,
    AdresseClient:null,
})

const soumettreFormulaire = () => {
    emit('submit-client', client.value);
};

</script>

<template>
    <div id="inscription">
        <h2>Connexion / Inscription</h2>
        <div id="se-connecter">
            <p>Vous avez déjà un compte ?</p>
            <RouterLink class="router-link" to="/connexion">Connectez-vous !</RouterLink>
        </div>

        <form @submit.prevent="soumettreFormulaire">
            <h2>Mes informations personnelles</h2>
            <div id="civil">
                <div>
                    <input type="radio" id="mr" name="civilite" :value="1" v-model="client.CiviliteId">
                    <label for="mr">M</label>
                </div>
                <div>
                    <input type="radio" id="mme" name="civilite" :value="2" v-model="client.CiviliteId">
                    <label for="mme">Mme</label>
                </div>
            </div>

            <Input label="Prénom" v-model="client.Prenom" required/>
            <Input label="Nom" v-model="client.Nom" required/>
            <Input label="Email" v-model="client.Email" required/>
            <Input type="date" label="Date de naissance" v-model="client.DateNaissance"/>
            <Input type="password" label="Mot de passe" v-model="client.Mdp" required/>

            
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
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
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
    color: #2fb5d2;
}

.router-link:hover {
    color: #298499;
    text-decoration: none;
}
</style>