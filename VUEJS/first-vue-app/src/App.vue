<template>
  <div id="app">
    <Header />
    <Menu/>
  </div>
</template>

<script>
import Header from './components/layout/TheHeader.vue'
import Menu from './components/layout/TheMenu.vue'

export default {
  name: 'App',
  components: {
    Header,
    Menu
  }
}
</script>

<style>
 @import url('/css/base/main.css');
</style>
