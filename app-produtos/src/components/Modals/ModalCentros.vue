<template>  
  <div>
      <div v-if="showModal" id="janelamodal">
        <div class="container"> <!-- Inicio do container  -->
            <div class="row">
            <div class="col-md-6 offset-md-3">
            
            <h2 class="bg-primary text-center text-white border border-info rounded m-0 pb-2">Atualizar Centro</h2>
            <form class="form-control"> <!-- Inicio do form  -->
              <label>Cep</label>
              <input type="number" placeholder="Cep do centro" v-model.number="endereco.cep" class="form-control">
              <label>Estado</label>
              <input type="text" placeholder="Estado do centro" v-model="endereco.estado" class="form-control">
              <label>Cidade</label>
              <input type="text" placeholder="Cidade do centro" v-model="endereco.cidade" class="form-control">
              <label>Rua</label>
              <input type="text" placeholder="Rua do centro" v-model="endereco.rua" class="form-control">
              <label>Bairro</label>
              <input type="text" placeholder="Bairro do centro" v-model="endereco.bairro" class="form-control">
              <label>Codigo</label>
              <input type="number" placeholder="Codigo do centro" v-model.number="centro.codigoCentro" class="form-control">

              <button @click.prevent="handleUpdateCentro()" class="btn-lg mx-2 btn-success text-white fas fa-edit mt-2"> Editar</button>
              <button @click.prevent="fechar()" class="btn-lg btn-secondary fas fa-sign-out-alt  mt-2"> Voltar</button>
            </form> <!-- Fim do container  -->
            </div>
            </div>
        </div> <!-- Fim do container  -->
    </div>  
</div>
</template>

<script>

import { bus } from '../../main.js'
import useCentros from "../../Composables/Centros/UseCentros";
import useEnderecos from "../../Composables/Enderecos/UseEnderecos";

const { updateCentro } =
useCentros();
const {updateEndereco} =
useEnderecos();

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
      this.centroPrev.Nome = data.nomeCentro;
      this.centroPrev.Codigo = data.codigoCentro;
      });

      bus.$on('editarendereco', (data) =>{
      this.showModal = true;
      this.enderecos = data;

      this.enderecos.forEach(endereco => {
        if(endereco.idEndereco == this.centro.idEndereco){
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
    //salvar edição
    async handleUpdateCentro(){
      await updateEndereco(this.endereco)
      await updateCentro(this.centro);
      this.showModal = false;
    },

    //fechar janela
    fechar(){
      this.centro.nomeCentro = this.centroPrev.Nome;
      this.centro.codigoCentro = this.centroPrev.Codigo;
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