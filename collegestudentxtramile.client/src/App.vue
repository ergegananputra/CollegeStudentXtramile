<script setup>
  import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <!--<img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />-->
    <nav class="navbar fixed-top base">
      <div class="container-fluid">
        <RouterLink class="navbar-brand" to="/">
          <img class="navbar-brand" src="https://avatars.githubusercontent.com/u/126530940?v=4" alt="ergegananputra" width="30" heigh="24">
          College Student
        </RouterLink>

      </div>
    </nav>
  </header>

  <main>
    <div class="container base">
      <RouterView />
    </div>
  </main>
</template>

<style scoped>

</style>
