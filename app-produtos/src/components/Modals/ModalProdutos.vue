<template>  
  <div>
      <div v-if="showModal" id="janelamodal">
        <div class="container"> <!-- Inicio da container  -->
            <div class="row">
            <div class="col-md-6 offset-md-3">
            
            <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Atualizar Produto</h2>
            <form class="form-control"> <!-- Inicio da from  -->
              <label>Nome</label>
              <input type="text" placeholder="Nome do produto" v-model="produto.nomeProduto" class="form-control">
              <label>Codigo</label>
              <input type="number" placeholder="Código do produto" v-model.number="produto.codigoProduto" class="form-control">
              <label>Centro</label>
              <select type="number" placeholder="Centro do produto" v-model.number="produto.idCentro" class="form-control">
               <option v-for="centro of centros" :key="centro.id" v-bind:value="centro.idCentro">{{centro.nomeCentro}}</option>
              </select>

              <label>Quantidade</label>
              <input type="number" placeholder="Quantidade do produto" v-model.number="produto.quantidadeProduto" class="form-control">
              <label>Valor</label>
              <input type="text" placeholder="Valor do produto" v-model="produto.valorProduto" class="form-control">
              <label>Imagem</label>
              <input type="file" @change.prevent="onFileSelected" style="" ref="fileinput" id="fileinput" class="form-control">
              <button @click.prevent="handleUpdateProduto()" class="btn-lg mx-2 btn-success text-white fas fa-edit mt-2"> Editar</button>
              <button @click.prevent="close()" class="btn-lg btn-secondary fas fa-sign-out-alt  mt-2"> Voltar</button>
            </form> <!-- fim da container  -->
            </div>
            </div>
        </div> <!-- Fim da container  -->
    </div>  
</div>
</template>

<script>

import { bus } from '../../main.js'
import useProdutos from "../../Composables/Produtos/UseProdutos";

const { updateProduto } =
useProdutos();

//import axios from 'axios'
export default {
  data() {
    return {
      produto:{
        type: Object
      },
      produtoPrev:{
      type: Object
      } ,
      centros:{
      type: Array
      } ,
      centrosPrev:{
      type: Array
      } ,
      showModal: false,
      selectedFile: null,
      data:'',
      config: '',
      ideditar:null
     }
  },
    
   created() {
      bus.$on('editar', (data) =>{
      this.showModal = true;
      this.produto = data;
      this.produtoPrev.Nome = data.nomeProduto;
      this.produtoPrev.Codigo = data.codigoProduto;
      this.produtoPrev.Quantidate = data.quantidadeProduto;
      this.produtoPrev.Valor = data.valorProduto;
      this.produtoPrev.Centro = data.idCentro;
      this.ideditar = data.idProduto;
      console.log(this.produto)
      });
      bus.$on('centro', (data) =>{
        this.centros = data;
        this.centrosPrev = data;
      });
   },

   methods: {
    async handleUpdateProduto(){
      await updateProduto(this.produto); 
      this.showModal = false;
    },
    //obter dados da imagem
    onFileSelected(event){
      console.log(this.produtos)
      let file = event.target.files[0];
      this.produto.blobFile.name = file.name;
      let reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onloadend = () => {
        file = /,(.+)/.exec(reader.result)[1];
        this.produto.blobFile.data = file
      };
    },

    close(){
      this.produto.nomeProduto = this.produtoPrev.Nome;  
      this.produto.codigoProduto = this.produtoPrev.Codigo ;
      this.produto.quantidadeProduto = this.produtoPrev.Quantidate;
      this.produto.valorProduto = this.produtoPrev.Valor;
      this.produto.idCentro = this.produtoPrev.Centro;
      this.data = null;
      this.showModal = false;
    },
  } 
}
</script>


<style scoped>

#janelamodal{
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