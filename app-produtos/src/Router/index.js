import Router from 'vue-router';
import Produtos from '../Views/Produtos/Produtos';
import Clientes from '../Views/Clientes/Clientes';
import Centros from '../Views/Centros/Centros';
import MapaCentros from '../Views/MapaCentros/MapaCentros';
import MapaClientes from '../Views/MapaClientes/MapaClientes';
import NotFound from '../Views/NotFound/NotFound';
import Home from '../Views/Home/Home';
import SignIn from '../Views/Login/SignIn'
import Vue from 'vue'

Vue.use(Router)

export const router = new Router({
    mode: 'history',
    base: __dirname,
    routes: [
        { path: '/', name: 'Home', component: Home },
        { path: '/SignIn', name: 'SignIn', component: SignIn },
        { path: '/produtos', component: Produtos },
        { path: '/clientes', component: Clientes },
        { path: '/centros', component: Centros },
        { path: '/MapaCentros', component: MapaCentros },
        { path: '/MapaClientes', component: MapaClientes },
        { path: '*', component: NotFound },
    ]
});

router.beforeEach((to, from, next) => {
    const publicPages = ['/SignIn'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem("token");

    if (authRequired && !loggedIn) {
        return next('/SignIn');
    }

    next();
})

export default router;