import EnderecosService from "../../Domain/Enderecos/enderecos.service";

export default function useEnderecos() {
    const enderecosService = new EnderecosService();

    async function getEnderecos() {
        let enderecos = [];
        try {
            const { data } = await enderecosService.get();
            enderecos = data;
            // enderecos = await enderecosService.get();
        } catch {
            console.log("Failed to get enderecos");
        }

        return enderecos;
    }

    async function getEndereco(IdEndereco) {
        let endereco;
        try {
            const { data } = await enderecosService.getByID(IdEndereco);
            endereco = data;
        } catch (err) {
            console.log("Failed to get enderecos", err);
        }

        return endereco;
    }

    async function deleteEndereco(IdEndereco) {
        let result = IdEndereco;
        try {
            const { data } = await enderecosService.delete(IdEndereco);
            result = data;
        } catch (err) {
            console.log("Failed to delete endereco", err);
        }

        return result;
    }

    async function createEndereco(endereco) {
        let result;
        try {
            const { data } = await enderecosService.post(endereco);
            result = data;
        } catch {
            console.log("Failed to create endereco");
        }

        return result;
    }

    async function updateEndereco(endereco) {
        let result;
        try {
            const { data } = await enderecosService.put(endereco);
            result = data;
        } catch (err) {
            console.log("Failed to update endereco", err);
        }

        return result;
    }

    return { getEnderecos, getEndereco, deleteEndereco, createEndereco, updateEndereco };
}