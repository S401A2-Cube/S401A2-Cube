<script setup>
defineProps({
  label: {
    type: String,
    required: true
  }
});
</script>

<template>
  <li class="detail_table_row">
    <div class="aside detail_table_element">
      <p class="bold">{{ label }}</p>
      <div class="content">
        <slot></slot>
      </div>
    </div>
  </li>
</template>

<style scoped>
li {
  list-style: none;
}

p {
  text-transform: uppercase;
  margin: 0;
}

.bold {
  font-weight: bold;
}

.aside {
  display: flex;
  gap: 1rem;
  flex-direction: row;
}

.detail_table_element > *:nth-child(1) {
  width: 20%;
}

.detail_table_element > *:nth-child(2) {
  width: 80%;
}

.detail_table_element > * {
  text-align: left;
}

.detail_table_row {
  padding: 1rem;
  border-bottom: 1px solid lightgray;
}

.content * {
    text-align: left;
}
</style>