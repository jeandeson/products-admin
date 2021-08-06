<template>
<div>
  <Modal/>
  <ModalProdutoImg/>
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
                      <option v-for="centro of centros" :key="centro.id" v-bind:value="centro.IdCentro">{{centro.NomeCentro}}</option>
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
                  <button @click.prevent="salvar()" class="btn-lg btn-primary fas fa-save mt-2"> Salvar</button>
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
                  <tr v-for="produto of produtos" :key="produto.id">
                  <td scope="row">{{produto.NomeProduto}}</td>
                  <td >{{produto.QuantidadeProduto}}</td>
                  <td>{{produto.ValorProduto}} R$</td>
                  <td>
                      <button @click.prevent="visualizar(produto)" class="btn btn-secondary"><i class="material-icons">remove_red_eye</i></button>
                      <button @click.prevent="editar(produto)" class="btn btn-primary"><i class="material-icons">create</i></button>
                      <button @click="remover(produto)" class="btn btn-danger"><i class="material-icons">delete_sweep</i></button>
                  </td>
                  </tr>
              </tbody> <!-- fim corpo da tabela -->
              
              </table> <!-- fim da tabela -->
              </div> <!-- fim da coluna da tabela -->
              </div>
        </div>
      </div>
  </div> <!-- fim do container principal -->   
</div>
</template>


<script>
import axios from 'axios'
import { bus } from '../main.js'
import Produto from '../services/produtos'
import Centro from '../services/centros'
import Titulo from './Titulo.vue'
import Modal from '../components/ModalProdutos'
import Notificacoes from '../services/Notificacoes'
import ModalProdutoImg from './ModalProdutoImg.vue'

export default {
  data() {
    return {
      showModal: true,
      showModalImg: true,

      produto:{
        CodigoProduto: null,
        NomeProduto: null,
        QuantidadeProduto: null,
        ValorProduto: null,
        IdCentro: null,
        ImagemProduto:null,
      },

      produtos:[],
      erros:[],
      centros:[],
      selectedFile: null,
      data:null,
      config: null,
      opcao:{
      NomeCentro: '-Selecione um centro-',
      IdCentro: null
      }
    }
  },

    components: {
    Titulo,
    Modal,
    ModalProdutoImg
  },

  beforeMount() {  
    this.listar();
  },

  methods: {
    //obter dados da imagem
    onFileSelected(event){
      this.data = new FormData();
      this.data.append("image", event.target.files[0]);
      this.config = {
        headers: {
          "Content-type": "application/x-www-form-urlencoded",
          Authorization: "Client-ID 3052282645ce5e9",
        },
      }
    },
    //upload da imagem depois salva no banco
    async salvar() {
      if (this.data != null && this.produto.NomeProduto != "") {
        Notificacoes.uploadmsg();
        await axios.post("https://api.imgur.com/3/image", this.data, this.config).then(response => {  
        var someStr = (response.data.data.link.replace(/['"]+/g, ''));
        this.produto.ImagemProduto = someStr;
      });
    } 
    JSON.stringify(this.produto);
    await axios.post('http://localhost:5000/api/produtos', this.produto)
      .then(response => {
          this.limparcampos();
          this.listar();
          Notificacoes.successmsg();
          console.info(response);
          document.getElementById("fileinput").value = "";
          this.data = null;
          this.config = null;
          this.produto.ImagemProduto = null;
      })
      .catch(e => {
        if(e.response.data.status == 400){ 
          Notificacoes.erroinvalid();
        }
        else{
          Notificacoes.erromsg(e);
        }
      });
     },
      //lista os produtos do banco na tabela
    listar(){
      Produto.listar().then(resposta =>{
        this.produtos = resposta.data;
      })
      Centro.listar().then(resposta =>{
        this.centros = resposta.data;
        this.centros.unshift(this.opcao);
        this.produto.IdCentro = this.centros[0].IdCentro;
      })  
    },

    //remove da tabela e exclui do banco
    remover(produto){
      this.$confirm(
        {
          message: `Você Tem Certeza?`,
          button: {
            yes: 'Sim',
            no: 'Não'
          },
          /**
          * Callback Function
          * @param {Boolean} confirm 
          */
          callback: confirm => {
            if (confirm) {
             let indice = this.produtos.indexOf(produto);
             this.produtos.splice(indice, 1);
             Produto.apagar(produto);
             Notificacoes.deletemsg();
            }
          }
        }
      );
    },

    //edita o produto
    editar(produto){
      bus.$emit('editar', produto);
      bus.$emit('centro', this.centros);
    },
    //visualizar o produto
    visualizar(produto){
      bus.$emit('verproduto', produto);
      bus.$emit('centroimg', this.centros);
    },
    //limpar os campos
    limparcampos(){
      Produto.limpar(this.produto);
    }
  },
}

</script>

<style>
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
