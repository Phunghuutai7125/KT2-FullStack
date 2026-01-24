import { defineStore } from 'pinia';
import api from '@/services/api';  // Sẽ tạo ở bước sau

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null,
    token: localStorage.getItem('token') || null,
    roles: []
  }),
  actions: {
    async login(email, password) {
      try {
        const res = await api.post('/auth/login', { email, password });
        this.token = res.data.token;
        localStorage.setItem('token', this.token);
        this.user = res.data;
        this.roles = res.data.roles || [];
        return true;
      } catch (error) {
        console.error('Login error:', error);
        return false;
      }
    },
    logout() {
      this.user = null;
      this.token = null;
      this.roles = [];
      localStorage.removeItem('token');
    },
    isAdmin() {
      return this.roles.includes('Admin');
    }
  }
});