import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '../views/LoginPage.vue';
import { useAuthStore } from '@/stores/auth';

const routes = [
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage
  },

  {
    path: '/dashboard',
    name: 'DashboardPage',
    component: () => import('../views/DashboardPage.vue'),
    meta: { requiresAuth: true }
  },

  // 🔹 ADMIN: quản lý thành viên
  {
  path: '/members',
  component: () => import('@/views/MembersPage.vue'),
  meta: { requiresAuth: true }
},
{
  path: '/news',
  component: () => import('@/views/NewsPage.vue'),
  meta: { requiresAuth: true }
},
  {
    path: '/courts',
    component: () => import('@/views/CourtsPage.vue'),
    meta: { requiresAuth: true }
  },
{
  path: '/bookings',
  component: () => import('@/views/BookingsPage.vue'),
  meta: { requiresAuth: true }
},
{
  path: '/matches',
  component: () => import('@/views/MatchesPage.vue'),
  meta: { requiresAuth: true, roles: ['Admin', 'Referee'] }
},
{
  path: '/transactions',
  component: () => import('@/views/TransactionsPage.vue'),
  meta: { requiresAuth: true, roles: ['Admin', 'Treasurer'] }
}
    ,

  {
    path: '/',
    redirect: '/login'
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

/* 🔐 ROUTER GUARD – BẮT BUỘC */
router.beforeEach((to, from, next) => {
  const auth = useAuthStore();

  // Chưa login
  if (to.meta.requiresAuth && !auth.token) {
    return next('/login');
  }

  // Có phân quyền role
  if (to.meta.roles && !to.meta.roles.includes(auth.role)) {
    return next('/dashboard');
  }

  next();
});

export default router;
