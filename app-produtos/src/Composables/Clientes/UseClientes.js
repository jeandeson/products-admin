import ClientesService from "../../Domain/Clientes/clientes.service";

export default function useClientes() {
    const clientesService = new ClientesService();

    async function getClientes() {
        let clientes = [];
        try {
            const { data } = await clientesService.get();
            clientes = data;
            // clientes = await clientesService.get();
        } catch {
            console.log("Failed to get clientes");
        }

        return clientes;
    }

    async function getCliente(IdCliente) {
        let cliente;
        try {
            const { data } = await clientesService.getByID(IdCliente);
            cliente = data;
        } catch (err) {
            console.log("Failed to get clientes", err);
        }

        return cliente;
    }

    async function deleteCliente(IdCliente) {
        let result = IdCliente;
        try {
            const { data } = await clientesService.delete(IdCliente);
            result = data;
        } catch (err) {
            console.log("Failed to delete cliente", err);
        }

        return result;
    }

    async function createCliente(cliente) {
        let result;
        try {
            const { data } = await clientesService.post(cliente);
            result = data;
        } catch {
            console.log("Failed to create cliente");
        }

        return result;
    }

    async function updateCliente(cliente) {
        let result;
        try {
            const { data } = await clientesService.put(cliente);
            result = data;
        } catch (err) {
            console.log("Failed to update cliente", err);
        }

        return result;
    }

    return { getClientes, getCliente, deleteCliente, createCliente, updateCliente };
}