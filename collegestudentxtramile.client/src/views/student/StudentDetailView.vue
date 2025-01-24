<script setup>
import { ref, onMounted, computed } from 'vue';
import axios from 'axios';
import { useRoute } from 'vue-router';

const student = ref(null);
const route = useRoute();

onMounted(async () => {
  const studentId = route.params.id;
  try {
    const response = await axios.get(`/api/v1/students/${studentId}`);
    student.value = response.data.data;
  } catch (error) {
    console.error('Error fetching student:', error);
  }
});

const formattedDateOfBirth = computed(() => {
  if (!student.value) return '';
  const date = new Date(student.value.dateOfBirth);
  return date.toLocaleDateString('en-ID', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
});
</script>

<template>
  <div>

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <router-link to="/students" class="breadcrumb-item">Students</router-link>
        <li class="breadcrumb-item active" aria-current="page" v-if="student">Student Detail Information</li>
      </ol>
    </nav>

    <div class="row">
      <div class="col">
        <i class="bi bi-person-circle"></i> Student Detail Information
      </div>

      <!-- float right icons -->
      <div class="col text-end">
        <router-link v-if="student" :to="`/students/${student.id}/edit`" class="btn btn-primary btn-sm">
          <i class="bi bi-pencil"></i> Edit
        </router-link>
      </div>

    </div>
    <hr>

    <div v-if="student" class="fs-5 fw-medium">

      <h2> {{ student.fullName }} </h2>

      <div class="row">
        <div class="col-4 col-md-3 col-lg-3">
          <i class="bi bi-person-badge-fill"></i> Student ID
        </div> :
        <div class="col">
          {{ student.id }}
        </div>
      </div>

      <div class="row">
        <div class="col-4 col-md-3 col-lg-3">
          <i class="bi bi-calendar-event-fill"></i> Date of Birth
        </div> :
        <div class="col">
          {{ formattedDateOfBirth }} ({{ student.age }} years)
        </div>
      </div>

      <hr>

      <div class="row">
        <div class="col-4 col-md-3 col-lg-3">
          First Name
        </div> :
        <div class="col">
          {{ student.firstName }}
        </div>
      </div>

      <div class="row">
        <div class="col-4 col-md-3 col-lg-3">
          Last Name
        </div> :
        <div class="col">
          {{ student.lastName }}
        </div>
      </div>

    </div>
  </div>
</template>

