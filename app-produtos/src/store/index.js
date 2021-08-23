import auth from "./Auth/index";
import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const createStore = new Vuex.Store({
    state: {},
    mutations: {},
    actions: {},
    modules: {
        auth,
    },
});

export default createStore;