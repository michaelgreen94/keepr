<template>
  <!-- keeps -->
  <div class="keep">
    <div v-if="user" class="card" @click="viewkeep(keep)" v-for="keep in filteredkeeps" :key="keep.id">
      <img @click="openkeep = true" :src="keep.img" alt="" class="cardimage">
      <div class="cardtext">
        <h1>{{keep.name}}</h1>
        <p>{{keep.description}}</p>
      </div>
      <div class="keepicons">
        <i @click="sharekeep(keep)" class="share far fa-share-square">: {{keep.shares}}</i>
        <i class="view far fa-eye">: {{keep.views}}</i>
        <i @click="addtovault(keep)" class="korvue fab fa-korvue">: {{keep.keeps}}</i>
        <v-btn v-if="openkeep == true && user.active" @click="editkeep = !editkeep">edit</v-btn>
        <i class="delete far fa-trash-alt" v-if="user.active && keep.isPrivate == true" @click="deletekeep(keep.id)"></i>
      </div>
    </div>

    <!-- editkeep modal -->
    <v-layout row justify-center>
      <v-dialog v-model="editkeep" absolute max-width="45rem" transition="scale-transition">
        <v-card dark flat>
          <v-toolbar dark>
            <v-toolbar-title>Edit Keep</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn flat @click="editkeep = !editkeep"><i class="fas fa-times fa-2x vclose"></i></v-btn>
          </v-toolbar>
          <v-container>
            <form ref="form" @submit.prevent="updatekeep">
              <v-text-field v-model="create.name" label="Name" required></v-text-field>
              <v-text-field v-model="create.description" label="Description" required></v-text-field>
              <v-text-field v-model="create.img" label="Img-URL"></v-text-field>
              <div v-if="activekeeps.isPrivate == true">
                <v-btn v-if="create.isprivate == false" @click="create.isprivate = true">Public</v-btn>
                <v-btn v-else @click="create.isprivate = false">Private</v-btn>
              </div>
              <v-btn type="submit" @click="updatekeep(activekeeps)">Submit</v-btn>
              <v-btn type="reset">Reset</v-btn>
            </form>
          </v-container>
        </v-card>
      </v-dialog>
    </v-layout>

    <!-- viewkeep modal -->
    <v-layout row justify-center>
      <v-dialog v-model="openkeep" absolute max-width="45rem" transition="scale-transition">
        <v-toolbar dark>
          <v-toolbar-title>{{activekeeps.name}}</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn flat @click="openkeep = !openkeep"><i class="fas fa-times fa-2x vclose"></i></v-btn>
          <v-layout>
          <v-menu bottom left v-if="user.active">
            <v-btn color="primary" slot="activator">Add to Vault</v-btn>
            <v-list>
              <v-list-tile v-for="vault in vaults" :key="vault.id" @click="addtovault(activekeeps, vault.id)">
                <v-list-tile-title>{{vault.name}}</v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
          </v-layout>
        </v-toolbar>
        <v-card dark>
          <div class="viewcard">
            <img :src="activekeeps.img" alt="" class="viewcardimage">
            <div class="viewcardtext">
              <h3>{{activekeeps.description}}</h3>
            </div>
            <div class="viewkeepicons">
              <i @click="sharekeep(activekeeps)" class="share far fa-share-square">: {{activekeeps.shares}}</i>
              <i class="view far fa-eye">: {{activekeeps.views}}</i>
              <i @click="addtovault(activekeeps, vault.id)" class="korvue fab fa-korvue">: {{activekeeps.keeps}}</i>
              <v-btn v-if="user.active" @click="editkeep = !editkeep">edit</v-btn>
              <v-btn v-if="user.active && activekeeps.isPrivate == true" @click="deletekeep(activekeeps.id)"><i class="delete far fa-trash-alt"></i></v-btn>
            </div>
          </div>
        </v-card>
      </v-dialog>
    </v-layout>
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
      // saveid = keep.id;
      this.$store.dispatch("getsinglekeep", keep.id);
      this.$store.dispatch("updatekeep", keep);
    },
    addtovault(keep, vaultid) {
      keep.keeps++;
      let payload = { keepid: keep.id, vaultid: vaultid, keep };
      this.$store.dispatch("addtovault", payload);
    },
    updatekeep(activekeep) {
      // payload = {id: keepid, editedkeep = this.create}
      this.create.id = activekeep.id;
      this.$store.dispatch("updatekeep", this.create);
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
  /* column-width: 20rem; */
  column-count: 6;
  -moz-column-count: 6;
  -webkit-column-count: 6;
  column-gap: 0.5rem;
  -moz-column-gap: 0.5rem;
  -webkit-column-gap: 0.5rem;
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
  height: 4rem;
}

.cardimage {
  display: block;
  width: 100%;
  height: 100%;
}

.keepicons {
  width: 100%;
  height: 0;
  position: absolute;
  bottom: 0;
  /* z-index: 3; */
  overflow: hidden;
  transition: height 0.2s;
  background-color: #353438;
  color: white;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
}

.cardtext {
  width: fit-content;
  position: absolute;
  font-size: 12px;
  top: 0;
  left: 1rem;
  color: white;
  opacity: 0;
  text-shadow: 1px 1px 2px black;
}

.viewcard {
  max-width: 45rem;
  position: relative;
}

.viewcardimage {
  width: 100%;
  height: 30rem;
  object-fit: cover;
  object-position: center;
  padding-top: 0.5rem;
  padding-right: 0.5rem;
  padding-left: 0.5rem;
}

.viewcardtext {
  width: 100%;
  height: 5rem;
  display: flex;
  justify-content: center;
  align-items: center;
}

.viewkeepicons {
  width: 100%;
  height: 3rem;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  background-color: #212121;
}

@media (max-width: 1300px) {
  .keep {
    column-count: 5;
    -moz-column-count: 5;
    -webkit-column-count: 5;
  }
}

@media (max-width: 1000px) {
  .keep {
    column-count: 4;
    -moz-column-count: 4;
    -webkit-column-count: 4;
  }
}

@media (max-width: 800px) {
  .keep {
    column-count: 3;
    -moz-column-count: 3;
    -webkit-column-count: 3;
  }
}

.card:hover .cardtext {
  opacity: 1;
}

@media (max-width: 600px) {
  .keep {
    column-count: 1;
    -moz-column-count: 1;
    -webkit-column-count: 1;
  }

  .cardtext {
    opacity: 1;
    font-size: 1rem;
  }

  .card {
    width: 25rem;
  }
}

.delete:hover {
  color: black;
}

.share:hover {
  cursor: pointer;
}

.view:hover {
  cursor: pointer;
}

.korvue:hover {
  cursor: pointer;
}
</style>