<template>
      <v-layout justify-center row wrap class="keep">
        <v-flex xs12 sm6 md4 v-for="keep in keeps" :key="keep._id">
          <v-card>
            <v-img :src="keep.img" height="200px">
              <v-layout fill-height>
                <v-flex xs12 align-end flexbox>
                  <span class="headline dark--text" v-text="keep.name"></span>
                </v-flex>
              </v-layout>
            </v-img>
            <v-card-text class="dark--text" v-text="keep.description"></v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn icon>
                <v-icon>favorite</v-icon>
              </v-btn>
              <v-btn icon>
                <v-icon>bookmark</v-icon>
              </v-btn>
              <v-btn icon>
                <v-icon>share</v-icon>
              </v-btn>
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