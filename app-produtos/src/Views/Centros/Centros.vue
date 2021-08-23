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
                  <button @click.prevent="handleCreateCentro()" class="btn-lg btn-primary fas fa-save mt-2"> Salvar</button>
                   <button @click.prevent="clear()" class="btn-lg btn-success fas fa-broom mt-2"> Limpar</button>
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
                    <td scope="row">#{{centro.codigoCentro}}</td>
                    <td>{{centro.nomeCentro}}</td>
                    <td>
                        <button @click="handleUpdateCentro(centro)" class="btn-sm btn-primary"><i class="material-icons">create</i></button>
                        <button @click="handleDeleteCentro(centro)" class="btn-sm btn-danger"><i class="material-icons">delete_sweep</i></button>
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
import { bus } from '../../main';
import Titulo from '../../components/Titulo/Titulo.vue';
import ModalCentros from '../../components/Modals/ModalCentros.vue';
//import { ref, onMounted } from "@vue/composition-api";
import useCentros from "../../Composables/Centros/UseCentros";
import useEnderecos from "../../Composables/Enderecos/UseEnderecos";
//import Notificacoes from '../../Domain/Notificacoes/Notificacoes.service';

const { getCentros, createCentro, deleteCentro, updateCentro, } =
useCentros();

const {createEndereco, getEnderecos, deleteEndereco} =
useEnderecos();



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
    this.handleGetCentros()
  },

  components: {
    Titulo,
    ModalCentros
  },

 created() {
    this.handleGetCentros();
  },
  
  methods: {
    onChangePage(pageOfItems) {
      // update page of items
      this.pageOfItems = pageOfItems;
    },

    async handleGetCentros(){
      this.centros = await getCentros();
      this.enderecos = await getEnderecos();
    },
    
   async handleCreateCentro(){
      const createdEndereco = await createEndereco(this.endereco);
      if (createdEndereco) {
        this.centro.IdEndereco = createdEndereco.idEndereco;
        this.centro.NomeCentro = createdEndereco.cidade
        this.enderecos.push(createdEndereco)
      }
      const createdCentro = await createCentro(this.centro);
      if (createdCentro) {
        this.centros.push(createdCentro);
      }
      this.clear();
    },

    async handleDeleteCentro(centro){
      await axios.get('http://localhost:5000/api/produtos/').then(response =>{
      let produtodelet;
      produtodelet = response.data;
        produtodelet.forEach(produto => {
          if(produto.idCentro == centro.idCentro){
            axios.delete('http://localhost:5000/api/produtos/' + produto.idProduto);
          }
        })
      }),
      await deleteCentro(centro.idCentro);
      await deleteEndereco(centro.idEndereco);
      let indice = this.centros.indexOf(centro);
      this.centros.splice(indice, 1);
    },

    handleUpdateCentro(centro){
      bus.$emit('editarcentro', centro);
      bus.$emit('editarendereco', this.enderecos);
    },

    async update(centro){
      const updatedCentro = await updateCentro(centro.IdCentro);
      if (updatedCentro) {
        let indice = this.centros.indexOf(centro);
        this.centros.splice(indice, 1);
      }
    },
    clear(){
      this.centro = {};
      this.endereco ={};
    }
  },
  
}

</script>

<style>
</style>
