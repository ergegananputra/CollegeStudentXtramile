import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import StudentsView from '../views/student/StudentsView.vue';
import StudentDetailView from '../views/student/StudentDetailView.vue';

const routes = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/students', name: 'Students', component: StudentsView },
  { path: '/students/:id', name: 'StudentDetail', component: StudentDetailView },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
