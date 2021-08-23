import axios from "../../plugins/axios";
export default class ClientesService {
    async get() {
        // return await clientes;
        return await axios.get("/clientes");
    }

    async getByID(IdCliente) {
        return await axios.get(`/clientes${IdCliente}`);
    }

    async post(cliente) {
        return await axios.post("/clientes", cliente);
    }

    async put(cliente) {
        console.log(cliente)
        return await axios.put(`/clientes/${cliente.idCliente}`, cliente);
    }

    async delete(IdCliente) {
        return await axios.delete(`/clientes/${+IdCliente}`);
    }
}