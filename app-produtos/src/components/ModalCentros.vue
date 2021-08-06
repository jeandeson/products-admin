<template>  
  <div>
      <div v-if="showModal" id="janelamodal">
        <div class="container"> <!--- Inicio do container  --->
            <div class="row">
            <div class="col-md-6 offset-md-3">
            
            <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Atualizar Centro</h2>
            <form class="form-control"> <!--- Inicio do form  --->
              <label>Cep</label>
              <input type="number" placeholder="Cep do centro" v-model.number="endereco.Cep" class="form-control">
              <label>Estado</label>
              <input type="text" placeholder="Estado do centro" v-model="endereco.Estado" class="form-control">
              <label>Cidade</label>
              <input type="text" placeholder="Cidade do centro" v-model="endereco.Cidade" class="form-control">
              <label>Rua</label>
              <input type="text" placeholder="Rua do centro" v-model="endereco.Rua" class="form-control">
              <label>Bairro</label>
              <input type="text" placeholder="Bairro do centro" v-model="endereco.Bairro" class="form-control">
              <label>Codigo</label>
              <input type="number" placeholder="Codigo do centro" v-model.number="centro.CodigoCentro" class="form-control">

              <button @click.prevent="salvaredit()" class="btn-lg mx-2 btn-success text-white fas fa-edit mt-2"> Editar</button>
              <button @click.prevent="fechar()" class="btn-lg btn-secondary fas fa-sign-out-alt  mt-2"> Voltar</button>
            </form> <!--- Fim do container  --->
            </div>
            </div>
        </div> <!--- Fim do container  --->
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
      endereco:{
          Bairro: '',
          Cep: null,
          Cidade: '',
          Estado: '',
          Rua: '',
          IdEndereco: null,
      },
      centroPrev:{
          type: Object
        },
        endprev:{
          type: Object
        },
      centro:{
      type: Object
      } ,
      enderecos:{
          type: Array
      },
      showModal: false,
      }
  },
    
  created() {
      bus.$on('editarcentro', (data) =>{
      this.showModal = true;
      this.centro = data;
      this.centroPrev.nome = data.NomeCentro;
      this.centroPrev.codigo = data.CodigoCentro;
      });

      bus.$on('editarendereco', (data) =>{
      this.showModal = true;
      this.enderecos = data;

      this.enderecos.forEach(endereco => {
        if(endereco.IdEndereco == this.centro.IdEndereco){
          this.endereco = endereco;
          this.endprev.cep = endereco.Cep
          this.endprev.bairro = endereco.Bairro
          this.endprev.cidade = endereco.Cidade
          this.endprev.rua = endereco.Rua
          this.endprev.estado = endereco.Estado
        }
      });
    });
  },

  methods: {
    //salvar edição
    salvaredit(){
        this.centro.NomeCentro = "Centro de " + this.endereco.Cidade;
        axios.put('http://localhost:5000/api/enderecos/' + this.endereco.IdEndereco, {
          IdEndereco: this.endereco.IdEndereco,
          Estado:this.endereco.Estado,
          Cidade:this.endereco.Cidade, 
          Bairro:this.endereco.Bairro, 
          Rua:this.endereco.Rua,
          Cep:this.endereco.Cep, 
      })
      .then(response => {
        Notificacoes.successmsg(response)
      });
        axios.put('http://localhost:5000/api/centros/' + this.centro.IdCentro, {
          IdCentro: this.centro.IdCentro,
          CodigoCentro: this.centro.CodigoCentro, 
          NomeCentro:  this.centro.NomeCentro,  
          IdEndereco:  this.centro.IdEndereco,  
      })
      .then(response => {
        Notificacoes.successmsg(response)
        this.fechar();
        this.data = '';
      });
    },

    //fechar janela
    fechar(){
      this.centro.NomeCentro = this.centroPrev.nome;
      this.centro.CodigoCentro = this.centroPrev.codigo;
      this.endereco.Cep = this.endprev.cep;
      this.endereco.Bairro = this.endprev.bairro ;
      this.endereco.Cidade = this.endprev.cidade;
      this.endereco.Rua = this.endprev.rua;
      this.endereco.Estado = this.endprev.estado;
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