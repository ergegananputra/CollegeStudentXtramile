import { createRouter, createWebHistory } from 'vue-router';
import StudentsView from '../views/student/StudentsView.vue';
import StudentDetailView from '../views/student/StudentDetailView.vue';
import StudentForm from '@/views/student/StudentForm.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: StudentsView,
  },
  {
    path: '/students',
    name: 'Students',
    component: StudentsView,
  },
  {
    path: '/students/new',
    name: 'CreateStudent',
    component: StudentForm,
    props: { isEditing: false }
  },
  {
    path: '/students/:id/edit',
    name: 'EditStudent',
    component: StudentForm,
    props: route => ({ isEditMode: true, studentId: route.params.id })
  },
  {
    path: '/students/:id',
    name: 'StudentDetail',
    component: StudentDetailView,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
