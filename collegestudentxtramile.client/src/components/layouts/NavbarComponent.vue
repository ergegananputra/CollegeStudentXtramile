<template>
  <nav class="bg-gray-800 p-4">
    <div class="container mx-auto flex justify-between items-center">
      <div class="text-white text-lg font-bold">College Student Xtramile</div>
      <ul class="flex space-x-4">
        <li><router-link to="/" class="text-white">Home</router-link></li>
        <li><router-link to="/students" class="text-white">Students</router-link></li>
      </ul>
    </div>
  </nav>
</template>

<script>export default {
  name: 'NavbarComponent',
};</script>

<style scoped>
  nav {
    background-color: #333;
    color: white;
  }
</style>
