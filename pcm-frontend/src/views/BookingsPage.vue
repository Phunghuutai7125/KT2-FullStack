<template>
  <div class="container mt-4">
    <h3>Lịch đặt sân của tôi</h3>

    <ul class="list-group">
      <li v-for="b in bookings" :key="b.id" class="list-group-item">
        {{ b.courtName }} | {{ b.startTime }} - {{ b.endTime }}
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const bookings = ref([]);

onMounted(async () => {
  try {
    const res = await api.get('/bookings');
    bookings.value = res.data;
  } catch (err) {
    console.error('Lỗi load bookings', err);
  }
});
</script>
