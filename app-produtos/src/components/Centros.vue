<template>
<div>
  <vue-confirm-dialog></vue-confirm-dialog>
  <div class="container"> <!-- Inicio do container principal  -->
      <div class="row">
          <div class="col-md-12"> <!-- Inicio do container form  -->
              <form class="form-group p-2 shadow border bg-white">
                <titulo texto="Cadastro de Centros"/>
                  <div class="row"> 
                  <div class="col-md-6">
                    <label>Cep</label>
                    <input type="number" placeholder="Cep do centro" v-model.number="endereco.Cep" class="form-control">
                    <label>Estado</label>
                    <input type="text" placeholder="Estado do centro" v-model="endereco.Estado" class="form-control">
                    <label>Cidade</label>
                    <input type="text" placeholder="Cidade do centro" v-model="endereco.Cidade" class="form-control">
                </div>
                <div class="col-md-6">
                    <label>Rua</label>
                    <input type="text" placeholder="Rua do centro" v-model="endereco.Rua" class="form-control">
                    <label>Bairro</label>
                    <input type="text" placeholder="Bairro do centro" v-model="endereco.Bairro" class="form-control">
                    <label>Codigo</label>   
                    <input type="number" placeholder="Código do centro" v-model.number="centro.CodigoCentro" class="form-control">
                </div>
                </div>
                  <button @click.prevent="salvar()" class="btn-lg btn-primary fas fa-save mt-2"> Salvar</button>
                   <button @click.prevent="limparcampos()" class="btn-lg btn-success fas fa-broom mt-2"> Limpar</button>
                </form> <!-- Inicio do container form  -->
            </div> <!-- fim do container form  -->
          </div>

            <div class="row">
                <div class="col-md-12 text-center"> <!-- Inicio do container table  -->
                <table class="table table-striped align-middle">
                <thead class="bg-info"> <!-- Inicio do table  -->
                    <tr class="text-white">
                    <th scope="col">CODIGO</th>
                    <th scope="col">NOME</th>
                    <th scope="col">OPÇÕES</th>
                    </tr>
                </thead> <!-- fim do table head  -->
                <tbody> <!-- Inicio do table body  -->
                    <tr v-for="centro of pageOfItems" :key="centro.id">
                    <td scope="row">#{{centro.CodigoCentro}}</td>
                    <td>{{centro.NomeCentro}}</td>
                    <td>
                        <button @click="editar(centro)" class="btn btn-primary"><i class="material-icons">create</i></button>
                        <button @click="remover(centro)" class="btn btn-danger"><i class="material-icons">delete_sweep</i></button>
                    </td>
                    </tr>
                </tbody> <!-- fim do tablebody  -->
                </table> <!-- fim do da table  -->
                <div class="card-footer bg-light fixed-bottom" style="padding:8px;">
                  <jw-pagination :pageSize=5 :items="centros" @changePage="onChangePage"></jw-pagination>
              </div>
                </div> <!-- fim do container da table  -->
            </div>
          </div>
          <ModalCentros/>
      </div> <!-- fim do container principal  -->
</template>


<script>
import axios from 'axios'

import Centro from '../services/centros'
import Endereco from '../services/enderecos'
import Titulo from './Titulo.vue'
import ModalCentros from './ModalCentros.vue'
import { bus } from '../main.js'
import Notificacoes from '../services/Notificacoes'

export default {

  data() {  
    return {
      centro:{
        CodigoCentro: '',
        NomeCentro: '',
        IdEndereco: 'null',
      },
      endereco:{
        Estado: '',
        Cidade: '',
        Bairro: '',
        Rua: '',
        Cep: null,
      },
      centros:[].map(i => ({ id: (i+1), name: 'centro' + (i+1) })),
      pageOfItems: [],
      enderecos:[],
      showModal: true,
    }
  },

  mounted() {
    this.listar();
  },

    components: {
    Titulo,
    ModalCentros
  },

  methods: {
    onChangePage(pageOfItems) {
            // update page of items
            this.pageOfItems = pageOfItems;
        },
    async salvar() {
     await axios.post("http://localhost:5000/api/enderecos", this.endereco).then(response => {
      this.centro.NomeCentro = 'Centro ' + this.endereco.Cidade;
      this.centro.IdEndereco = response.data.IdEndereco;
    });
      JSON.stringify(this.centro)
    await axios.post('http://localhost:5000/api/centros', this.centro)
      .then(response => {
          this.listar();
          Notificacoes.successmsg();
          console.log(response);
          this.limparcampos();
      })
      .catch(function(error) {
          Notificacoes.erromsg(error);
          axios.delete('http://localhost:5000/api/enderecos/' + this.centro.IdEndereco);
      });
    },

    listar(){
      Endereco.listar().then(resposta =>{
        this.enderecos = resposta.data;
      });
      Centro.listar().then(resposta =>{
       this.centros = resposta.data;
      });
    },

    remover(centro){
      this.$confirm(
        {
          message: `Você Tem Certeza? Isso irá deletar todos os produtos cadastrados neste centro.`,
          button: {
            yes: 'Sim',
            no: 'Não'
          },
          /**
          * Callback Function
          * @param {Boolean} confirm 
          */
          callback: async confirm => {
            if (confirm) {
             let indice = this.centros.indexOf(centro);
             this.centros.splice(indice, 1);
             await axios.delete('http://localhost:5000/api/centros/' + centro.IdCentro); 
             await axios.delete('http://localhost:5000/api/enderecos/' + centro.IdEndereco);
             await axios.get('http://localhost:5000/api/produtos/').then(response =>{
               let produtodelet;
               produtodelet = response.data;
               produtodelet.forEach(produto => {
                 if(produto.IdCentro == centro.IdCentro){
                   axios.delete('http://localhost:5000/api/produtos/' + produto.IdProduto);
                 }
               });
             });
             Notificacoes.deletemsg();
            }
          }
        }
      );
    },

    editar(centro){
      bus.$emit('editarcentro', centro);
      bus.$emit('editarendereco', this.enderecos);
      this.showModal = true;
    },

    limparcampos(){
      Endereco.limpar(this.endereco);
      Centro.limpar(this.centro);
    }
  },
  
}

</script>

<style>
</style>
