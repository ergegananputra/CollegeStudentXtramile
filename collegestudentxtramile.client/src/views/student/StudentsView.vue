<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

const students = ref([]);

onMounted(async () => {
  try {
    const response = await axios.get('/api/v1/students');
    students.value = response.data.data.items;
  } catch (error) {
    console.error('Error fetching students:', error);
  }
});
</script>

<template>
  <div>
    <h1>Students</h1>

    <table class="table table-striped">
      <thead>
        <tr class="table-secondary">
          <th scope="col">ID</th>
          <th scope="col">Full Name</th>
          <th scope="col">Age</th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="student in students" :key="student.id">
          <td>{{ student.id }}</td>
          <td>{{ student.fullName }}</td>
          <td>{{ student.age }}</td>
          <td>
            <router-link class="btn btn-secondary btn-sm" :to="`/students/${student.id}`"><i class="bi bi-eye"></i></router-link>
          </td>
        </tr>
      </tbody>
    </table>

  </div>
</template>
