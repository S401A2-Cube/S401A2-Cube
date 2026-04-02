<script setup>
import { computed, onMounted, ref } from 'vue';
import axios from 'axios';
import { getAuthHeaders, useUtilsStore } from '@/stores/utils';
import AdminProductPanel from '@/components/admin/AdminProductPanel.vue';
import AdminCataloguePanel from '@/components/admin/AdminCataloguePanel.vue';
import AdminEditBikePanel from '@/components/admin/AdminEditBikePanel.vue';

const utils = useUtilsStore();

const sections = [
  { id: 'product', label: 'Article' },
  { id: 'catalogue', label: 'Catalogue' },
  { id: 'edit', label: 'Edition velo' }
];

const currentSection = ref('product');
const authHeaders = computed(() => getAuthHeaders());

const categories = ref([]);
const modeles = ref([]);
const couleurs = ref([]);
const tailles = ref([]);
const cadres = ref([]);
const geometries = ref([]);
const createdArticle = ref(null);

const velos = ref([]);
const selectedBike = ref(null);

const loadLookups = async () => {
  try {
    const [categoriesRes, modelesRes, couleursRes, taillesRes, cadresRes, geometriesRes, velosRes] = await Promise.all([
      axios.get(utils.url + 'Categories'),
      axios.get(utils.url + 'Modeles'),
      axios.get(utils.url + 'Couleurs'),
      axios.get(utils.url + 'Tailles'),
      axios.get(utils.url + 'Cadres'),
      axios.get(utils.url + 'Geometries'),
      axios.get(utils.url + 'Velos')
    ]);

    categories.value = categoriesRes.data || [];
    modeles.value = modelesRes.data || [];
    couleurs.value = couleursRes.data || [];
    tailles.value = taillesRes.data || [];
    cadres.value = cadresRes.data || [];
    geometries.value = geometriesRes.data || [];
    velos.value = velosRes.data || [];
  } catch (error) {
    console.error('Erreur lors du chargement des donnees:', error);
  }
};

const handleArticleCreated = (article) => {
  createdArticle.value = article;
};

const handleBikeSelected = (velo) => {
  selectedBike.value = velo;
  currentSection.value = 'edit';
};

const handleBikeUpdated = async () => {
  const selectedBikeId = selectedBike.value?.idVelo;
  await loadLookups();

  if (!selectedBikeId) {
    return;
  }

  selectedBike.value = velos.value.find((bike) => bike.idVelo === selectedBikeId) || null;
};

const activeComponent = computed(() => {
  if (currentSection.value === 'catalogue') {
    return AdminCataloguePanel;
  }

  if (currentSection.value === 'edit') {
    return AdminEditBikePanel;
  }

  return AdminProductPanel;
});

onMounted(loadLookups);
</script>

<template>
  <main class="admin-layout">
    <aside class="side-menu">
      <h2>Administration</h2>

      <button
        v-for="section in sections"
        :key="section.id"
        type="button"
        :class="['menu-item', { active: currentSection === section.id }]"
        @click="currentSection = section.id"
      >
        {{ section.label }}
      </button>

      <div v-if="createdArticle?.articleId" class="article-badge">
        <span>Article actif</span>
        <strong>#{{ createdArticle.articleId }}</strong>
        <small>{{ createdArticle.reference }}</small>
      </div>

      <div class="lookup-box">
        <span>{{ categories.length }} categories</span>
        <span>{{ modeles.length }} modeles</span>
        <span>{{ couleurs.length }} couleurs</span>
      </div>
    </aside>

    <section class="content-panel">
      <component
        :is="activeComponent"
        :categories="categories"
        :modeles="modeles"
        :colors="couleurs"
        :tailles="tailles"
        :cadres="cadres"
        :geometries="geometries"
        :velos="velos"
        :article="createdArticle"
        :selected-bike="selectedBike"
        :article-reference="createdArticle?.reference || ''"
        :utils-url="utils.url"
        :auth-headers="authHeaders"
        @created="handleArticleCreated"
        @updated="handleBikeUpdated"
        @select-bike="handleBikeSelected"
      />
    </section>
  </main>
</template>

<style scoped>
.admin-layout {
  display: grid;
  grid-template-columns: 260px 1fr;
  gap: 1.25rem;
  width: min(1180px, 95vw);
  margin: 1.75rem auto 4rem;
  align-items: start;
}

.side-menu {
  position: sticky;
  top: 95px;
  background: #111;
  color: #fff;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.55rem;
}

.side-menu h2,
.side-menu p,
.side-menu label,
.side-menu strong,
.side-menu small,
.side-menu span {
  text-align: left;
}

.side-menu h2 {
  font-size: 1.4rem;
}

.side-menu p {
  color: #c6c6c6;
  font-size: 0.92rem;
  margin-bottom: 0.35rem;
}

.menu-item {
  border: 1px solid #3f3f3f;
  background: transparent;
  color: #fff;
  text-align: left;
  padding: 0.55rem 0.65rem;
  cursor: pointer;
}

.menu-item.active,
.menu-item:hover {
  background: #fff;
  color: #000;
}

.article-badge {
  margin-top: 0.65rem;
  border: 1px solid #4f4f4f;
  padding: 0.55rem;
  display: flex;
  flex-direction: column;
  gap: 0.2rem;
}

.article-badge strong {
  font-size: 1.15rem;
}

.lookup-box {
  margin-top: 0.65rem;
  display: flex;
  flex-wrap: wrap;
  gap: 0.45rem;
}

.lookup-box span {
  background: #1d1d1d;
  border: 1px solid #444;
  padding: 0.25rem 0.55rem;
  font-size: 0.82rem;
}

.content-panel {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.hero {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  background: #fff;
  border: 1px solid #ddd;
  box-shadow: var(--card-shadow);
  padding: 1.2rem;
}

.hero h1,
.hero p,
.hero .eyebrow {
  text-align: left;
}

.eyebrow {
  font-size: 0.8rem;
  text-transform: uppercase;
  letter-spacing: 0.12em;
  color: #777;
  margin-bottom: 0.35rem;
}

.hero h1 {
  font-size: 2.3rem;
  line-height: 1.05;
  margin-bottom: 0.75rem;
}

.hero p {
  color: #333;
  max-width: 60ch;
}

.feedback {
  margin-top: 0.5rem;
  padding: 0.65rem 0.75rem;
  font-weight: 600;
  width: fit-content;
  max-width: 100%;
}

.feedback.success {
  background: #dcf5e6;
  color: #0b6f2f;
}

.feedback.error {
  background: #ffe0e0;
  color: #991b1b;
}

.feedback.neutral {
  background: #ececec;
  color: #444;
}

@media (max-width: 1024px) {
  .admin-layout {
    grid-template-columns: 1fr;
  }

  .side-menu {
    position: static;
  }

  .hero {
    flex-direction: column;
  }
}
</style>
