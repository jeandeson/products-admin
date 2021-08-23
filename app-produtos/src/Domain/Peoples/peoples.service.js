import PEOPLES from "../../mocks/people";
import axios from "../../plugins/axios";
export default class PeoplesService {
    async get() {
        // return await PEOPLES;
        return await axios.get("/");
    }

    async getByID(id) {
        return await axios.get(`/${id}`);
    }

    async post(people) {
        return await axios.post("/", people);
    }

    async put(people) {
        return await axios.put(`/${people.id}`, people);
    }

    async delete(id) {
        return await axios.delete(`/${+id}`);
    }
}