<template>
  <div class="home">
    <v-toolbar class="white--text" color="#353438">
      <v-toolbar-title>Keepr</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-text-field v-if="search" flat label="Search" v-model="searched" solo-inverted></v-text-field>
      <v-btn icon>
        <v-icon @click="search = !search">search</v-icon>
      </v-btn>
    </v-toolbar>
    <v-layout row justify-center>
      <v-dialog v-model="addkeep" absolute max-width="45rem" transition="scale-transition">
        <v-card dark flat>
          <v-toolbar class="white--text" color="#353438">
            <v-toolbar-title>Add Keep</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn flat @click="addkeep = !addkeep"><i class="fas fa-times fa-2x vclose"></i></v-btn>
          </v-toolbar>
          <v-container>
          <form ref="form" @submit.prevent>
            <v-text-field v-model="create.name" label="Name" required></v-text-field>
            <v-text-field v-model="create.description" label="Description" required></v-text-field>
            <v-text-field v-model="create.img" label="Img-URL"></v-text-field>
            <v-btn v-if="create.isprivate == false" @click="create.isprivate = true">Public</v-btn>
            <v-btn v-else @click="create.isprivate = false">Private</v-btn>
            <v-btn type="submit" @click="createkeep">Create</v-btn>
            <v-btn @click="clear">Clear</v-btn>
          </form>
          </v-container>
        </v-card>
      </v-dialog>
    </v-layout>
    <div class="hellouser">
      <h1>Hello {{user.username}}!</h1>
    </div>
    <keep v-bind:filteredkeeps="filteredkeeps" />
    <v-speed-dial fixed bottom right direction="left">
      <v-btn slot="activator" color="darken-2" dark fab>
        <v-icon>add</v-icon>
      </v-btn>
      <dashboard />
      <v-btn @click="addkeep = !addkeep" dark>Add Keep</v-btn>
      <v-btn  dark @click="logout">Logout</v-btn>
    </v-speed-dial>
  </div>
</template>

<script>
import dashboard from "../components/dashboard.vue";
import keep from "../components/keep.vue";
export default {
  name: "home",
  data() {
    return {
      dashboard: false,
      search: false,
      searched: "",
      addkeep: false,
      create: {
        name: "",
        description: "",
        img: "",
        isprivate: false
      }
    };
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
    createkeep() {
      this.$store.dispatch("addkeep", this.create);
      this.addkeep = false;
    },
    clear() {
      this.create.name = "";
      this.create.description = "";
      this.create.img = "";
      this.create.isprivate = false;
    }
  },
  mounted() {
    //blocks users not logged in
    if (!this.$store.state.user.id) {
      this.$router.push({ name: "login" });
      this.$store.dispatch("getvaults");
    }
  },
  computed: {
    filteredkeeps: function() {
      return this.$store.state.keeps.filter(keep => {
        return keep.name.match(this.searched);
      });
    },
    user() {
      return this.$store.state.user;
    }
  },
  components: {
    keep,
    dashboard
  }
};
</script>

<style>
.hellouser {
  width: 100vw;
  height: 10vh;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>