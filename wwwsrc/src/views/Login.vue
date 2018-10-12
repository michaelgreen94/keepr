<template>
  <div class="login">
    <v-toolbar class="white--text" color="#353438">
      <v-toolbar-title>Keepr</v-toolbar-title>
      <v-spacer></v-spacer>
        <v-text-field v-if="search" flat label="Search" v-model="searched" solo-inverted></v-text-field>
      <v-btn icon>
        <v-icon @click="search = !search">search</v-icon>
      </v-btn>
      <v-toolbar-items>
        <v-btn class="white--text" @click="showForms = !showForms" flat>Login/Register</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-layout row justify-center>
      <v-dialog v-model="showForms" scrollable width="45rem" transition="scale-transition">
        <v-card>
          <v-container>
            <v-card flat v-if="loginForm">
              <form ref="form">
                <v-text-field v-model="creds.email" @submit.prevent="loginUser" :rules="emailRules" label="E-mail"
                  required></v-text-field>
                <v-text-field v-model="creds.password" type="password" :rules="passwordRules" label="Password"
                  required></v-text-field>
                <v-btn :disabled="!valid" type="submit" @click="loginUser">Login</v-btn>
                <v-btn type="reset">Reset</v-btn>
                <v-layout justify-center>
                  <p v-if="loginForm">No account <strong @click="loginForm = !loginForm">click</strong> to Register</p>
                  <p v-else>Already have an account <strong @click="loginForm = !loginForm">click</strong> to Login</p>
                </v-layout>
              </form>
            </v-card>
            <v-card flat v-else>
              <form ref="form">
                <v-text-field v-model="newUser.username" :rules="usernameRules" label="Username" required></v-text-field>
                <v-text-field v-model="newUser.email" :rules="emailRules" label="E-mail" required></v-text-field>
                <v-text-field v-model="newUser.password" type="password" :rules="passwordRules" label="Password"
                  required></v-text-field>
                <v-btn :disabled="!valid" type="submit" @click="register">Register</v-btn>
                <v-btn type="reset">Reset</v-btn>
                <v-layout justify-center>
                  <p v-if="loginForm">No account <strong @click="loginForm = !loginForm">click</strong> to Register</p>
                  <p v-else>Already have an account <strong @click="loginForm = !loginForm">click</strong> to Login</p>
                </v-layout>
              </form>
            </v-card>
          </v-container>
        </v-card>
      </v-dialog>
    </v-layout>
    <v-container grid-list-md fluid>
      <keep v-bind:filteredkeeps="filteredkeeps"/>
    </v-container>
  </div>
</template>

<script>
import keep from "../components/keep.vue";
export default {
  name: "login",
  mounted() {
    //checks for valid session
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
  mounted() {
    this.getkeeps(), this.$store.dispatch("authenticate");
  },
  methods: {
    register() {
      this.$store.dispatch("register", this.newUser);
    },
    loginUser() {
      this.$store.dispatch("login", this.creds);
    },
    clear() {
      this.$refs.form.reset();
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