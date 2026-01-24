<template>
  <div class="container mt-5">
    <h1 class="text-center mb-5">Dashboard CLB Vợt Thủ Phố Núi</h1>

<!-- 🔥 MODULE QUICK ACCESS -->
<div class="row mb-5">
  <div class="col-md-3 mb-3" @click="$router.push('/members')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>👥</h3>
        <h6>Members</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/news')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>📰</h3>
        <h6>News</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/courts')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>🏟</h3>
        <h6>Courts</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/bookings')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>📅</h3>
        <h6>Bookings</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/matches')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>⚽</h3>
        <h6>Matches</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/transactions')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>💰</h3>
        <h6>Transactions</h6>
      </div>
    </div>
  </div>

  <div class="col-md-3 mb-3" @click="$router.push('/categories')">
    <div class="card shadow text-center module-card">
      <div class="card-body">
        <h3>📂</h3>
        <h6>Categories</h6>
      </div>
    </div>
  </div>
</div>

    <div v-if="loading" class="text-center py-5">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Đang tải dữ liệu...</span>
      </div>
    </div>

    <div v-else>
      <!-- Top 5 Ranking -->
      <div class="row mb-5">
        <div class="col-12">
          <div class="card shadow">
            <div class="card-header bg-success text-white">
              <h5 class="mb-0">Top 5 Thành viên xếp hạng cao nhất</h5>
            </div>
            <div class="card-body p-4">
              <table class="table table-hover table-bordered">
                <thead class="table-light">
                  <tr>
                    <th>#</th>
                    <th>Tên</th>
                    <th>Rank DUPR</th>
                    <th>Số trận thắng</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(member, index) in topRanking" :key="member.id">
                    <td>{{ index + 1 }}</td>
                    <td>{{ member.fullName }}</td>
                    <td>{{ member.rankLevel.toFixed(1) }}</td>
                    <td>{{ member.winMatches }} / {{ member.totalMatches }}</td>
                  </tr>
                  <tr v-if="topRanking.length === 0">
                    <td colspan="4" class="text-center text-muted">Chưa có dữ liệu xếp hạng</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>

      <!-- Tin ghim -->
      <div class="row mb-5">
        <div class="col-12">
          <div class="card shadow">
            <div class="card-header bg-info text-white">
              <h5 class="mb-0">Thông báo ghim</h5>
            </div>
            <div class="card-body">
              <div v-for="news in pinnedNews" :key="news.id" class="alert alert-info mb-3">
                <strong>{{ news.title }}</strong>
                <p class="mb-0">{{ news.content }}</p>
              </div>
              <p v-if="pinnedNews.length === 0" class="text-center text-muted">Chưa có thông báo ghim nào</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Summary Quỹ -->
      <div class="row mb-5">
        <div class="col-12">
          <div class="card shadow">
            <div class="card-header bg-warning text-dark">
              <h5 class="mb-0">Tình hình tài chính</h5>
            </div>
            <div class="card-body">
              <div class="row text-center">
                <div class="col-md-4 mb-3">
                  <h6>Tổng thu</h6>
                  <h4 class="text-success fw-bold">{{ summary.totalIncome.toLocaleString('vi-VN') }} ₫</h4>
                </div>
                <div class="col-md-4 mb-3">
                  <h6>Tổng chi</h6>
                  <h4 class="text-danger fw-bold">{{ summary.totalExpense.toLocaleString('vi-VN') }} ₫</h4>
                </div>
                <div class="col-md-4 mb-3">
                  <h6>Số dư quỹ</h6>
                  <h4 :class="summary.balance < 0 ? 'text-danger fw-bold' : 'text-success fw-bold'">
                    {{ summary.balance.toLocaleString('vi-VN') }} ₫
                  </h4>
                  <div v-if="summary.isNegative" class="alert alert-danger mt-2">
                    <strong>CẢNH BÁO:</strong> Quỹ đang âm! Cần bổ sung ngay!
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Nút đăng xuất -->
      <div class="text-center mt-5">
        <button 
          class="btn btn-danger btn-lg px-5 py-3"
          @click="logout"
          :disabled="isLoggingOut"
        >
          <span v-if="isLoggingOut" class="spinner-border spinner-border-sm me-2"></span>
          Đăng xuất
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth';
import { useToast } from 'vue-toastification';
import api from '@/services/api';

const router = useRouter();
const authStore = useAuthStore();
const toast = useToast();

const topRanking = ref([]);
const pinnedNews = ref([]);
const summary = ref({ totalIncome: 0, totalExpense: 0, balance: 0, isNegative: false });
const loading = ref(true);
const isLoggingOut = ref(false);

// Nếu chưa login, tự động redirect về login
if (!authStore.token) {
  router.push('/login');
}

const fetchData = async () => {
  try {
    // Top 5 ranking
    const rankingRes = await api.get('/members/top-ranking?limit=5');
    topRanking.value = rankingRes.data || [];

    // Tin ghim
    const newsRes = await api.get('/news?isPinned=true');
    pinnedNews.value = newsRes.data || [];

    // Summary quỹ
    const summaryRes = await api.get('/transactions/summary');
    summary.value = summaryRes.data;
  } catch (error) {
    console.error('Lỗi tải dữ liệu dashboard:', error);
    console.error('Chi tiết:', error.response?.data || error.message);
    toast.error('Lỗi tải dữ liệu: ' + (error.response?.data?.message || error.message));
  } finally {
    loading.value = false;
  }
};

const logout = async () => {
  isLoggingOut.value = true;
  try {
    authStore.logout();
    toast.success('Đăng xuất thành công!');
    router.push('/login');
  } catch (error) {
    toast.error('Đăng xuất thất bại!');
  } finally {
    isLoggingOut.value = false;
  }
};

onMounted(() => {
  fetchData();
});
</script>

<style scoped>
.card-header {
  font-weight: bold;
}
.alert-info {
  margin-bottom: 1rem;
}
</style>