import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5262/api',  // Thay 5262 bằng port backend của bạn (kiểm tra trong Visual Studio)
  headers: {
    'Content-Type': 'application/json'
  }
});

// Tự động thêm token vào header
api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Xử lý lỗi 401
api.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      localStorage.removeItem('token');
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default api;