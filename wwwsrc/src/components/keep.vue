<template>
      <v-layout justify-center row wrap class="keep">
        <v-flex xs12 sm6 md3 v-for="keep in keeps" :key="keep._id">
          <v-card v-if="keep.isPrivate == false" raised color="#353438">
            <v-img :src="keep.img" height="200px">
              <v-layout fill-height>
                <v-flex xs12 align-end flexbox>
                  <span class="headline white--text">{{keep.name}}</span>
                </v-flex>
              </v-layout>
            </v-img>
            <v-card-text class="grey--text">{{keep.description}}</v-card-text>
            <v-card-actions justify-content->
              <v-spacer></v-spacer>
              <v-layout align-center justify-space-around row fill-height>
                <i class="far fa-eye">: {{keep.views}}</i>
                <i class="fab fa-kaggle">: {{keep.keeps}}</i>
                <i class="fas fa-share">: {{keep.shares}}</i>
              </v-layout>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
</template>

<script>
export default {
  name: "keep",
  mounted() {},
  computed: {
    keeps() {
      return this.$store.state.keeps;
    }
  }
};
</script>
<style>
</style>