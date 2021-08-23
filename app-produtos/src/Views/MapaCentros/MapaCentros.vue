<template>
  <div class="container-fluid"> <!-- Inicio do container  -->
      <div class="row">
          <div class="col-md-6 offset-md-3 " style="padding:8px;"> 
            <titulo texto="Centros Cadastrados"/> 
            <GmapMap class="border border-5 rounded-3 shadow" :center="center" :map-type-id="mapTypeId" :zoom="9">
            <GmapMarker
                v-for="(item, index) in markers"
                :key="index"
                :position="item.position"
                :clickable="true"
                :icon="icon"
                @click="trocarInfoJanela(item, index)"
            />
            <gmap-info-window
                :options="infoOptions"
                :position="infoWindowPos"
                :opened="infoWinOpen"
                @closeclick="infoWinOpen = true"
            >
            </gmap-info-window>
            <gmap-polygon ref="polygon">
            </gmap-polygon>
            </GmapMap>
            <button class="btn btn-info mt-2" @click.prevent="mostrarMarkers()">Mostrar Marcações</button>
            <button class="btn btn-info mt-2 mx-2" @click.prevent="LimparMaps()">Remover Marcações</button>
          </div>
      </div>
  </div> <!-- Fim do container  -->
</template> 
 
<script>


import Notificacoes from '../../Domain/Notificacoes/Notificacoes.service'
import axios from 'axios'
import Titulo from '../../components/Titulo/Titulo.vue'
import useCentros from "../../Composables/Centros/UseCentros";
import useEnderecos from "../../Composables/Enderecos/UseEnderecos";
//import { bus } from '../../main'
//import Notificacoes from '../../Domain/Notificacoes/Notificacoes.service';

const { getCentros } =
useCentros();

const { getEnderecos } =
useEnderecos();

var indice = { lat: -12.8905201, lng: -38.3455045 };

export default {
  data() {
    return {
      infoText: "<strong>Centro</strong>",
      icon: {
            url: require("../../assets/to_location.png"),
            scaledSize: { width: 35, height: 35 },
          },
      center: indice,
      mapTypeId: "terrain",
      enderecos:[],
      centros:[],
      clientes:[],
      markers: [],
      infoWindowPos: null,
      infoWinOpen: false,
      currentMidx: null,
      infoOptions: {
        content: "",
        pixelOffset: {
          width: 0,
          height: -35,
        },
      },
    };
  },
  components: {
    Titulo,
  },

  created() {
    this.pegarcentros();
  }, 

  methods: {
    //metodo principal que faz a conversão de endereços em Latitude e Longitude
    async pusharray(){   
        this.centros.forEach(centro => {
          axios.get(`https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyAjRnMrLQlHMXp9QIaN4CiC6LRBvOcuZwE&address=${centro.IdEnderecoNavigation.rua}${centro.IdEnderecoNavigation.cidade}${centro.IdEnderecoNavigation.bairro}${centro.IdEnderecoNavigation.estado}${centro.IdEnderecoNavigation.cep}`).then(response => {
            if(response.data.results.length != 0){
              centro.position = response.data.results[0].geometry.location;
              this.markers.push(centro);
            }
            else{
              Notificacoes.mapaerro(centro);
            }
         });
      });
    },
       
     //ao clicar mostra as informações do centro na janela de info
    trocarInfoJanela: function(marker, idx) {
      this.infoWindowPos = marker.position;
      this.infoOptions.content = '<strong>' + marker.nomeCentro +'<br>'+ "Codigo do centro: " + marker.codigoCentro + '<br>' + 'Bairro: ' + marker.IdEnderecoNavigation.bairro + '<br>' + "Rua: "+marker.IdEnderecoNavigation.rua + '<br>' + "Cep: "+marker.IdEnderecoNavigation.cep;

      //checa se este mesmo marker foi selecionado se sim troca
      if (this.currentMidx == idx) {
        this.infoWinOpen = !this.infoWinOpen;
      }
      //se um marcador diferente for colocando no infowindow reseta o indice atual
      else {
        this.infoWinOpen = true;
        this.currentMidx = idx;
      }
    },
    //mostra os markers no mapa
    mostrarMarkers() {
      if(this.markers.length == 0){
        this.pusharray();
      }
    },
    //lista os clientes e passa para o array
    async pegarcentros(){
        var centro_temp;
        var end_temp;
        let i = 0;

        centro_temp = await getCentros();
        end_temp = await getEnderecos();

        end_temp.forEach(endereco => {
          if(centro_temp.length != i && endereco.idEndereco == centro_temp[i].idEndereco){ 
            centro_temp[i].IdEnderecoNavigation = endereco;
            i++;
          }
          if(i >= centro_temp.length){
              return;
            }
        });
        this.centros = centro_temp;
        this.pusharray();
    },
    LimparMaps() {
      this.markers = [];
      this.infoWinOpen = false;
    },
  },
};
</script>

<style scoped>
h1{
  color: #287ae6 !important;
}
.vue-map-container {
  height: 450px;
  max-width: 992px;
  width: 100%;
}
.btn{
  background-color: #e74940 !important;
}
</style>
