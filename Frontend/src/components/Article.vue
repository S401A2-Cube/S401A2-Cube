<script setup>
import router from '@/router';



defineProps({
  id: {
    type:Number,
    required:true
  },
  articleName: {
    type: String,
    required: true
  },
  availableOnline: {
    type: Boolean,
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
});



</script>

<template>
    <div class="item-container" @click="router.push({path: 'detail/article/'+id});">
        <img class="item-image" :src=imageURL>
        <div class="item-content">
          <p class="item-name">{{articleName}}</p>
          <div class="item-details">
            <div class="item-info-aside">
              <div v-if="availableOnline" class="item-available-icon yes" ><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-check-icon lucide-check"><path d="M20 6 9 17l-5-5"/></svg></div>
              <div v-if="!availableOnline" class="item-available-icon no" ><svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-x-icon lucide-x"><path d="M18 6 6 18"/><path d="m6 6 12 12"/></svg></div>
              <p class="item-available" :class="{itemOffline: !availableOnline}">{{availableOnline ? "Disponible en ligne" : "Indisponible en ligne"}}</p>
            </div>
            <div class="item-info-aside item-magasin">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-map-pin-icon lucide-map-pin"><path d="M20 10c0 4.993-5.539 10.193-7.399 11.799a1 1 0 0 1-1.202 0C9.539 20.193 4 14.993 4 10a8 8 0 0 1 16 0"/><circle cx="12" cy="10" r="3"/></svg>
              <a class="item-link" href="#">Choisir un magasin</a>
            </div>
          </div>
          <p class="item-price">{{price}}€</p>
        </div>
        <div class="item-button">
          <div>
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-chevrons-right-icon lucide-chevrons-right"><path d="m6 17 5-5-5-5"/><path d="m13 17 5-5-5-5"/></svg>
            <a>Voir le produit</a>
          </div>
        </div>
    </div>
</template>

<style scoped>
.item-container
{
    width: 25%;
    display: flex;
    justify-content: center;
    align-items: center;
    flex-direction: column;
    text-align: left;
    gap: 1rem;
    border: 1px solid lightgray;
    cursor: pointer;
}

.item-container p 
{
    width: 100%;
    text-align: left;
}

.item-name
{
    font-style: italic;
    font-weight: bold;
    text-transform: uppercase;
    font-size: larger;
}

.item-details
{
  width: 100%;
  padding: 0 0 0 .25rem;
  gap: .5rem;
  display: flex;
  justify-content: left;
  align-items: center;
  flex-direction: column;
}

.item-magasin 
{
  color: rgb(87, 87, 87);
  transition: all 0.2s ease;
}

.item-magasin:hover
{
  color: rgb(87, 87, 87);
}

.item-link 
{
  text-decoration: underline;
  color: inherit;
}

.itemOffline 
{
    color: gray;
}

.item-available-icon 
{
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.item-available-icon.yes 
{
  color: green;
}
.item-available-icon.no 
{
  color: gray;
}

.item-price
{
    font-style: italic;
    font-weight: bold;
}

.item-info-aside 
{
  width: 100%;
  display: flex;
  flex-direction: row;
  gap: .25rem;
  align-items: center;
  justify-content: left;
}

.item-image, .item-content
{
  width: 100%;
  padding: 1rem 1rem 0 1rem;
}

.item-image
{
  padding: 2rem 2rem 0 2rem;
}

.item-content 
{
  display: flex;
  align-items: center;
  justify-content: left;
  flex-direction: column;
  gap: 1rem;
}

.item-button
{
  width: 100%;
  display: flex;
  justify-content: right;
  align-items: center;
}

.item-button div
{
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: black;
  color: white;
  padding-left: 1rem;
  cursor: pointer;
	-webkit-clip-path: polygon(15px 0,100% 0,100% 100%,0 100%);
	clip-path: polygon(15px 0,100% 0,100% 100%,0 100%);

}

.item-button > div > svg
{
  transform: none;
  transition: all 0.2s ease;
}

.item-button > div:hover > svg
{
  transform: translateX(.25rem);
}

.item-button > div > a
{
  font-size: 0.9rem;
  text-transform: uppercase;
  font-weight: bold;
  font-style: italic;
  padding: .5rem 1rem .5rem .5rem;
}

.item-button > div > a:hover
{
  text-decoration: underline;
}

</style>