<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const courts = ref([]);

onMounted(async () => {
  const res = await api.get('/courts');
  courts.value = res.data;
});
</script>

<template>
  <div class="container mt-4">
    <h3>Danh sách sân</h3>

    <ul class="list-group">
      <li
        v-for="c in courts"
        :key="c.id"
        class="list-group-item d-flex justify-content-between align-items-center"
      >
        <div>
          <strong>{{ c.name }}</strong><br />
          <small class="text-muted">{{ c.description }}</small>
        </div>

        <span
          class="badge"
          :class="c.isActive ? 'bg-success' : 'bg-secondary'"
        >
          {{ c.isActive ? 'Hoạt động' : 'Ngưng' }}
        </span>
      </li>
    </ul>
  </div>
</template>

