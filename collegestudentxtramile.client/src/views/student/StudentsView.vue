<template>
  <div>
    <h1>Students</h1>
    <ul>
      <li v-for="student in students" :key="student.id">
        <router-link :to="`/students/${student.id}`">{{ student.firstName }} {{ student.lastName }}</router-link>
      </li>
    </ul>
  </div>
</template>

<script>import axios from 'axios';

export default {
  name: 'StudentsView',
  data() {
    return {
      students: [],
    };
  },
  async created() {
    try {
      const response = await axios.get('/api/v1/students');
      this.students = response.data.data.items;
    } catch (error) {
      console.error('Error fetching students:', error);
    }
  },
};</script>
