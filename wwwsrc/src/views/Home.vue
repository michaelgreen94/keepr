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
  </div>
</template>

<script>
export default {
  name: "home",
  data() {
    return {
      dashboard: false,
      search: false,
      searched: ""
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
  }
};
</script>