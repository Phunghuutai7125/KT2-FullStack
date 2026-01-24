<template>
  <div class="container mt-4">
    <h3>Giao dịch tài chính</h3>

    <table class="table table-striped">
      <tr v-for="t in transactions" :key="t.id">
        <td>{{ t.type }}</td>
        <td>{{ t.amount }}</td>
        <td>{{ t.description }}</td>
      </tr>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const transactions = ref([]);

onMounted(async () => {
  const res = await api.get('/transactions');
  transactions.value = res.data;
});
</script>
