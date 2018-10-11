<template>
      <v-layout align-start justify-start row wrap class="keep">
        <v-flex xs12 sm6 md4 v-for="keep in filteredkeeps" :key="keep._id">
          <v-card tile v-if="keep.isPrivate == false" raised color="#555356">
            <v-img :src="keep.img" height="200px"></v-img>
            <br>
              <v-layout class="dark--text mb-3" justify-space-around>
                <i class="far fa-eye">: {{keep.views}}</i>
                <i class="fab fa-kaggle">: {{keep.keeps}}</i>
                <i class="fas fa-share">: {{keep.shares}}</i>
              </v-layout>
            <v-divider></v-divider>
            <v-card-text v-if="user.active" class="headline dark--text">{{keep.name}}</v-card-text>
            <v-divider></v-divider>
            <v-card-text v-if="user.active" class="dark--text">{{keep.description}}</v-card-text>
            <v-divider></v-divider>
            <v-card-actions v-if="user.active" >
              <v-layout justify-center>
              <v-btn dark><i class="far fa-eye"></i></v-btn>
              <v-btn dark><i class="fab fa-kaggle"></i></v-btn>
              <v-btn dark><i class="fas fa-share"></i></v-btn>
              </v-layout>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
</template>

<script>
export default {
  name: "keep",
  props: ["filteredkeeps"],
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
</style>