import Vue from 'vue'
import App from './App.vue'
import vueResource from 'vue-resource'
import fontawesome from './assets/fontawesome/css/fontawesome.min.css'
import * as VueGoogleMaps from 'vue2-google-maps'
import VueConfirmDialog from 'vue-confirm-dialog'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import JwPagination from 'jw-vue-pagination';
import VueCompositionAPI from '@vue/composition-api'
import router from './Router/index'

import Vuex from 'vuex'
Vue.use(Vuex)

Vue.use(VueGoogleMaps, {
    load: {
        key: 'AIzaSyAjRnMrLQlHMXp9QIaN4CiC6LRBvOcuZwE',
        libraries: 'places',
    },
});

Vue.use(fontawesome);
Vue.use(vueResource);
Vue.use(VueConfirmDialog);
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(VueCompositionAPI);


import store from "./store/index";
import VueTheMask from "vue-the-mask";



Vue.use(store).use(VueTheMask);

Vue.component('vue-confirm-dialog', VueConfirmDialog.default);
Vue.component('jw-pagination', JwPagination);

Vue.config.productionTip = false;
export const bus = new Vue();

new Vue({
    store,
    router,
    render: h => h(App),
}).$mount('#app')