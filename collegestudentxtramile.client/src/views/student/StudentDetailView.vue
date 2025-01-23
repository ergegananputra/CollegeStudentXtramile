<template>
  <div>
    <h1>Student Detail</h1>
    <div v-if="student">
      <p>ID: {{ student.id }}</p>
      <p>First Name: {{ student.firstName }}</p>
      <p>Last Name: {{ student.lastName }}</p>
      <p>Date of Birth: {{ student.dateOfBirth }}</p>
    </div>
  </div>
</template>

<script>import axios from 'axios';

export default {
  name: 'StudentDetailView',
  data() {
    return {
      student: null,
    };
  },
  async created() {
    const studentId = this.$route.params.id;
    try {
      const response = await axios.get(`/api/v1/students/${studentId}`);
      this.student = response.data.data;
    } catch (error) {
      console.error('Error fetching student:', error);
    }
  },
};</script>
