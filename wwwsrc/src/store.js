import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

var production = !window.location.host.includes('localhost');
var baseUrl = production ? '//pinkeepr.herokuapp.com/' : '//localhost:5000/';
let api = Axios.create({
  baseURL: baseUrl + 'api/',
  timeout: 3000,
  withCredentials: true
})
let auth = Axios.create({
  baseURL: baseUrl + 'auth/',
  timeout: 3000,
  withCredentials: true
})

// let auth = Axios.create({
//   baseURL: "//localhost:5000/account/",
//   timeout: 3000,
//   withCredentials: true
// })

// let api = Axios.create({
//   baseURL: "//localhost:5000/api/",
//   timeout: 3000,
//   withCredentials: true
// })

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    vaults: [],
    activekeep: {},
    activevault: {},
    vaultkeeps: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setkeeps(state, keeps) {
      state.keeps = keeps
    },
    logout(state, data) {
      state.user = {}
    },
    updatekeeps(state, data) {
      state.keeps.push(data)
    },
    setactivekeep(state, keep) {
      state.activekeep = keep
    },
    setvaults(state, vaults) {
      state.vaults = vaults
    },
    setactivevault(state, vault) {
      state.activevault = vault
    },
    setvaultkeeps(state, keeps) {
      state.vaultkeeps = keeps
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
    logout({ commit, dispatch }) {
      auth.delete('logout')
        .then(res => {
          commit('logout', res)
          router.push({ name: 'login' })
        })
        .catch(err => console.error(err))
    },
    //keep actions
    getkeeps({ commit, dispatch }) {
      api.get('keeps')
        .then(res => {
          commit('setkeeps', res.data)
        })
    },
    getsinglekeep({ commit, dispatch }, id) {
      api.get('keeps/' + id)
        .then(res => {
          commit('setactivekeep', res.data)
        })
    },
    addkeep({ commit, dispatch }, payload) {
      api.post('keeps', payload)
        .then(res => {
          dispatch('getkeeps')
        })
    },
    deletekeep({ commit, dispatch }, id) {
      api.delete('keeps/' + id)
        .then(res => {
          dispatch('getkeeps')
        })
    },
    updatekeep({ commit, dispatch, state }, keep) {
      api.put('keeps', keep)
        .then(res => {
          dispatch('getkeeps')
          if (state.activekeep.id == keep.id) {
            commit('setactivekeep', res.data)
          }
        })
    },
    getvaults({ commit, dispatch }) {
      api.get('vault')
        .then(res => {
          commit('setvaults', res.data)
        })
    },
    createvault({ commit, dispatch }, vault) {
      api.post('vault', vault)
        .then(res => {
          dispatch('getvaults')
        })
    },
    deletevault({ commit, dispatch }, id) {
      api.delete('vault/' + id)
        .then(res => {
          dispatch('getvaults')
        })
    },
    updatevault({ commit, dispatch }, vault) {
      api.put('vault', vault)
        .then(res => {
          dispatch('getvaults')
        })
    },
    getvaultkeeps({ commit, dispatch }, vaultid) {
      api.get('vaultkeep/' + vaultid)
        .then(res => {
          commit('setvaultkeeps', res.data)
        })
    },
    getactivevault({ commit, dispatch }, vaultid) {
      api.get('vault/' + vaultid)
        .then(res => {
          commit('setactivevault', res.data)
        })
    },
    addtovault({ commit, dispatch }, payload) {
      api.post('vaultkeep', payload)
        .then(res => {

          dispatch("updatekeep", payload.keep)
        })
    }
  }
})