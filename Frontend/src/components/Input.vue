<script setup>
import { computed, defineModel, defineProps } from 'vue';

const model = defineModel();
const props = defineProps({
    label: String,
    type: {
        type: String,
        default: 'text'
    },
    required: {
        type: Boolean,
        default: false
    }
});

const inputClasses = computed(() => {
    return {
        'input-effect': true,
        'has-content': props.type === 'date' && model.value
    };
});
</script>

<template>
    <div class="input-container">
        <input 
            :type="type"
            v-model="model"
            :class="inputClasses"
            placeholder=" "
            :required="required">
        <label>
            {{ label }}
        </label>
        <span class="focus-border"></span>
    </div>
</template>

<style scoped>
input:focus {
    outline: none;
}

.input-container {
    margin: 20px 0; 
    position: relative;
    text-align: left;
    width: 400px;
}

.input-effect {
    border: none; 
    padding: 5px 0; 
    border-bottom: none; 
    background-color: transparent;
    width: 100%;
    text-align: left;
    line-height: 24px;
    border: none;
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

input[type="date"].input-effect::-webkit-datetime-edit {
    opacity: 0;
    transition: opacity 0.3s;
}

input[type="date"].input-effect::-webkit-calendar-picker-indicator {
    opacity: 0;
    transition: opacity 0.3s;
}

input[type="date"].input-effect:focus::-webkit-datetime-edit,
input[type="date"].input-effect.has-content::-webkit-datetime-edit,
input[type="date"].input-effect:focus::-webkit-calendar-picker-indicator,
input[type="date"].input-effect.has-content::-webkit-calendar-picker-indicator {
    opacity: 1;
}

input[type="date"].input-effect:not(:focus):not(.has-content) ~ label {
    top: 9px;
    font-size: 16px;
    color: #aaa;
}

input[type="date"].input-effect:not(:focus):not(.has-content) ~ .focus-border {
    background-color: #ccc;
}
</style>