<template>  
  <div>
      <div v-if="showModal" id="janelamodal">
        <div class="container"> <!-- Inicio da container  -->
            <div class="row">
            <div class="col-md-6 offset-md-3">
            
            <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Atualizar cliente</h2>
            <form class="form-control"> <!-- Inicio da form  -->
              <label>Nome</label>
              <input type="text" placeholder="Nome do cliente" v-model="cliente.nomeCliente" class="form-control">
              <label>Sobrenome</label>
              <input type="text" placeholder="Sobrenome do cliente" v-model="cliente.sobrenomeCliente" class="form-control">
              <label>Email</label>
              <input type="text" placeholder="Email do cliente" v-model="cliente.emailCliente" class="form-control">
              <label>Cep</label>
              <input type="number" placeholder="Cep do cliente" v-model.number="endereco.cep" class="form-control">
              <label>Estado</label>
              <input type="text" placeholder="Estado do cliente" v-model="endereco.estado" class="form-control">
              <label>Cidade</label>
              <input type="text" placeholder="Cidade do cliente" v-model="endereco.cidade" class="form-control">
              <label>Rua</label>
              <input type="text" placeholder="Rua do cliente" v-model="endereco.rua" class="form-control">
              <label>Bairro</label>
              <input type="text" placeholder="Bairro do cliente" v-model="endereco.bairro" class="form-control">

              <button @click.prevent="handleUpdateCliente()" class="btn-lg mx-2 btn-success text-white fas fa-edit mt-2"> Editar</button>
              <button @click.prevent="fechar()" class="btn-lg btn-secondary fas fa-sign-out-alt  mt-2"> Voltar</button>
            </form> <!-- Fim da form  -->
            </div>
            </div>
        </div> <!-- Fim da container  -->
    </div>  
</div>
</template>

<script>

import { bus } from '../../main.js'
import useClientes from "../../Composables/Clientes/UseClientes";
import useEnderecos from "../../Composables/Enderecos/UseEnderecos";
const { updateCliente } =
useClientes();
const {updateEndereco} =
useEnderecos();
//import Notificacoes from '../../Domain/Notificacoes/Notificacoes.service'


export default {
  data() {
    return {
        endereco:{
            bairro: null,
            cep: null,
            cidade: null,
            estado: null,
            rua: null,
            idEndereco: null,
        },
        cliente:{
        type: Object
        } ,
        clientePrev:{
          type: Object
        },
        endprev:{
          type: Object
        },
        showModal: false,
        
        enderecos:{
            type: Array
        },
     }
  },
    
  mounted() {
      bus.$on('editarcliente', (data) =>{
      this.showModal = true;
      this.cliente = data;
      this.clientePrev.Nome = data.nomeCliente;
      this.clientePrev.Sobrenome = data.sobrenomeCliente;
      this.clientePrev.Email = data.emailCliente;
      });

      bus.$on('editarendereco', (data) =>{
      this.showModal = true;
      this.enderecos = data;

      this.enderecos.forEach(endereco => {
        if(endereco.idEndereco == this.cliente.idEndereco){
          this.endereco = endereco;
          this.endprev.Cep = endereco.cep
          this.endprev.Bairro = endereco.bairro
          this.endprev.Cidade = endereco.cidade
          this.endprev.Rua = endereco.rua
          this.endprev.Estado = endereco.estado
        }
      });
    });
  },

  methods: {
    async handleUpdateCliente(){
      await updateEndereco(this.endereco)
      await updateCliente(this.cliente);
      this.showModal = false;
    },

    fechar(){
      this.cliente.sobrenomeCliente = this.clientePrev.Sobrenome;
      this.cliente.nomeCliente = this.clientePrev.Nome;
      this.cliente.emailCliente = this.clientePrev.Email;
      this.endereco.cep = this.endprev.Cep;
      this.endereco.bairro = this.endprev.Bairro ;
      this.endereco.cidade = this.endprev.Cidade;
      this.endereco.rua = this.endprev.Rua;
      this.endereco.estado = this.endprev.Estado;
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