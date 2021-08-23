<template>
  <div id="app">
    <Nav/>
    <Notificacoes/>
    <router-view></router-view>
  </div>
</template>



<script>


import Nav from './components/Nav/Nav.vue'
import Notificacoes from './components/Notificacoes/Notificacoes.vue'


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
}

</script>
  
<style>
    .fixed-bottom{
      z-index: 0 !important;
    }
    .table{
      margin-bottom: 0;
    }
    div.card-footer{
      padding: 0;
    }
    button.btn{
      max-height: 42px !important;
    }
    body{
      background: rgb(247, 245, 243);
      color: black;
      font-size: 0.94rem;
    }
</style>

