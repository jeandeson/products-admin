import Router from 'vue-router';
import Clientes from './components/Clientes.vue'
import Produtos from './components/Produtos.vue'
import Centros from './components/Centros.vue'




export default new Router({
    routes: [{
            path: '/produtos',
            nome: 'produtos',
            components: Produtos
        },
        {
            path: '/clientes',
            nome: 'clientes',
            components: Clientes
        }, {
            path: '/centros',
            nome: 'centros',
            components: Centros
        },
        {
            path: '/mapacentros',
            nome: 'mapacentros',
            components: MapaCentros
        },
    ]
})