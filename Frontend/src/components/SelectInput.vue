<script setup>
import { computed, defineModel, defineProps, onBeforeUnmount, onMounted, ref } from 'vue';

const model = defineModel();

const props = defineProps({
    label: String,
    required: {
        type: Boolean,
        default: false
    },
    options: {
        type: Array,
        default: () => []
    },
    optionLabel: {
        type: String,
        default: 'label'
    },
    optionValue: {
        type: String,
        default: 'value'
    },
    placeholder: {
        type: String,
        default: 'Selectionner'
    },
    readonly: {
        type: Boolean,
        default: false
    },
    disabled: {
        type: Boolean,
        default: false
    }
});

const rootRef = ref(null);
const isOpen = ref(false);
const activeIndex = ref(-1);

const labelIndication = computed(() => props.required ? `${props.label} *` : `${props.label} (optionnel)`);

const selectedOption = computed(() => props.options.find((option) => option[props.optionValue] === model.value) || null);
const selectedLabel = computed(() => selectedOption.value ? selectedOption.value[props.optionLabel] : props.placeholder);
const hasValue = computed(() => model.value !== null && model.value !== undefined && model.value !== '');

const openDropdown = () => {
    if (props.disabled || props.readonly) {
        return;
    }

    isOpen.value = true;
    activeIndex.value = props.options.findIndex((option) => option[props.optionValue] === model.value);
    if (activeIndex.value < 0) {
        activeIndex.value = 0;
    }
};

const closeDropdown = () => {
    isOpen.value = false;
};

const toggleDropdown = () => {
    if (isOpen.value) {
        closeDropdown();
        return;
    }

    openDropdown();
};

const chooseOption = (option) => {
    model.value = option[props.optionValue];
    closeDropdown();
};

const handleKeydown = (event) => {
    if (props.disabled || props.readonly) {
        return;
    }

    if (!isOpen.value && (event.key === 'ArrowDown' || event.key === 'Enter' || event.key === ' ')) {
        event.preventDefault();
        openDropdown();
        return;
    }

    if (!isOpen.value) {
        return;
    }

    if (event.key === 'Escape') {
        event.preventDefault();
        closeDropdown();
        return;
    }

    if (event.key === 'ArrowDown') {
        event.preventDefault();
        activeIndex.value = Math.min(activeIndex.value + 1, props.options.length - 1);
        return;
    }

    if (event.key === 'ArrowUp') {
        event.preventDefault();
        activeIndex.value = Math.max(activeIndex.value - 1, 0);
        return;
    }

    if (event.key === 'Enter' && props.options[activeIndex.value]) {
        event.preventDefault();
        chooseOption(props.options[activeIndex.value]);
    }
};

const handleDocumentClick = (event) => {
    if (!rootRef.value) {
        return;
    }

    if (!rootRef.value.contains(event.target)) {
        closeDropdown();
    }
};

onMounted(() => {
    document.addEventListener('click', handleDocumentClick);
});

onBeforeUnmount(() => {
    document.removeEventListener('click', handleDocumentClick);
});
</script>

<template>
    <div ref="rootRef" class="select-container">
        <button
            type="button"
            class="select-trigger"
            :class="{ open: isOpen, filled: hasValue, disabled: disabled || readonly }"
            :disabled="disabled || readonly"
            :aria-expanded="isOpen"
            aria-haspopup="listbox"
            @click="toggleDropdown"
            @keydown="handleKeydown"
        >
            <span class="select-label">{{ labelIndication }}</span>
            <span class="select-value">{{ selectedLabel }}</span>
            <span class="select-icon">▾</span>
        </button>

        <div v-if="isOpen" class="select-menu" role="listbox">
            <button
                v-for="(option, index) in options"
                :key="option[optionValue]"
                type="button"
                class="select-option"
                :class="{ active: model === option[optionValue], focused: activeIndex === index }"
                role="option"
                :aria-selected="model === option[optionValue]"
                @mouseenter="activeIndex = index"
                @click="chooseOption(option)"
            >
                {{ option[optionLabel] }}
            </button>
        </div>

        <input v-if="required" type="text" class="hidden-validator" tabindex="-1" :value="hasValue ? 'ok' : ''" :required="required" readonly />
    </div>
</template>

<style scoped>
.select-container {
    /* margin: 12px 0; */
    position: relative;
    text-align: left;
    width: 100%;
    min-width: 0;
}

.field:has(.select-container) {
    background-color: rebeccapurple;
    margin: 0px 0px;
}

.select-trigger {
    width: 100%;
    border: 1px solid #bdbdbd;
    background: #fff;
    border-radius: 0;
    padding: 0.52rem 0.75rem;
    cursor: pointer;
    display: grid;
    grid-template-columns: 1fr auto;
    grid-template-areas:
        'label icon'
        'value icon';
    gap: 0.08rem 0.55rem;
    align-items: center;
    text-align: left;
    transition: border-color 0.2s ease, box-shadow 0.2s ease, transform 0.15s ease, background-color 0.2s ease;
    color: #111;
}

.select-trigger:hover {
    border-color: #8f8f8f;
}

.select-trigger.open {
    border-color: #2fb5d2;
    box-shadow: 0 0 0 3px rgba(47, 181, 210, 0.12);
}

.select-trigger.filled .select-value {
    color: #111;
}

.select-trigger.disabled {
    cursor: not-allowed;
    opacity: 0.65;
}

.select-label {
    grid-area: label;
    font-size: 0.68rem;
    letter-spacing: 0.08em;
    text-transform: uppercase;
    color: #666;
}

.select-value {
    grid-area: value;
    font-size: 0.93rem;
    color: #8a8a8a;
    line-height: 1.1;
}

.select-icon {
    grid-area: icon;
    font-size: 0.9rem;
    color: #333;
    transition: transform 0.2s ease;
}

.select-trigger.open .select-icon {
    transform: rotate(180deg);
}

.select-menu {
    position: absolute;
    left: 0;
    right: 0;
    z-index: 20;
    margin-top: 0.45rem;
    border: 1px solid #bdbdbd;
    border-radius: 0;
    background: #fff;
    box-shadow: none;
    overflow: hidden;
    max-height: 280px;
    overflow-y: auto;
}

.select-option {
    width: 100%;
    padding: 0.8rem 0.95rem;
    border: none;
    background: #fff;
    text-align: left;
    cursor: pointer;
    color: #111;
    transition: background-color 0.15s ease, color 0.15s ease;
}

.select-option:hover,
.select-option.focused {
    background: #efefef;
}

.select-option.active {
    background: #2fb5d2;
    color: #fff;
}

.hidden-validator {
    position: absolute;
    opacity: 0;
    pointer-events: none;
    height: 0;
    width: 0;
}

@media (max-width: 760px) {
    .select-container {
        width: 100%;
    }
}
</style>
