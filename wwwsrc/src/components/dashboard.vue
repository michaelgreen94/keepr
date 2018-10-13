<template>
  <v-layout row justify-center>
    <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
      <v-btn slot="activator" color="primary" dark>Dashboard</v-btn>
      <v-card>
        <v-toolbar dark color="primary">
          <v-btn icon dark @click.native="dialog = false">
            <v-icon>close</v-icon>
          </v-btn>
          <v-toolbar-title>Dashboard</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <vault/>
        <v-dialog v-model="vaultmodal" absolute scrollable width="45rem" transition="scale-transition">
            <v-card dark flat>
              <form ref="form">
                <v-text-field v-model="addvault.name" @submit.prevent="createvault" label="Name" required></v-text-field>
                <v-text-field v-model="addvault.description" label="Description" required></v-text-field>
                <v-btn type="submit" @click="createvault">Create</v-btn>
                <v-btn type="reset">Reset</v-btn>
              </form>
            </v-card>
      </v-dialog>
      <v-speed-dial fixed bottom left right>
        <v-btn slot="activator" color="darken-2" dark fab>
          <v-icon>add</v-icon>
        </v-btn>
        <v-btn @click="vaultmodal = !vaultmodal" dark>Add Vault</v-btn>
      </v-speed-dial>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
import vault from "../components/vault.vue";
export default {
  name: "dashboard",
  data() {
    return {
      dialog: false,
      vaultmodal: false,
      addvault: {
        name: "",
        description: ""
      }
    };
  },
  methods: {
    createvault() {
      this.$store.dispatch("createvault", this.addvault);
    }
  },
  components: {
    vault
  }
};
</script>