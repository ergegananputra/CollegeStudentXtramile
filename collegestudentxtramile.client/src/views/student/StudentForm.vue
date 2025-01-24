<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';

const props = defineProps({
  isEditMode: Boolean,
  studentId: String
});

const studentId = ref(props.studentId || '');
const firstName = ref('');
const lastName = ref('');
const fullName = ref('');
const dob = ref('');
const errors = ref({});

const router = useRouter();

onMounted(async () => {
  if (props.isEditMode && props.studentId) {
    try {
      const response = await axios.get(`/api/v1/students/${props.studentId}`);
      const student = response.data.data;
      studentId.value = student.id;
      firstName.value = student.firstName;
      lastName.value = student.lastName;
      dob.value = student.dateOfBirth;
      fullName.value = student.fullName;
    } catch (error) {
      console.error('Error fetching student:', error);
    }
  }
});

const submitForm = async () => {
  errors.value = {};

  const studentData = {
    id: studentId.value,
    firstName: firstName.value,
    lastName: lastName.value,
    dateOfBirth: dob.value,
  };

  try {
    if (props.isEditMode) {
      await axios.put(`/api/v1/students/${studentId.value}`, studentData);
    } else {
      await axios.post('/api/v1/students', studentData);
    }
    router.push('/students');
  } catch (error) {
    if (error.response && error.response.data && error.response.data.errors) {
      errors.value = error.response.data.errors;
    } else {
      console.error('Error submitting form:', error);
    }
  }
};
</script>

<template>
  <div class="container mt-5">

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <router-link to="/students" class="breadcrumb-item">Students</router-link>
        <router-link v-if="isEditMode" :to="`/students/${studentId}`" class="breadcrumb-item">{{ fullName }}</router-link>
        <li class="breadcrumb-item active" aria-current="page"> {{ isEditMode ? 'Edit Form' : "Form" }} </li>
      </ol>
    </nav>

    <h2>{{ isEditMode ? 'Edit Student' : 'New Student' }}</h2>
    <form @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="studentId" class="form-label"><i class="bi bi-person-badge-fill me-2"></i>Student ID</label>
        <input type="text" class="form-control" id="studentId" v-model="studentId" :readonly="isEditMode" required>
        <div class="col">
          <div class="text-danger" v-for="(errMsg, index) in errors.Id" :key="index">{{ errMsg }}</div>
        </div>
      </div>
      <div class="mb-3">
        <label for="firstName" class="form-label"><i class="bi bi-person-fill me-2"></i>Name</label>
        <div class="input-group">
          <input type="text" class="form-control" id="firstName" placeholder="First Name" v-model="firstName" required>
          <input type="text" class="form-control" id="lastName" placeholder="Last Name" v-model="lastName">
        </div>
        <div class="col">
          <div class="text-danger" v-for="(errMsg, index) in errors.FirstName" :key="index">{{ errMsg }}</div>
          <div class="text-danger" v-for="(errMsg, index) in errors.LastName" :key="index">{{ errMsg }}</div>
        </div>
      </div>
      <div class="mb-3">
        <label for="dob" class="form-label"><i class="bi bi-calendar-event-fill me-2"></i>Date of Birth</label>
        <input type="date" class="form-control" id="dob" v-model="dob" required>
        <div class="col">
          <div class="text-danger" v-for="(errMsg, index) in errors.DateOfBirth" :key="index">{{ errMsg }}</div>
        </div>
      </div>

      <button type="submit" class="btn btn-success mt-4">{{ isEditMode ? 'Update' : 'Submit' }}</button>
    </form>
  </div>
</template>

<style scoped>

</style>
