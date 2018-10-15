<template>
  <div class="vault">
<!-- shows vaults -->
    <div v-for="vault in vaults" :key="vault.id" class="vaultcard">
      <h1>{{vault.name}}</h1>
      <p>{{vault.description}}</p>
      <v-btn @click="getactivevault(vault.id)" >Open</v-btn>
      <v-btn @click="setactivevault(vault.id)">edit</v-btn>
      <i class="delete far fa-trash-alt" @click="deletevault(vault.id)"></i>
    </div>
    <!-- edit vault modal -->
    <v-dialog v-model="vaultmodal" absolute scrollable width="45rem" transition="scale-transition">
      <v-card dark flat>
        <form ref="form" @submit.prevent="editvault">
          <v-text-field v-model="activevault.name" label="Name" required></v-text-field>
          <v-text-field v-model="activevault.description" label="Description" required></v-text-field>
          <v-btn type="submit" @click="editvault(activevault.id)">edit</v-btn>
          <v-btn type="reset">Reset</v-btn>
        </form>
      </v-card>
    </v-dialog>
    <!-- fullscreen vault view to show keeps -->
    <v-dialog v-model="viewvaultmodal" fullscreen hide-overlay transition="dialog-bottom-transition">
      <v-card>
        <v-toolbar dark color="primary">
          <v-btn icon dark @click="viewvaultmodal = !viewvaultmodal">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>{{activevault.name}}</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
      <div class="vault">
        <div class="vaultkeepcard" v-for="keep in vaultkeeps" :key="keep.id">
         {{keep.name}}
        </div>
          <v-btn @click="vaultmodal = true">edit</v-btn>
      </div>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
// import keep from "../components/keep.vue";
export default {
  name: "vault",
  data() {
    return {
      viewvaultmodal: false,
      vaultmodal: false,
      addvault: {
        name: "",
        description: ""
      }
    };
  },
  methods: {
    deletevault(id) {
      this.$store.dispatch("deletevault", id);
    },
    editvault() {
      this.$store.dispatch("updatevault", this.activevault);
      this.vaultmodal = false;
    },
    getactivevault(vaultid) {
      this.viewvaultmodal = true;
      this.$store.dispatch("getactivevault", vaultid);
      this.$store.dispatch("getvaultkeeps", vaultid);
    },
    setactivevault(vaultid) {
      this.vaultmodal = true;
      this.$store.dispatch("getactivevault", vaultid);
      this.$store.dispatch("getvaultkeeps", vaultid);
    }
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
    activevault() {
      return this.$store.state.activevault;
    },
    vaultkeeps() {
      return this.$store.state.vaultkeeps;
    }
  }
  // mounted() {
  // }
  // components: {
  //   keep
  // }
};
</script>