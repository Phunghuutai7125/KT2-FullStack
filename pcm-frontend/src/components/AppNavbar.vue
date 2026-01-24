<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <a class="navbar-brand" href="#">PCM Club</a>

    <ul class="navbar-nav ms-auto">
      <li class="nav-item">
        <RouterLink class="nav-link" to="/dashboard">Dashboard</RouterLink>
      </li>

      <li class="nav-item">
        <RouterLink class="nav-link" to="/members">Members</RouterLink>
      </li>

      <li class="nav-item">
        <RouterLink class="nav-link" to="/news">News</RouterLink>
      </li>

      <li class="nav-item" v-if="isAdmin">
        <RouterLink class="nav-link" to="/courts">Courts</RouterLink>
      </li>

      <li class="nav-item">
        <RouterLink class="nav-link" to="/bookings">Bookings</RouterLink>
      </li>

      <li class="nav-item" v-if="isReferee">
        <RouterLink class="nav-link" to="/matches">Matches</RouterLink>
      </li>

      <li class="nav-item" v-if="isAdmin">
        <RouterLink class="nav-link" to="/transactions">Finance</RouterLink>
      </li>
    </ul>
  </nav>
</template>

<script setup>
import { computed } from 'vue';
import { useAuthStore } from '@/stores/auth';

const auth = useAuthStore();

const isAdmin = computed(() => auth.roles.includes('Admin'));
const isReferee = computed(() => auth.roles.includes('Referee'));
</script>
