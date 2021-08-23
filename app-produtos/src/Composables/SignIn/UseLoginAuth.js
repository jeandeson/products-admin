import SignInService from "../../Domain/SignIn/signin.service";
import store from "../../store/index";

export default function useLoginAuth() {
    async function login(signIn) {
        try {
            const signInService = new SignInService();
            const { data } = await signInService.get(signIn);
            localStorage.setItem("token", JSON.stringify(data.id));
            store.dispatch("auth/createToken", data);
        } catch (err) {
            console.log("User not found");
        }
        return store.state.auth.token != null;
    }

    function logout() {
        store.dispatch("auth/deleteToken");
        localStorage.removeItem("token");
    }

    return { login, logout };
}