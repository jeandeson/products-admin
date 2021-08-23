import PeoplesService from "../../domain/Peoples/peoples.service.js";

export default function usePeoples() {
  const peoplesService = new PeoplesService();

  async function getPeoples() {
    let peoples = [];
    try {
      const { data } = await peoplesService.get();
      peoples = data;
      // peoples = await peoplesService.get();
    } catch {
      console.log("Failed to get peoples");
    }

    return peoples;
  }

  async function getPeople(peopleID) {
    let people;
    try {
      const { data } = await peoplesService.getByID(peopleID);
      people = data;
    } catch (err) {
      console.log("Failed to get peoples", err);
    }

    return people;
  }

  async function deletePeople(deleteID) {
    let result = deleteID;
    try {
      const { data } = await peoplesService.delete(deleteID);
      result = data;
    } catch (err) {
      console.log("Failed to delete people", err);
    }

    return result;
  }

  async function createPeople(people) {
    let result;
    try {
      const { data } = await peoplesService.post(people);
      result = data;
    } catch {
      console.log("Failed to create peopl");
    }

    return result;
  }

  async function updatePeople(people) {
    let result;
    try {
      const { data } = await peoplesService.put(people);
      result = data;
    } catch (err) {
      console.log("Failed to update peopl", err);
    }

    return result;
  }

  return { getPeoples, getPeople, deletePeople, createPeople, updatePeople };
}
