<template>
<div>
  <ModalClientes/>
  <vue-confirm-dialog></vue-confirm-dialog>
  <div class="container"> <!--- Inicio do container principal  --->
      <div class="row">
          <div class="col-md-12"> <!--- Inicio da coluna form  --->
                <form class="form-group p-2 shadow border bg-white"> <!--- Inicio do form  --->
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
                        <button class="btn-lg btn-primary fas fa-save mt-2" @click.prevent="salvar()"> Salvar</button>
                        <button @click.prevent="Limparcampos()" class="btn-lg btn-success fas fa-broom mt-2"> Limpar</button>
                   </form> <!--- Fim do container  --->

            </div> <!--- fim da coluna form  --->

          <div class="container"> <!--- Inicio do container table  --->
              <div class="row">
                <div class="col-md-12 text-center">
                  <table class="table table-striped align-middle"> <!--- Inicio do table head  --->
                  <thead class="bg-info">
                      <tr class="text-white">
                      <th scope="col">NOME</th>
                      <th scope="col">EMAIL</th>
                      <th scope="col">OPÇÕES</th>
                      </tr>
                  </thead> <!--- fim do table head  --->
                  <tbody> <!--- incio do table body  --->
                    <tr v-for="cliente of clientes" :key="cliente.id">
                      <td scope="row">{{cliente.NomeCliente}} {{cliente.SobrenomeCliente}}</td>
                      <td>{{cliente.EmailCliente}}</td>
                      <td>
                          <button @click.prevent="editar(cliente)" class="btn btn-primary"><i class="material-icons">create</i></button>
                          <button @click.prevent="remover(cliente)" class="btn btn-danger"><i class="material-icons">delete_sweep</i></button>
                      </td>
                    </tr>
                  </tbody> <!--- fim do table body  --->
                  </table> <!--- fim da  da table  --->
                </div>
             </div>
          </div> <!--- fim do container da table  --->
      </div>
    </div> <!--- fim  do container principal  --->
  </div>
</template>


<script>
import axios from 'axios'
import { bus } from '../main.js'
import Cliente from '../services/clientes'
import Titulo from './Titulo.vue'
import Endereco from '../services/enderecos'
import ModalClientes from './ModalClientes.vue'
import Notificacoes from '../services/Notificacoes.js'

export default {

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
      clientes:[]
    }
  },

    components: {
    Titulo,
    ModalClientes
  },

  mounted() {
    this.listar();
  },

  methods: {
    //salva o cliente
    async salvar() {
      await axios.post("http://localhost:5000/api/enderecos", this.endereco).then(response => {
      this.cliente.IdEndereco = response.data.IdEndereco;
    });
      JSON.stringify(this.cliente);
      await axios.post('http://localhost:5000/api/clientes', this.cliente)
      .then(response => {
          Notificacoes.successmsg();
          console.log(response);
          this.listar();
          this.cliente.complemento = null;
      })
      .catch(function(error) {
          console.log(error);
          axios.delete('http://localhost:5000/api/enderecos/' + this.cliente.IdEndereco)
      });
    },
    //lista os clientes na table
    listar(){
      Endereco.listar().then(resposta =>{
      this.enderecos = resposta.data;
      this.Limparcampos();
      });
      Cliente.listar().then(resposta =>{
      this.clientes = resposta.data;
      this.Limparcampos();
      });
    },
    //remove o cliente da table
    async remover(cliente){
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
          callback: async confirm => {
            if (confirm) {
              let indice = this.clientes.indexOf(cliente);
              this.clientes.splice(indice, 1);
              await axios.delete('http://localhost:5000/api/clientes/' + cliente.IdCliente);
              await axios.delete('http://localhost:5000/api/enderecos/' + cliente.IdEndereco);
            }
          }
        }
      );
    },
    //edita o cliente
    editar(cliente){
      bus.$emit('editarcliente', cliente);
      bus.$emit('editarendereco', this.enderecos);
      this.showModal = true;
    },
    //limpa os campos do form
    Limparcampos(){
      Cliente.limpar(this.cliente);
      Endereco.limpar(this.endereco);
    }
  },
  
}

</script>

<style>
</style>
