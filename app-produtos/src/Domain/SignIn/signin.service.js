import axios from "../../plugins/axios";


export default class SignIn {
    async get(body) {
        return await axios.post(`/users`, body);
    }
}