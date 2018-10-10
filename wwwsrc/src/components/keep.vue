<template>
      <v-layout align-start justify-start row wrap class="keep">
        <v-flex xs12 sm6 md3 v-for="keep in filteredkeeps" :key="keep._id">
          <v-card hover tile v-if="keep.isPrivate == false" raised color="#555356">
            <v-img :src="keep.img" height="200px"></v-img>
            <br>
            <v-card-actions>
              <v-layout class="dark--text" justify-space-around>
                <i class="far fa-eye">: {{keep.views}}</i>
                <i class="fab fa-kaggle">: {{keep.keeps}}</i>
                <i class="fas fa-share">: {{keep.shares}}</i>
              </v-layout>
            </v-card-actions>
            <v-card-text class="headline dark--text">{{keep.name}}</v-card-text>
            <v-card-text class="dark--text">{{keep.description}}</v-card-text>
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
    }
  }
};
</script>
<style>
</style>