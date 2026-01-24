<template>
  <div class="container mt-4">
    <h3>Danh sách thành viên</h3>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>Tên</th>
          <th>Email</th>
          <th>Rank</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="m in members" :key="m.id">
          <td>{{ m.fullName }}</td>
          <td>{{ m.email }}</td>
          <td>{{ m.rankLevel }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import api from '@/services/api';

const members = ref([]);

onMounted(async () => {
  const res = await api.get('/members');
  members.value = res.data;
});
</script>
