<script setup>
import Article from '@/components/Article.vue';


</script>


<template>
    <main>
        <Article 
            :id=1
            article-name="Attain Race"
            :available-online="true"
            :price=1299
            image-u-r-l="https://www.cubebikes.fr/38929-pdt_1920/attain.jpg"    
            />
            
        <Article 
            :id=2
            article-name="Attain SLX"
            :available-online="false"
            :price=1499
            image-u-r-l="https://www.cubebikes.fr/38946-pdt_1920/attain.jpg"
            
        />


    </main>
</template>

<style scoped>
    main
    {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 2rem;

    }
</style>