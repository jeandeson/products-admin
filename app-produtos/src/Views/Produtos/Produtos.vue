<template>
<div>
  <vue-confirm-dialog></vue-confirm-dialog>
  <div class="container"> <!-- Inicio container principal -->
      <div class="row">
          <div class="col-md-12"> <!-- Inicio coluna do form -->
                <form class="form-group p-2 shadow border bg-white"> <!-- Inicio form -->
                  <titulo texto="Cadastro de produtos"/>
                  
                  <div class="row">
                  <div class="col-md-6"> 
                    <label>Nome</label>
                    <input type="text" placeholder="Nome do produto" v-model="produto.NomeProduto" class="form-control">
                    <label>Codigo</label>
                    <input type="number" placeholder="Código do produto" v-model.number="produto.CodigoProduto" class="form-control">
                    <label>Centro</label>
                    <select type="number" placeholder="Centro do produto" v-model.number="produto.IdCentro" class="form-control">
                      <option v-for="centro of centros" :key="centro.id" v-bind:value="centro.idCentro">{{centro.nomeCentro}}</option>
                    </select>
                  </div>

                  <div class="col-md-6">
                    <label>Quantidade</label>
                    <input type="number" placeholder="Quantidade do produto" v-model.number="produto.QuantidadeProduto" class="form-control">
                    <label>Valor</label>
                    <input type="text" placeholder="Valor do produto" v-model="produto.ValorProduto" class="form-control">
                    <label>Imagem</label>
                    <input type="file" @change.prevent="onFileSelected" style="" ref="fileinput" id="fileinput" accept="image/*" class="form-control">
                  </div>
                  </div>
                  <button @click.prevent="handleCreateProduto()" class="btn-lg btn-primary fas fa-save mt-2"> Salvar</button>
                  <button @click.prevent="limparcampos()" class="btn-lg btn-success fas fa-broom mt-2"> Limpar</button>
                </form> <!-- Fim form -->

          </div> <!-- Fim coluna do form -->
          <div class="container"> <!-- Inicio container da tabela -->
              <div class="row">
              <div class="col-md-12 text-center"> <!-- Inicio da coluna da tabela -->
              <table class="table table-striped align-middle">

              <thead class="bg-info"> <!-- Cabeçalho da tabela -->
                  <tr class="text-white">
                  <th scope="col">NOME</th>
                  <th scope="col">QTD</th>
                  <th scope="col">VALOR</th>
                  <th scope="col">OPÇÕES</th>
                  </tr>
              </thead>

              <tbody> <!-- corpo da tabela -->
                  <tr v-for="produto of pageOfItems" :key="produto.id">
                  <td scope="row">{{produto.nomeProduto}}</td>
                  <td >{{produto.quantidadeProduto}}</td>
                  <td>{{produto.valorProduto}} R$</td>
                  <td>
                      <button @click.prevent="visualizar(produto)" class="btn-sm btn-secondary"><i class="material-icons">remove_red_eye</i></button>
                      <button @click.prevent="handleUpdateProduto(produto)" class="btn-sm btn-primary"><i class="material-icons">create</i></button>
                      <button @click="handleDeleteProduto(produto)" class="btn-sm btn-danger"><i class="material-icons">delete_sweep</i></button>
                  </td>
                  </tr>
              </tbody> <!-- fim corpo da tabela -->
              </table> <!-- fim da tabela -->
              <div class="card-footer bg-light fixed-bottom" style="padding:8px;">
                  <jw-pagination :pageSize=5 :items="produtos" @changePage="onChangePage"></jw-pagination>
              </div> 
              </div> <!-- fim da coluna da tabela -->
              </div>
        </div>
      </div>
  </div> <!-- fim do container principal -->   
    <modal-produto-img></modal-produto-img>
      <Modal/>
</div>
</template>


<script>


import { bus } from '../../main.js'
import Titulo from '../../components/Titulo/Titulo.vue'
import Modal from '../../components/Modals/ModalProdutos.vue'
import useProdutos from "../../Composables/Produtos/UseProdutos";
import useCentros from "../../Composables/Centros/UseCentros";
import ModalProdutoImg from '../../components/Modals/ModalProdutoImg.vue'

const { getProdutos, createProduto, deleteProduto, updateProduto, } =
useProdutos();

const {getCentros} =
useCentros();

export default {
  data() {
    return {
      showModal: false,
      showModalImg: true,

      produto:{
        CodigoProduto: null,
        NomeProduto: null,
        QuantidadeProduto: null,
        ValorProduto: null,
        IdCentro: null,
        FileID:null,
        BlobFile:{
          Name:null,
          Data:null
        }
      },

      produtos:[].map(i => ({ id: (i+1), name: 'produto' + (i+1) })),
      pageOfItems: [],
      erros:[],
      centros:[],
      selectedFile: null,
      data:null,
      config: null,
      opcao:{
      nomeCentro: '-Selecione um centro-',
      idCentro: null
      }
    }
  },

    components: {
    Titulo,
    Modal,
    ModalProdutoImg
  },

  beforeMount() { 
    this.handleGetProdutos(); 
  },

  methods: {
    onChangePage(pageOfItems) {
      // update page of items
      this.pageOfItems = pageOfItems;
    },
    //obter dados da imagem
    onFileSelected(event){
      console.log(this.produtos)
      let file = event.target.files[0];
      this.produto.BlobFile.name = file.name;
      let reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onloadend = () => {
        reader.result.split(',').pop();
        file = /,(.+)/.exec(reader.result)[1];
        this.produto.BlobFile.data = file
      };
    },
    async handleGetProdutos(){
      this.produtos = await getProdutos();
      this.centros = await getCentros();
      this.centros.unshift(this.opcao);
    },
    
    async handleCreateProduto(){
      const createdProduto = await createProduto(this.produto);
      if (createdProduto) {
        this.produtos.push(createdProduto);
      }
      this.clear();
    },

    async handleDeleteProduto(produto){
      await deleteProduto(produto.idProduto);
      let indice = this.produtos.indexOf(produto);
      this.produtos.splice(indice, 1);
    },

    handleUpdateProduto(produto){
      bus.$emit('editar', produto);
      bus.$emit('centro', this.centros);
    },

    async update(produto){
      const updatedProduto = await updateProduto(produto.IdProduto);
      if (updatedProduto) {
        let indice = this.produtos.indexOf(produto);
        this.produtos.splice(indice, 1);
      }
    },

    clear(){
      this.produto.CodigoProduto = null;
      this.produto.NomeProduto = '';
      this.produto.QuantidadeProduto = null;
      this.produto.ValorProduto = '';
      this.produto.FileID = '';
      this.produto.IdCentro = this.centros[0].idCentro;
      this.endereco ={};
    },

    visualizar(produto){
      bus.$emit('verproduto', produto);
      bus.$emit('centroimg', this.centros);
    },

  },
}

</script>

<style>
.page-item.active .page-link {
    z-index: 0 !important;
}

div.card-footer:hover{
  background-color: #f7f5f3 !important;
}
th {
  border: none !important;
}

tbody tr:hover{
  background: rgb(201, 213, 214);
}

img{
  max-width: 50px;
}

.form-group{
  background:#e9ecf0 !important;
  border-top:  2px solid #e8ecee;
  border-radius: 7% 7% 3% 3%;
}
.table > :not(caption) > * > * {
  max-width: 85px;
  min-width: 85px;
}
</style>
