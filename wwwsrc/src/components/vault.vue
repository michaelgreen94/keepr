<template>
  <div class="vault">
    <div v-for="vault in vaults" :key="vault.id" class="vaultcard">
      <h1>{{vault.name}}</h1>
      <p>{{vault.description}}</p>
      <i @click="vaultmodal = true" class="korvue fab fa-korvue">edit</i>
      <i class="delete far fa-trash-alt" @click="deletevault(vault.id)"></i>
    </div>
    <v-dialog v-model="vaultmodal" absolute scrollable width="45rem" transition="scale-transition">
      <v-card dark flat>
        <form ref="form">
          <v-text-field v-model="addvault.name" label="Name" required></v-text-field>
          <v-text-field v-model="addvault.description" label="Description" required></v-text-field>
          <v-btn type="submit" @click="editvault(activevault.id)">edit</v-btn>
          <v-btn type="reset">Reset</v-btn>
        </form>
      </v-card>
    </v-dialog>
    <v-dialog v-model="vaultmodal" absolute scrollable fullscreen transition="scale-transition">
      <div class="vault">
        <div v-for="activevault in activevaults" :key="activevault.id" class="vaultcard">
          <h1>{{activevault.name}}</h1>
          <p>{{activevault.description}}</p>
          <div class="vks">
            <div v-for="vaultkeep in vaultkeeps" :key="vaultkeep.id" class="vk"></div>
          </div>
          <i @click="vaultmodal = true" class="korvue fab fa-korvue">edit</i>
          <i class="delete far fa-trash-alt" @click="deletevault(vault.id)"></i>
        </div>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "vault",
  data() {
    return {
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
      this.$store.dispatch("editvault", this.addvault);
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
};
</script>