<template>
      <v-layout align-start justify-start fill-height row wrap class="keep">
        <v-flex xs12 sm6 md4 flexbox v-for="(keep, index) in filteredkeeps" :key="keep._id">
          <!-- :height="!(index % 3) ? '20rem' : !(index % 2) ? '25rem' : '30rem'" -->
          <!-- @mouseover="displaycontent(keep.id)" @mouseout="hidecontent(keep.id)" -->
          <v-card tile v-if="keep.isPrivate == false" raised hover dark>
            <v-img :src="keep.img">
            <v-card-text class="headline dark--text">{{keep.name}}</v-card-text>
            <v-card-text class="dark--text">{{keep.description}}</v-card-text>
            </v-img>
              <v-layout class="dark--text my-2" justify-space-around>
                <i class="far fa-eye">: {{keep.views}}</i>
                <i class="fab fa-kaggle">: {{keep.keeps}}</i>
                <i class="fas fa-share">: {{keep.shares}}</i>
              </v-layout>
            <v-card-actions class="actions" >
              <v-layout justify-center>
              <v-btn dark><i class="far fa-eye"></i></v-btn>
              <v-btn dark><i class="fab fa-kaggle"></i></v-btn>
              <v-btn dark><i class="fas fa-share"></i></v-btn>
              </v-layout>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
      <!-- <div class="content">
        <div class="keep">
          <div class="keep-holder" v-bind:class="{one:index == 1}, {two:index == 2}" v-for="(keep, index) in filteredkeeps" :key="keep._id">
            <div class="keepimg">
              <div class="keepname">{{keep.name}}</div>
              <div class="keepdescription">{{keep.description}}</div>
              <div class="keepicons"></div>
            </div>
          </div>
        </div>
      </div> -->
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
    // displaycontent(id) {
    //   $("actions").css({ display: "flex" });
    //   this.hover = true;
    // },
    // hidecontent(id) {
    //   $("actions").css({ display: "none" });
    //   this.hover = false;
    // }
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
/* .content {
  grid-template-columns: repeat(6, 1rf);
  grid-auto-rows: minmax(10rem, auto);
  grid-gap: 0.5rem;
}

.content div {
  background: #333;
  padding: 30px;
  color: white;
}

.one {
  grid-column: 1/3;
  grid-row: 1/5;
  background-color: blue;
}

.two {
  grid-column: 3/7;
  grid-row: 1/3;
  background-color: ;
}

.three {
  grid-column: 3/5;
  grid-row: 3/5;
}

.four {
  grid-column: 5/7;
  grid-row: 3/7;
}

.five {
  grid-column: 1/5;
  grid-row: 5/7;
} */
</style>