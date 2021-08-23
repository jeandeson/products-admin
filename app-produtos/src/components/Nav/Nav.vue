<template>
    <div  v-show="shownav">
        <nav class="navbar navbar-expand-lg navbar-dark shadow mb-3 bg-white rounded" v-if="shownav">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
             </button>
            <div class="collapse navbar-collapse justify-content-center" id="navbarNavAltMarkup">
                <div class="navbar-nav">
                    <router-link class="nav-item nav-link" to="/produtos">Produtos</router-link>
                    <router-link class="nav-item nav-link" to="/clientes">Clientes</router-link>
                    <router-link class="nav-item nav-link" to="/centros">Centros.Dist</router-link>
                    <div class="dropdown">
                        <button class="btn btn-secondary bg-none dropdown-toggle text-primary" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            Mapas
                        </button>
                        <div class="dropdown-menu bg-info" aria-labelledby="dropdownMenuButton">
                            <router-link class="nav-item nav-link mx-2" to="/MapaCentros">Mapa Centros</router-link>
                             <router-link class="nav-item nav-link mx-2" to="/MapaClientes">Mapa Clientes</router-link>
                        </div>
                        </div>
                    <button class="btn btn-danger" @click="logout()">Logout</button>
                </div>
            </div>
        </nav>
    </div>
</template>

<script>
import router from "../../Router/index"
import store from "../../store/index";
import { bus } from '../../main';

export default {

    methods: {
        logout() {
            store.dispatch("auth/deleteToken");
            localStorage.removeItem("token");
            this.shownav = false
            router.push('/SignIn')
        },
    },

    data() {
        return {
            shownav: true,
        }
    },
    
    created() {
        if (localStorage.getItem("token")) {
            this.shownav = true
        }
        else{
            this.shownav = false
        }
        bus.$on('mostrarnav', () =>{
        if (localStorage.getItem("token")) {
           this.shownav = true
          }
        })
    },
    beforeDestroy() {
        bus.$off('mostrarnav')
    },
}
</script>

<style scoped>
    nav .nav-item.nav-link{
        color:white;
        font-size: 1.1em !important;
        font-weight: bold !important;
    }
    nav .nav-item.nav-link.logout{
        color:rgb(202, 50, 50);
        font-size: 1.1em !important;
        font-weight: bold !important;
    }
    nav .nav-item.nav-link.logout:hover{
        color:rgb(202, 33, 33);
    }
    nav .nav-item.nav-link:hover{
        color:rgb(248, 237, 237);
    }
    nav  .router-link-active{
        color: rgb(64, 66, 68) !important;

    }
    .navbar {
        background:#b7c3ce !important;
    }
    .btn {
        background: none !important;
        border: none !important;
        
    }
    .btn-check:checked{
         color: rgb(64, 66, 68) !important;
        font-size: 1.1em !important;
        font-weight: bold !important;
        box-shadow: none;
    }
    button:focus(:focus-visible) {
   outline: none !important;
}
.btn:focus {
    outline: none !important;
}   
.btn{
    box-shadow: none !important;;
    font-size: 1.1em !important;
    padding: 8px;
}


    
</style>