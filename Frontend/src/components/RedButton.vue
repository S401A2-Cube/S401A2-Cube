<script setup>
import { defineProps } from 'vue';

const props = defineProps({
    type: {
        type: String,
        default: 'button'
    },
});
</script>

<template>
    <button :type="type">
        <slot>Valider</slot>
    </button>
</template>

<style scoped>
button {
    background-color: var(--pink-cube);
    color: #fff;
    padding: 0.85rem 1.5rem;
    border: none;
    cursor: pointer;
    transition: 0.4s;
    width: 200px;
    text-align: center;
    clip-path: polygon(10px 0%, 100% 0%, 100% 100%, 0% 100%);
}
button:hover {
    /* clip-path: polygon(0% 0%, calc(100% - 10px) 0%, 100% 100%, 0% 100%); */
    clip-path: polygon(20px 0%, 100% 0%, 100% 100%, 0% 100%);
}

/* .btn-disabled {
    background-color: #ff7c78;
} */
</style>