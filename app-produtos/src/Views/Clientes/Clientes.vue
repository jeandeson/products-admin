<template>
<div>
  <vue-confirm-dialog></vue-confirm-dialog>
  <div class="container"> <!-- Inicio do container principal  -->
      <div class="row">
          <div class="col-md-12"> <!-- Inicio da coluna form  -->
                <form class="form-group p-2 shadow border bg-white"> <!-- Inicio do form  -->
                    <titulo texto="Cadastro de clientes"/>
                    
                    <div class="row">
                      <div class="col-md-4">
                        <label>Nome</label>
                        <input type="text" placeholder="Nome do cliente" v-model="cliente.NomeCliente" class="form-control">
                        <label>Sobrenome</label>
                        <input type="text" placeholder="Cidade do cliente" v-model="cliente.SobrenomeCliente" class="form-control">
                        <label>Email</label>
                        <input type="text" placeholder="Email do cliente" v-model="cliente.EmailCliente" class="form-control">
                      </div>

                      <div class="col-md-4">
                        <label for="sobrenome">Estado</label>
                        <input type="text" placeholder="Estado do cliente" v-model="endereco.Estado" class="form-control">
                        <label>Cidade</label>
                        <input type="text" placeholder="Cidade do cliente" v-model="endereco.Cidade" class="form-control">
                        <label>Bairro</label>
                        <input type="text" placeholder="Rua do cliente" v-model="endereco.Bairro" class="form-control">
                      </div>

                      <div class="col-md-4">
                        <label>Rua</label>
                        <input type="text" placeholder="Rua do cliente" v-model="endereco.Rua" class="form-control">
                        <label>Cep</label>
                        <input type="text" placeholder="Cep do cliente" v-model.number="endereco.Cep" class="form-control">
                        <label>Complemento</label>
                        <input type="text"  placeholder="Complemento (Opcional)" v-model="cliente.complemento" class="form-control">
                      </div>
                    
                    </div>
                        <button class="btn-lg btn-primary fas fa-save mt-2" @click.prevent="handleCreateCliente()"> Salvar</button>
                        <button @click.prevent="Limparcampos()" class="btn-lg btn-success fas fa-broom mt-2"> Limpar</button>
                   </form> <!-- Fim do container  -->

            </div> <!-- fim da coluna form  -->

          <div class="container"> <!-- Inicio do container table  -->
              <div class="row">
                <div class="col-md-12 text-center">
                  <table class="table table-striped align-middle"> <!-- Inicio do table head  -->
                  <thead class="bg-info">
                      <tr class="text-white">
                      <th scope="col">NOME</th>
                      <th scope="col">EMAIL</th>
                      <th scope="col">OPÇÕES</th>
                      </tr>
                  </thead> <!-- fim do table head  -->
                  <tbody> <!-- incio do table body  -->
                    <tr v-for="cliente of pageOfItems" :key="cliente.IdCliente">
                      <td scope="row">{{cliente.nomeCliente}}</td>
                      <td>{{cliente.emailCliente}}</td>
                      <td>
                          <button @click.prevent="handleUpdateCliente(cliente)" class="btn-sm btn-primary"><i class="material-icons">create</i></button>
                          <button @click.prevent="handleDeleteCliente(cliente)" class="btn-sm btn-danger"><i class="material-icons">delete_sweep</i></button>
                      </td>
                    </tr>
                  </tbody> <!-- fim do table body  -->
                  </table> <!-- fim da  da table  -->
              <div class="card-footer bg-light fixed-bottom" style="padding:8px;">
                  <jw-pagination :pageSize=5 :items="clientes" @changePage="onChangePage"></jw-pagination>
              </div> 
                </div>
             </div>
          </div> <!-- fim do container da table  -->
       </div>
    </div> <!-- fim  do container principal  -->
    <ModalClientes/>
  </div>
</template>


<script>
//import axios from 'axios'
import { bus } from '../../main'
//import cliente from '../../Domain/Clientes/clientes.service'
import Titulo from '../../components/Titulo/Titulo.vue'
//import Endereco from '../../Domain/Enderecos/enderecos.service'
import ModalClientes from '../../components/Modals/ModalClientes.vue'
//import Notificacoes from '../../Domain/Notificacoes/Notificacoes.service'
import useClientes from "../../Composables/Clientes/UseClientes";
import useEnderecos from "../../Composables/Enderecos/UseEnderecos";
//import { ref, onMounted } from "@vue/composition-api";

const { getClientes, createCliente, deleteCliente, updateCliente, } =
useClientes();

const {createEndereco, getEnderecos, deleteEndereco} =
useEnderecos();

export default {

    components: {
    Titulo,
    ModalClientes
  },
   data() {
    return {
      showModal: true,
      cliente:{
        NomeCliente: '',
        SobrenomeCliente: '',
        EmailCliente: '',
        IdEndereco: null,
        complemento: null,
      },
      endereco:{
        Estado: '',
        Cidade: '',
        Bairro: '',
        Rua: '',
        Cep: '',
      },
      enderecos:[],
      clientes:[].map(i => ({ id: (i+1), name: 'cliente' + (i+1) })),
      pageOfItems: [],
    }
  },

  created() {
    this.handleGetClientes();
  },
  
  methods: {
    onChangePage(pageOfItems) {
      // update page of items
      this.pageOfItems = pageOfItems;
    },

    async handleGetClientes(){
      this.clientes = await getClientes();
      this.enderecos = await getEnderecos();
    },
    
   async handleCreateCliente(){
      const createdEndereco = await createEndereco(this.endereco);
      if (createdEndereco) {
        this.cliente.IdEndereco = createdEndereco.idEndereco;
        this.enderecos.push(createdEndereco)
      }
      const createdCliente = await createCliente(this.cliente);
      if (createdCliente) {
        this.clientes.push(createdCliente);
      }
      this.clear();
    },

    async handleDeleteCliente(cliente){
      await deleteCliente(cliente.idCliente);
      let indice = this.clientes.indexOf(cliente);
      this.clientes.splice(indice, 1);
      await deleteEndereco(cliente.idEndereco);
    },

    handleUpdateCliente(cliente){
      bus.$emit('editarcliente', cliente);
      bus.$emit('editarendereco', this.enderecos);
      this.showModal = true;
    },

    async update(cliente){
      const updatedCliente = await updateCliente(cliente.IdCliente);
      if (updatedCliente) {
        let indice = this.clientes.indexOf(cliente);
        this.clientes.splice(indice, 1);
      }
    },
    clear(){
      this.cliente = {};
      this.endereco ={};
    }
  },
}
</script>

<style>
</style>
