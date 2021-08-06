<template>
  <div id="app">
    <Nav/>
    <Notificacoes/>
    <router-view></router-view>
  </div>
</template>



<script>


import Nav from './components/Nav.vue'
import Notificacoes from './components/Notificacoes.vue'


export default {
  data() {
    return {
      showModal: true,
    }
  },
  name: 'App',
  components: {
    Nav,
    Notificacoes,
  },
  created() {
    
  },
}

</script>
  
<style>
    body{
      background: rgb(247, 245, 243);
      color: black
    }
</style>

