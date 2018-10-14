<template>
<!-- keeps -->
  <div class="keep">
    <div class="card" @click="viewkeep(keep)" v-for="keep in filteredkeeps" :key="keep.id">
      <img @click="openkeep = true" :src="keep.img" alt="" class="cardimage">
      <div class="cardtext">
        <h1>{{keep.name}}</h1>
        <p>{{keep.description}}</p>
      </div>
      <div class="keepicons">
        <i @click="sharekeep(keep)" class="share far fa-share-square">: {{keep.shares}}</i>
        <i class="view far fa-eye">: {{keep.views}}</i>
        <!-- needs to be a v-menu with a list of vaults -->
        <i @click="addtovault(keep)" class="korvue fab fa-korvue">: {{keep.keeps}}</i>
        <v-btn v-if="openkeep == true" @click="editkeep = !editkeep">edit</v-btn>
        <i class="delete far fa-trash-alt" v-if="user.active && keep.isPrivate == true" @click="deletekeep(keep.id)"></i>
      </div>
    </div>
    <!-- editkeep modal -->
    <v-dialog v-model="editkeep" absolute scrollable width="45rem" transition="scale-transition">
      <v-card dark flat v-for="activekeep in activekeeps" :key="activekeep.id">
        <form ref="form">
          <v-text-field v-model="create.name" @submit.prevent="editkeep" label="Name" required></v-text-field>
          <v-text-field v-model="create.description" label="Description" required></v-text-field>
          <v-text-field v-model="create.img" label="Img-URL"></v-text-field>
          <v-btn v-if="activekeep.isprivate == false" @click="create.isprivate = true">Public</v-btn>
          <v-btn v-else @click="create.isprivate = false">Private</v-btn>
          <v-btn type="submit" @click="updatekeep(activekeep)">Submit</v-btn>
          <v-btn type="reset">Reset</v-btn>
        </form>
      </v-card>
    </v-dialog>
    <!-- viewkeep modal -->
    <v-dialog v-model="openkeep" absolute width="45" transition="scale-transition">
      <div class="keep">
        <div class="card" v-for="activekeep in activekeeps" :key="activekeep.id">
          <img :src="activekeep.img" alt="" class="cardimage">
          <div class="cardtext">
            <h1>{{activekeep.name}}</h1>
            <p>{{activekeep.description}}</p>
          </div>
          <div class="keepicons">
            <i @click="sharekeep(activekeep)" class="share far fa-share-square">: {{activekeep.shares}}</i>
            <i class="view far fa-eye">: {{activekeep.views}}</i>
            <!-- needs to be a v-menu with a list of vaults -->
            <v-menu>
              <v-btn slot="activator">Add to Vault</v-btn>
              <v-list>
                <v-list-tile @click="addtovault(activekeep.id, vault.id)" v-for="vault in vaults" :key="vault.id"></v-list-tile>
              </v-list>
            </v-menu>
            <!-- <i @click="addtovault(activekeep)" class="korvue fab fa-korvue">: {{activekeep.keeps}}</i> -->
            <v-btn @click="editkeep = !editkeep">edit</v-btn>
            <i class="delete far fa-trash-alt" v-if="user.active && activekeep.isPrivate == true" @click="deletekeep(activekeep.id)"></i>
          </div>
        </div>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "keep",
  data() {
    return {
      editkeep: false,
      openkeep: false,
      create: {
        name: "",
        description: "",
        img: "",
        isprivate: false
      }
    };
  },
  props: ["filteredkeeps"],
  methods: {
    deletekeep(id) {
      this.$store.dispatch("deletekeep", id);
    },
    sharekeep(keep) {
      keep.shares++;
      this.$store.dispatch("updatekeep", keep);
    },
    viewkeep(keep) {
      keep.views++;
      id = keep.id;
      this.$store.dispatch("setactivekeep", id);
      this.$store.dispatch("updatekeep", keep);
    },
    addtovault(keepid, vaultid) {
      payload = { keepid: keepid, vaultid: vaultid };
      this.$store.dispatch("addtovault", payload);
    },
    updatekeep(activekeep) {
      // payload = {id: keepid, editedkeep = this.create}
      edited = this.create;
      activekeep.name = edited.name;
      activekeep.description = edited.description;
      activekeep.img = edited.img;
      activekeep.isprivate = edited.isprivate;
      this.$store.dispatch("updatekeep", activekeep);
    }
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    user() {
      return this.$store.state.user;
    },
    activekeeps() {
      return this.$store.state.activekeep;
    },
    vaults() {
      return this.$store.state.vaults;
    }
  }
};
</script>
<style>
.keep {
  column-width: 20rem;
  column-count: auto;
  padding: 0.5rem 0.5rem;
}

.card {
  width: 20rem;
  margin-bottom: 0.5rem;
  position: relative;
  cursor: pointer;
  margin-left: auto;
  margin-right: auto;
}

.card:hover .keepicons {
  height: 5rem;
}

.cardimage {
  display: block;
  width: 100%;
  height: 100%;
}

.cardtext {
  width: fit-content;
  position: absolute;
  font-size: 12px;
  bottom: 0;
  left: 1rem;
  color: white;
  opacity: 0;
}

.card:hover .cardtext {
  opacity: 1;
}

.keepicons {
  width: 100%;
  height: 0;
  position: absolute;
  top: 100%;
  z-index: 3;
  overflow: hidden;
  transition: height 0.2s;
  background-color: #353438;
  color: white;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
}

.delete:hover {
  color: black;
}

.share:hover {
  color: black;
}

.view:hover {
  color: black;
}

.korvue:hover {
  color: black;
}
</style>