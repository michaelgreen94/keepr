<template>
  <div class="keep">
    <div class="card" v-for="keep in filteredkeeps" :key="keep.id">
        <img :src="keep.img" alt="" class="cardimage">
        <div class="cardtext">
          <h1>{{keep.name}}</h1>
          <p>{{keep.description}}</p>
        </div>
        <div class="keepicons" >
          <i @click="sharekeep(keep)" class="share far fa-share-square">: {{keep.shares}}</i>
          <i @click="viewkeep(keep)" class="view far fa-eye">: {{keep.views}}</i>
          <i @click="addtovault(keep)" class="korvue fab fa-korvue">: {{keep.keeps}}</i>
          <i class="delete far fa-trash-alt" v-if="user.active && keep.isPrivate == true" @click="deletekeep(keep.id)"></i>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "keep",
  data() {
    return {
      // hover: false
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
      this.$store.dispatch("updatekeep", keep);
    },
    addtovault(keep) {
      this.$store.dispatch("addtovault", keep);
    }
  },
  computed: {
    keeps() {
      return this.$store.state.keeps;
    },
    user() {
      return this.$store.state.user;
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
  break-inside: avoid;
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