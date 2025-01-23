<script setup>
  import { useRouter } from 'vue-router'

  const router = useRouter()

  function navigateToStudents() {
    router.push('/students')
  }

</script>

<template>
  <div>
    <h1>Home</h1>
    <router-link to="/students">View Students</router-link>
  </div>
</template>
