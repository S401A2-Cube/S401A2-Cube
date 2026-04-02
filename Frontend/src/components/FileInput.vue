<script setup>
import { defineEmits, defineProps } from 'vue';

const props = defineProps({
    label: String,
    accept: {
        type: String,
        default: 'image/*'
    },
    multiple: {
        type: Boolean,
        default: false
    }
});

defineEmits(['change']);
</script>

<template>
    <div class="file-input-container">
        <label class="file-label">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-upload-icon lucide-upload"><path d="M12 3v12"/><path d="m17 8-5-5-5 5"/><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/></svg>
            <span>{{ label }}</span>
            <input style="display: none;" :accept="accept" :multiple="multiple" type="file" @change="$emit('change', $event)" />
        </label>
    </div>
</template>

<style scoped>
.file-input-container {
    margin: 12px 0;
    text-align: left;
    width: 100%;
    min-width: 0;
}

.file-label {
    display: flex;
    align-items: center;
    gap: 0.75rem;
    border: 1px dashed #ccc;
    padding: 0.75rem 1rem;
    cursor: pointer;
    width: 100%;
    background: #fff;
}

.file-label input {
    width: 100%;
    display: block;
}
</style>
