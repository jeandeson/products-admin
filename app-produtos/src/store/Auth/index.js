export default {
  namespaced: true,

  state: {
    token: null,
  },

  mutations: {
    SET_TOKEN: (state, token) => (state.token = token),
  },

  actions: {
    createToken({ commit }, token) {
      commit("SET_TOKEN", token);
    },
    deleteToken({ commit }) {
      commit("SET_TOKEN", null);
    },
  },
};
