import CentrosService from "../../Domain/Centros/centros.service";

export default function useCentros() {
    const centrosService = new CentrosService();

    async function getCentros() {
        let centros = [];
        try {
            const { data } = await centrosService.get();
            centros = data;
            // centros = await centrosService.get();
        } catch {
            console.log("Failed to get centros");
        }

        return centros;
    }

    async function getCentro(IdCentro) {
        let centro;
        try {
            const { data } = await centrosService.getByID(IdCentro);
            centro = data;
        } catch (err) {
            console.log("Failed to get centros", err);
        }

        return centro;
    }

    async function deleteCentro(IdCentro) {
        let result = IdCentro;
        try {
            const { data } = await centrosService.delete(IdCentro);
            result = data;
        } catch (err) {
            console.log("Failed to delete centro", err);
        }

        return result;
    }

    async function createCentro(centro) {
        let result;
        try {
            const { data } = await centrosService.post(centro);
            result = data;
        } catch {
            console.log("Failed to create centro");
        }

        return result;
    }

    async function updateCentro(centro) {
        let result;
        try {
            const { data } = await centrosService.put(centro);
            result = data;
        } catch (err) {
            console.log("Failed to update centro", err);
        }

        return result;
    }

    return { getCentros, getCentro, deleteCentro, createCentro, updateCentro };
}