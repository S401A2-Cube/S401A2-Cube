<script setup>
import { ref } from "vue";

const props = defineProps({
  name: { type: String, required: true },
  values: { type: Array, required: true },
  modelValue: { type: Array, default: () => [] }, 
  bikes: { type: Array, required: true },         
  matchOption: { type: Function, required: true },
  getLabel: { type: Function, default: (v) => v } 
});

const emit = defineEmits(["update:modelValue"]);
const isOpen = ref(false);

const toggleRotation = () => {
  isOpen.value = !isOpen.value;
};

const isSelected = (option) => {
  return props.modelValue.some(
    (selectedItem) => props.getLabel(selectedItem) === props.getLabel(option)
  );
};

const toggleSelection = (option) => {
  const selected = [...props.modelValue];
  
  const idx = selected.findIndex(
    (selectedItem) => props.getLabel(selectedItem) === props.getLabel(option)
  );
  
  if (idx > -1) {
    selected.splice(idx, 1);
  } else {
    selected.push(option);
  }
  
  emit("update:modelValue", selected);
};

const getCount = (option) => {
  return props.bikes.filter((bike) => props.matchOption(bike, option)).length;
};
</script>

<template>
  <div class="filter-group">
    <div class="side_filtre" @click="toggleRotation">
      <h2>{{ name }}</h2>
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24" height="24" viewBox="0 0 24 24"
        fill="none" stroke="currentColor" stroke-width="2"
        stroke-linecap="round" stroke-linejoin="round"
        class="chevron"
        :class="{ rotated: !isOpen }"
      >
        <path d="m18 15-6-6-6 6" />
      </svg>
    </div>

    <div v-show="isOpen" class="filtre-panel">
      <span
        v-for="(option, index) in values"
        :key="index"
        class="filter-btn"
        :class="{ selected: isSelected(option) }"
        @click.stop="toggleSelection(option)"
      >
        {{ getLabel(option) }} ({{ getCount(option) }})
      </span>
    </div>
  </div>
</template>

<style scoped>
.side_filtre {
	-webkit-box-pack: justify;
	-ms-flex-pack: justify;
	display: -webkit-box;
	display: -ms-flexbox;
	display: flex;
	font-size: 16px;
	font-style: italic;
	font-weight: 700;
	justify-content: space-between;
	text-transform: uppercase;
	width: 100%;
    cursor: pointer;
	padding-bottom: 20px;
	padding-top: 20px;
}

h2 {
  font-weight: bold;
  margin: 0;
  font-size: 16px;
}
.chevron {
  transition: transform 0.3s ease;
}
.rotated {
  transform: rotate(180deg);
}
.filtre-panel {
  display: flex;
  width: fit-content;
  flex-wrap: wrap;
  flex-direction: column;
  gap: 8px;
  padding: 10px 0 14px;
}
.filter-btn {
  border: 1.5px solid #ccc;
  border-radius: 4px;
  padding: 4px 10px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  user-select: none;
  transition: all 0.15s;
}
.filter-btn:hover {
  border-color: #e63000;
  color: #e63000;
}
.filter-btn.selected {
  background: #e63000;
  border-color: #e63000;
  color: #fff;
}
</style>