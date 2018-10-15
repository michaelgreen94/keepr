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
      <v-dialog v-model="addkeep" absolute scrollable width="45rem" transition="scale-transition">
            <v-card dark flat>
              <form ref="form">
                <v-text-field v-model="create.name" @submit.prevent="createkeep" label="Name" required></v-text-field>
                <v-text-field v-model="create.description" label="Description" required></v-text-field>
                <v-text-field v-model="create.img" label="Img-URL"></v-text-field>
                <v-btn v-if="create.isprivate == false" @click="create.isprivate = true">Public</v-btn>
                <v-btn v-else @click="create.isprivate = false">Private</v-btn>
                <v-btn type="submit" @click="createkeep">Create</v-btn>
                <v-btn type="reset">Reset</v-btn>
              </form>
            </v-card>
      </v-dialog>
      <keep v-bind:filteredkeeps="filteredkeeps" />
      <v-speed-dial fixed bottom left right>
        <v-btn slot="activator" color="darken-2" dark fab>
          <v-icon>account_circle</v-icon>
        </v-btn>
        <v-btn dark @click="logout">Logout</v-btn>
        <dashboard/>
        <v-btn @click="addkeep = !addkeep" dark>Add Keep</v-btn>
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
    }
  },
  components: {
    keep,
    dashboard
  }
};
</script>

<style>
</style>