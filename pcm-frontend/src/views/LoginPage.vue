<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card shadow">
          <div class="card-header bg-primary text-white text-center">
            <h3>Đăng nhập CLB Vợt Thủ Phố Núi</h3>
          </div>
          <div class="card-body">
            <form @submit.prevent="login">
              <div class="mb-3">
                <label class="form-label">Email</label>
                <input v-model="email" type="email" class="form-control" placeholder="admin@pcm.com" required />
              </div>
              <div class="mb-3">
                <label class="form-label">Mật khẩu</label>
                <input v-model="password" type="password" class="form-control" placeholder="Admin@123" required />
              </div>
              <button type="submit" class="btn btn-primary w-100">Đăng nhập</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth';
import { useToast } from 'vue-toastification';

const email = ref('');
const password = ref('');
const router = useRouter();
const authStore = useAuthStore();
const toast = useToast();

const login = async () => {
  if (!email.value || !password.value) {
    toast.error('Vui lòng nhập đầy đủ thông tin!');
    return;
  }

  try {
    await authStore.login(email.value, password.value);
    toast.success('Đăng nhập thành công!');
    router.push('/dashboard');
  } catch (error) {
    toast.error('Đăng nhập thất bại!');
  }
};
</script>