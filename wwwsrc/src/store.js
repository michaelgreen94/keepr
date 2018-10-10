import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "//localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "//localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    activekeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setkeeps(state, keeps) {
      state.keeps = keeps
      state.activekeeps = keeps
    },
    setactivekeeps(state, keeps) {
      state.activekeeps = keeps
    }
  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    //keep actions
    getkeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setkeeps', res.data)
        })
    },
    getsearchedkeeps({ commit, dispatch }, search) {
      api.get('keeps')
        .then(res => {
          let keeps = res.data
          const results = keeps.filter(keep => keep.name.includes(search))
          commit('setactivekeeps', results)
        })
    }
  }
})