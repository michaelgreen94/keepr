<template>
  <div class="login">
    <v-toolbar class="white--text" color="#353438">
      <v-toolbar-title>Keepr</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-text-field v-if="search" flat label="Search" v-model="searched" solo-inverted></v-text-field>
      <v-btn icon @click="search = !search">
        <v-icon>search</v-icon>
      </v-btn>
      <v-toolbar-items>
        <v-btn class="white--text" @click="showForms = !showForms" flat>Login/Register</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-layout row justify-center>
      <v-dialog v-model="showForms" max-width="45rem" transition="scale-transition">
        <v-card dark flat v-if="loginForm">
          <v-toolbar class="white--text" color="#353438">
            <v-toolbar-title>Login</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn flat @click="showForms = !showForms"><i class="fas fa-times fa-2x vclose"></i></v-btn>
          </v-toolbar>
          <v-container>
            <form ref="form" @submit.prevent>
              <v-text-field v-model="creds.email" :rules="emailRules" label="E-mail" required></v-text-field>
              <v-text-field v-model="creds.password" type="password" :rules="passwordRules" label="Password" required></v-text-field>
              <v-btn @click="loginUser" type="submit">Login</v-btn>
              <v-btn @click="clear">Clear</v-btn>
              <v-layout justify-center>
                <p v-if="loginForm">No account <strong class="darktext" @click="loginForm = !loginForm">click</strong>
                  to Register</p>
                <p v-else>Already have an account <strong class="darktext" @click="loginForm = !loginForm">click</strong>
                  to Login</p>
              </v-layout>
            </form>
          </v-container>
        </v-card>
        <v-card dark flat v-else>
          <v-toolbar class="white--text" color="#353438">
            <v-toolbar-title>Register</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn flat @click="showForms = !showForms"><i class="fas fa-times fa-2x vclose"></i></v-btn>
          </v-toolbar>
          <v-container>
            <form @submit.prevent>
              <v-text-field v-model="newUser.username" :rules="usernameRules" label="Username" required></v-text-field>
              <v-text-field v-model="newUser.email" :rules="emailRules" label="E-mail" required></v-text-field>
              <v-text-field v-model="newUser.password" type="password" :rules="passwordRules" label="Password" required></v-text-field>
              <v-btn @click="register" type="submit">Register</v-btn>
              <v-btn @click="clear">Clear</v-btn>
              <v-layout justify-center>
                <p v-if="loginForm">No account <strong class="darktext" @click="loginForm = !loginForm">click</strong>
                  to Register</p>
                <p v-else>Already have an account <strong class="darktext" @click="loginForm = !loginForm">click</strong>
                  to Login</p>
              </v-layout>
            </form>
          </v-container>
        </v-card>
      </v-dialog>
    </v-layout>
    <keep v-bind:filteredkeeps="filteredkeeps" />
  </div>
</template>

<script>
import keep from "../components/keep.vue";
export default {
  name: "login",
  mounted() {
    //checks for valid session
    this.getkeeps();
    this.$store.dispatch("authenticate");
  },
  data() {
    return {
      loginForm: true,
      showForms: false,
      search: false,
      creds: {
        email: "",
        password: ""
      },
      newUser: {
        email: "",
        password: "",
        username: ""
      },
      searched: "",
      valid: true,
      usernameRules: [
        v => !!v || "Username is Required",
        v => (v && v.length <= 20) || "Username must be less then 20 characters"
      ],
      emailRules: [
        v => !!v || "E-mail is Required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ],
      passwordRules: [
        v => !!v || "Password is Required",
        v =>
          (v && v.length >= 5) || "Password must be greater then 5 characters"
      ]
    };
  },
  methods: {
    register() {
      this.$store.dispatch("register", this.newUser);
    },
    loginUser() {
      this.$store.dispatch("login", this.creds);
    },
    clear() {
      this.creds.email = "";
      this.creds.password = "";
      this.newUser.email = "";
      this.newUser.password = "";
      this.newUser.username = "";
    },
    getkeeps() {
      this.$store.dispatch("getkeeps");
    },
    wantedkeeps() {
      this.$store.dispatch("getsearchedkeeps", this.searched);
    }
  },
  computed: {
    filteredkeeps: function() {
      return this.$store.state.keeps.filter(keep => {
        return keep.name.match(this.searched);
      });
    }
  },
  components: {
    keep
  }
};
</script>

<style>
.keepcontainer {
  width: 100%;
  margin: 0 auto;
  font-size: 0;
  text-align: center;
}

.darktext {
  color: black;
}
</style>