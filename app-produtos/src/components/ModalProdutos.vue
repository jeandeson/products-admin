<template>  
  <div>
      <div v-if="showModal" id="janelamodal">
        <div class="container"> <!--- Inicio da container  --->
            <div class="row">
            <div class="col-md-6 offset-md-3">
            
            <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Atualizar Produto</h2>
            <form class="form-control"> <!--- Inicio da from  --->
              <label>Nome</label>
              <input type="text" placeholder="Nome do produto" v-model="produto.NomeProduto" class="form-control">
              <label>Codigo</label>
              <input type="number" placeholder="Código do produto" v-model.number="produto.CodigoProduto" class="form-control">
              <label>Centro</label>
              <select type="number" placeholder="Centro do produto" v-model.number="produto.IdCentro" class="form-control">
               <option v-for="centro of centros" :key="centro.id" v-bind:value="centro.IdCentro">{{centro.NomeCentro}}</option>
              </select>

              <label>Quantidade</label>
              <input type="number" placeholder="Quantidade do produto" v-model.number="produto.QuantidadeProduto" class="form-control">
              <label>Valor</label>
              <input type="text" placeholder="Valor do produto" v-model="produto.ValorProduto" class="form-control">
              <label>Imagem</label>
              <input type="file" @change.prevent="onFileSelected" style="" ref="fileinput" id="fileinput" class="form-control">
              <button @click.prevent="salvaredit()" class="btn-lg mx-2 btn-success text-white fas fa-edit mt-2"> Editar</button>
              <button @click.prevent="fechar()" class="btn-lg btn-secondary fas fa-sign-out-alt  mt-2"> Voltar</button>
            </form> <!--- fim da container  --->
            </div>
            </div>
        </div> <!--- Fim da container  --->
    </div>  
</div>
</template>

<script>

import { bus } from '../main.js'
import axios from 'axios'
import Notificacoes from '../services/Notificacoes.js'


//import axios from 'axios'
export default {
  data() {
    return {
        produto:{
        type: Object
        } ,
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
      this.produtoPrev.nome = data.NomeProduto;
      this.produtoPrev.codigo = data.CodigoProduto;
      this.produtoPrev.quantidate = data.QuantidadeProduto;
      this.produtoPrev.valor = data.ValorProduto;
      this.produtoPrev.centro = data.IdCentro;
      this.ideditar = data.IdProduto;
      });
      bus.$on('centro', (data) =>{
        this.centros = data;
        this.centrosPrev = data;
      });
  },

  methods: {
      //editar a imagem
      onFileSelected(event){
        this.data = new FormData();
        this.data.append("image", event.target.files[0]);
        this.config = {
          headers: {
            "Content-type": "application/x-www-form-urlencoded",
            Authorization: "Client-ID 3052282645ce5e9",
          },
        };
     },
    //salvar edição
    async salvaredit(){
      try {
        if (this.data != '') {
        Notificacoes.uploadmsg();
        await axios.post("https://api.imgur.com/3/image", this.data, this.config).then(response => {
          var someStr = (response.data.data.link.replace(/['"]+/g, ''));
          this.produto.ImagemProduto = someStr;
         });
        }
        axios.put('http://localhost:5000/api/produtos/' + this.ideditar, {
          IdProduto: this.produto.IdProduto,
          CodigoProduto:this.produto.CodigoProduto,
          NomeProduto:this.produto.NomeProduto,
          QuantidadeProduto:this.produto.QuantidadeProduto,
          ValorProduto:this.produto.ValorProduto,
          ImagemProduto:this.produto.ImagemProduto,
          IdCentro:this.produto.IdCentro
        }).then(response => {
          Notificacoes.successmsg(response)
          this.fechar();
          this.data = '';
        });
        } catch (error) {
          Notificacoes.erromsg(error);
        }
      },
    //fechar janela
    fechar(){
      this.produto.NomeProduto = this.produtoPrev.nome;  
      this.produto.CodigoProduto = this.produtoPrev.codigo ;
      this.produto.QuantidadeProduto = this.produtoPrev.quantidate;
      this.produto.ValorProduto = this.produtoPrev.valor;
      this.produto.IdCentro = this.produtoPrev.centro;
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