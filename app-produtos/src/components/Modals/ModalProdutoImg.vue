<template>  
  <div>
      <div v-show="showModalImg" id="janelamodal">
        <div class="container"> <!-- Inicio da container  -->
            <div class="row">
            <div class="col-md-4 offset-md-4">
              <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Visualizar o produto</h2>
              <div id="#imagemdiv" class="d-flex justify-content-end"> <button @click.prevent="fechar()"  class="btn btn-danger"><i class="far fa-times-circle fa-2x"></i></button></div>
                <div class="d-flex justify-content-center bg-white" id="divimg">
                  <img  :src=produto.file  class="img-fluid p-5 animated" alt="Responsive image">
                </div>
            </div>
            </div>
        </div> <!-- Fim da container  -->
    </div>  
</div>
</template>

<script>

import { bus } from '../../main.js'
//import axios from 'axios'
//import Notificacoes from '../services/Notificacoes.js'


//import axios from 'axios'
export default {
  data() {
    return {
        produto:{
        type: Object
        } ,
        centros:{
        type: Array
        } ,
        showModalImg: false,
     }
  },
    
  created() {
      bus.$on('verproduto', (data) =>{
      this.showModalImg = true;
      this.produto = data;
      });
      bus.$on('centroimg', (data) =>{
        this.centros = data;
      });
  },

  methods: {
        fechar(){
          this.showModalImg = false;
        }
      }
  }
</script>


<style scoped>
#divimg{
  max-width: 100%;
  max-height: 100%;
}
#imagemdiv{

  position: relative;
  display: flex;
  justify-content: center;
  z-index: 9999;
}
.btn{
    display: flex !important;
    position: absolute;
    margin: 5px;
    border: 0px solid rgb(186, 25, 25) !important;
}
img {
  background: white;

}

#janelamodal {
    transition: all ease-in-out 2s;
    position: fixed;
    top: 0px;

    display: flex;
    justify-content: center;
    align-items: center;

    width: 100vw;
    min-height: 100vh;
    overflow-x: hidden;
    background: rgba(0, 0, 0, 0.5);
  }
</style>