import Vue from 'vue'
import App from './App.vue'
import vueResource from 'vue-resource'
import fontawesome from './assets/fontawesome/css/fontawesome.min.css'
import * as VueGoogleMaps from 'vue2-google-maps'
import VueConfirmDialog from 'vue-confirm-dialog'

import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

import VueRouter from 'vue-router';

Vue.use(VueRouter);

import Produtos from './components/Produtos.vue';
import Clientes from './components/Clientes.vue';
import Centros from './components/Centros.vue';
import MapaCentros from './components/MapaCentros.vue';
import MapaClientes from './components/MapaClientes.vue';
import NotFound from './components/NotFound.vue';
import Home from './components/Home.vue';



const router = new VueRouter({
    mode: 'history',
    base: __dirname,
    routes: [
        { path: '/', name: 'Home', component: Home },
        { path: '/produtos', component: Produtos },
        { path: '/clientes', component: Clientes },
        { path: '/centros', component: Centros },
        { path: '/MapaCentros', component: MapaCentros },
        { path: '/MapaClientes', component: MapaClientes },
        { path: '*', component: NotFound },
    ]
});

Vue.use(VueGoogleMaps, {
    load: {
        key: 'AIzaSyAjRnMrLQlHMXp9QIaN4CiC6LRBvOcuZwE',
        libraries: 'places',
    },
});

Vue.use(fontawesome);
Vue.use(vueResource);
Vue.use(router);
Vue.use(VueConfirmDialog);
Vue.component('vue-confirm-dialog', VueConfirmDialog.default);

Vue.config.productionTip = false;

export const bus = new Vue();

new Vue({
    router,
    render: h => h(App),
}).$mount('#app')