import axios from "../../plugins/axios";
export default class EnderecosService {
    async get() {
        // return await enderecos;
        return await axios.get("/enderecos");
    }

    async getByID(IdEndereco) {
        return await axios.get(`/${IdEndereco}`);
    }

    async post(endereco) {
        return await axios.post("/enderecos", endereco);
    }

    async put(endereco) {
        return await axios.put(`/enderecos/${endereco.idEndereco}`, endereco);
    }

    async delete(IdEndereco) {
        return await axios.delete(`/enderecos/${+IdEndereco}`);
    }
}