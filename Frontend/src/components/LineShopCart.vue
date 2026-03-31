<script setup>

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
    type: string,
    required: true
  },
  price: {
    type: Number,
    required: true
  },
  imageURL: {
    type: String,
    required: true
  },
  taille: {
    type: String,
    required: false
  },
});


// TODO : méthode connexion compte
</script>

<template>
    <div id="connexion">
        <form>
            <h2>{}</h2>
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
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
}

.router-link {
    text-decoration: underline;
    transition: all 0.4s;
    color: #2fb5d2;
    margin-top: 1.5rem;
}

.router-link:hover {
    color: #298499;
    text-decoration: none;
}

</style>