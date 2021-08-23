import ProdutosService from "../../Domain/Produtos/produtos.service";

export default function useProdutos() {
    const produtosService = new ProdutosService();

    async function getProdutos() {
        let produtos = [];
        try {
            const { data } = await produtosService.get();
            produtos = data;
            // produtos = await produtosService.get();
        } catch {
            console.log("Failed to get produtos");
        }

        return produtos;
    }

    async function getProduto(IdProduto) {
        let produto;
        try {
            const { data } = await produtosService.getByID(IdProduto);
            produto = data;
        } catch (err) {
            console.log("Failed to get produtos", err);
        }

        return produto;
    }

    async function deleteProduto(IdProduto) {
        let result = IdProduto;
        try {
            const { data } = await produtosService.delete(IdProduto);
            result = data;
        } catch (err) {
            console.log("Failed to delete produto", err);
        }

        return result;
    }

    async function createProduto(produto) {
        let result;
        try {
            const { data } = await produtosService.post(produto);
            result = data;
        } catch {
            console.log("Failed to create produto");
        }

        return result;
    }

    async function updateProduto(produto) {
        let result;
        try {
            const { data } = await produtosService.put(produto);
            result = data;
        } catch (err) {
            console.log("Failed to update produto", err);
        }

        return result;
    }

    return { getProdutos, getProduto, deleteProduto, createProduto, updateProduto };
}