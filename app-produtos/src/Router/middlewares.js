import store from "../store/index";

export default function useMiddlewares() {
    function isValidToken() {
        const token = JSON.parse(localStorage.getItem("token"));
        token && store.commit("auth/SET_TOKEN", token);
    }

    function isSigned(to, from, next) {
        const isNotLogged = !store.state.auth.token;
        if (to.name !== "SignIn" && isNotLogged) {
            next({ name: "SignIn" });
        } else if (to.name === "SignIn" && !isNotLogged) {
            next({ name: "Produtos" });
        } else {
            next();
        }
    }

    return { isValidToken, isSigned };
}