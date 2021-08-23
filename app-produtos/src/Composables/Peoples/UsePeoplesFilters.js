export default function usePeoplesFilters() {
  let conditions = [];

  function useFilters(peoples, filters) {
    filters.value.name && conditions.push(searchByName);
    filters.value.cpf && conditions.push(searchByCPF);

    if (conditions) {
      return peoples.value.filter((people) => {
        return conditions.every((condition) =>
          condition(people, filters.value)
        );
      });
    }
  }

  function searchByName(people, { name }) {
    return people.name.toLowerCase().includes(name);
  }

  function searchByCPF(people, { cpf }) {
    return people.cpf.includes(cpf);
  }

  return { useFilters };
}
