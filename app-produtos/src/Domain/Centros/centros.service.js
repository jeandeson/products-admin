import axios from "../../plugins/axios";
export default class CentrosService {
    async get() {
        // return await centros;
        return await axios.get("/centros");
    }

    async getByID(IdCentro) {
        return await axios.get(`/${IdCentro}`);
    }

    async post(centro) {
        return await axios.post("/centros", centro);
    }

    async put(centro) {
        return await axios.put(`/centros/${centro.idCentro}`, centro);
    }

    async delete(IdCentro) {
        return await axios.delete(`/centros/${+IdCentro}`);
    }
}