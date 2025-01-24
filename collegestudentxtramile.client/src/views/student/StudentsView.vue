<script setup>
import { ref, onMounted, watch } from 'vue';
import axios from 'axios';
import debounce from 'lodash/debounce';

const students = ref([]);
const keyword = ref('');
const page = ref('');
const limit = ref('');
const total = ref(0);
const totalPages = ref(0);
const previousPageUrl = ref('');
const nextPageUrl = ref('');

const fetchStudents = async (params = {}) => {
  try {
    const response = await axios.get('/api/v1/students', { params });
    students.value = response.data.data.items;
    page.value = response.data.data.page;
    limit.value = response.data.data.limit;
    total.value = response.data.data.total;
    totalPages.value = response.data.data.totalPages;
    previousPageUrl.value = response.data.data.url.previous;
    nextPageUrl.value = response.data.data.url.next;
  } catch (error) {
    console.error('Error fetching students:', error);
  }
};

const fetchStudentsDebounce = debounce(
  () => fetchStudents({
    keyword: keyword.value,
    page: page.value,
    limit: limit.value
  }),
  500
)

const studentToDelete = ref(null);
const confirmDeleteStudent = (student) => {
  if (confirm(`Are you sure you want to delete ${student.fullName}?`)) {
    deleteStudent(student.id);
  }
};

const deleteStudent = async () => {
  if (studentToDelete.value) {
    try {
      await axios.delete(`/api/v1/students/${studentToDelete.value.id}`);
      fetchStudents();
    } catch (error) {
      console.error('Error deleting student:', error);
    }
  }
};

onMounted(async () => {
  fetchStudents();
});

watch([keyword, page, limit], fetchStudentsDebounce, { immediate: true });

</script>

<template>

  <div>
    <h1>Students</h1>

    <div class="row mb-3">
      <div class="col">
        <router-link to="/students/new" class="btn btn-warning">
          <i class="bi bi-plus"></i> Add Student
        </router-link>
      </div>
      <div class="col-5">
        <div class="input-group">
          <input type="text" class="form-control" placeholder="Search student by name" v-model="keyword" @input="fetchStudents" />
          <button class="btn btn-outline-secondary" type="button" @click="fetchStudents">Search</button>
        </div>
      </div>
    </div>

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
          <td class="text-end">
            <router-link class="btn btn-outline-secondary btn-sm me-1" :to="`/students/${student.id}`"><i class="bi bi-eye-fill"></i></router-link>
            <router-link class="btn btn-outline-secondary btn-sm me-1" :to="`/students/${student.id}/edit`"><i class="bi bi-pen-fill"></i></router-link>
            <button class="btn btn-outline-danger btn-sm me-1" @click="confirmDeleteStudent(student)"><i class="bi bi-trash-fill"></i></button>
          </td>
        </tr>
      </tbody>
    </table>

    <div class="row align-items-center">

      <div class="col-auto me-auto">
        <p class="text-muted">Result found {{ total }} students</p>
      </div>

      <div class="col-auto me-3">
        <div class="input-group input-group-sm">
          <span class="input-group-text">Show</span>
          <select class="form-control" id="limit" v-model="limit" @change="fetchStudents">
            <option value="5">5</option>
            <option value="10">10</option>
            <option value="15">15</option>
            <option value="20">20</option>
            <option value="50">50</option>
            <option value="100">100</option>
            <option value="200">200</option>
          </select>
        </div>
      </div>

      <div class="col-auto">
          <nav aria-label="Page navigation">
            <ul class="pagination justify-content-center mb-0">
              <li class="page-item" :class="{ disabled: page === 1 }">
                <button class="page-link" @click="page--" :disabled="page === 1">Previous</button>
              </li>
              <li
                v-for="p in totalPages"
                :key="p"
                class="page-item"
                :class="{ active: page === p }"
              >
                <button class="page-link" @click="page = p">{{ p }}</button>
              </li>
              <li class="page-item" :class="{ disabled: page === totalPages }">
                <button class="page-link" @click="page++" :disabled="page === totalPages">Next</button>
              </li>
            </ul>
          </nav>
        </div>
      </div>

      <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="deleteModalLabel">Confirm Delete</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              Are you sure you want to delete {{ studentToDelete?.fullName }}?
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
              <button type="button" class="btn btn-danger" @click="deleteStudent">Delete</button>
            </div>
          </div>
        </div>
      </div>

  </div>
</template>
