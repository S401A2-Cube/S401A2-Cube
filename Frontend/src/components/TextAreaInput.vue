<script setup>
import { computed, defineModel, defineProps } from 'vue';

const model = defineModel();

const props = defineProps({
    label: String,
    required: {
        type: Boolean,
        default: false
    },
    rows: {
        type: Number,
        default: 4
    },
    placeholder: {
        type: String,
        default: ' '
    }
});

const inputClasses = computed(() => ({
    'input-effect': true,
    'has-content': !!model.value
}));

const labelIndication = computed(() => props.required ? `${props.label} *` : `${props.label} (optionnel)`);
</script>

<template>
    <div class="input-container">
        <textarea
            v-model="model"
            :class="inputClasses"
            :rows="rows"
            :placeholder="placeholder"
            :required="required"
        ></textarea>
        <label>
            {{ labelIndication }}
        </label>
        <span class="focus-border"></span>
    </div>
</template>

<style scoped>
textarea:focus {
    outline: none;
}

.input-container {
    margin: 12px 0;
    position: relative;
    text-align: left;
    width: 100%;
    min-width: 0;
}

.input-effect {
    border: none;
    padding: 5px 0;
    background-color: transparent;
    width: 100%;
    text-align: left;
    line-height: 24px;
    resize: vertical;
}

.input-effect ~ label {
    position: absolute;
    left: 0;
    top: 9px;
    color: #aaa;
    transition: 0.4s;
    font-size: 16px;
    pointer-events: none;
}

.input-effect:focus ~ label,
.input-effect:not(:placeholder-shown) ~ label,
.input-effect.has-content ~ label {
    top: -16px;
    font-size: 14px;
    color: #2fb5d2;
}

.input-effect ~ .focus-border {
    position: absolute;
    bottom: -1px;
    left: 0;
    width: 100%;
    height: 1px;
    background-color: #ccc;
    transition: 0.4s;
    z-index: 1;
}

.input-effect:focus ~ .focus-border,
.input-effect:not(:placeholder-shown) ~ .focus-border,
.input-effect.has-content ~ .focus-border {
    height: 1px;
    background-color: #2fb5d2;
}
</style>
