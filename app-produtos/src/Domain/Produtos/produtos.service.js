import axios from "../../plugins/axios";
export default class ProdutosService {
    async get() {
        // return await produtos;
        return await axios.get("/produtos");
    }

    async getByID(IdProduto) {
        return await axios.get(`/produtos/${IdProduto}`);
    }

    async post(produto) {
        return await axios.post("/produtos/", produto);
    }

    async put(produto) {
        return await axios.put(`/produtos/${produto.idProduto}`, produto);
    }

    async delete(idProduto) {
        return await axios.delete(`/produtos/${+idProduto}`);
    }
}