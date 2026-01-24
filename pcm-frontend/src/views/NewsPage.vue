<template>
  <div class="container mt-4">
    <h3>Tin tức CLB</h3>

    <div v-for="n in news" :key="n.id" class="card mb-3">
      <div class="card-body">
        <h5>{{ n.title }}</h5>
        <p>{{ n.content }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const news = ref([]);

onMounted(async () => {
  const res = await api.get('/news');
  news.value = res.data;
});
</script>
