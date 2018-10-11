<template>
  <div class="home">
    <v-toolbar class="white--text" color="#353438">
      <v-toolbar-title>Keepr</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-text-field v-if="search" flat label="Search" v-model="searched" solo-inverted></v-text-field>
      <v-btn icon>
        <v-icon @click="search = !search">search</v-icon>
      </v-btn>
      <v-toolbar-items>
        <v-btn class="white--text" @click="logout" flat>Logout</v-btn>
        <v-btn class="white--text" @click="dashboard = !dashboard" flat>Dashboard</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-container grid-list-md fluid>
      <keep v-bind:filteredkeeps="filteredkeeps" />
      <v-speed-dial fixed bottom right>
        <v-btn slot="activator" v-model="fab" color="darken-2" dark fab>
          <v-icon>account_circle</v-icon>
          <v-icon>close</v-icon>
        </v-btn>
        <v-btn fab dark small color="green">
          <v-icon>edit</v-icon>
        </v-btn>
        <v-btn fab dark small color="indigo">
          <v-icon>add</v-icon>
        </v-btn>
        <v-btn fab dark small color="red">
          <v-icon>delete</v-icon>
        </v-btn>
      </v-speed-dial>
    </v-container>
  </div>
</template>

<script>
import keep from "../components/keep.vue";
export default {
  name: "home",
  data() {
    return {
      dashboard: false,
      search: false,
      searched: "",
      direction: "top",
      fab: false,
      fling: false,
      hover: false,
      tabs: null,
      top: false,
      right: true,
      bottom: true,
      left: false,
      transition: "slide-y-reverse-transition"
    };
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  mounted() {
    //blocks users not logged in
    if (!this.$store.state.user.id) {
      this.$router.push({ name: "login" });
    }
  },
  computed: {
    filteredkeeps: function() {
      return this.$store.state.keeps.filter(keep => {
        return keep.name.match(this.searched);
      });
    }
  },
  components: {
    keep
  }
};
</script>

<style>
</style>
