<template>
  <div class="container mt-4">
    <h3>Danh sách trận đấu</h3>

    <table class="table">
      <tr v-for="m in matches" :key="m.id">
        <td>{{ m.playerA }} vs {{ m.playerB }}</td>
        <td>{{ m.winner }}</td>
      </tr>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const matches = ref([]);

onMounted(async () => {
  const res = await api.get('/challenges');
  matches.value = res.data;
});

</script>
