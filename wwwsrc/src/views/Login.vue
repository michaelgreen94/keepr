<template>
    <div class="login">
        <v-toolbar fixed>
            <v-toolbar-side-icon></v-toolbar-side-icon>
            <v-toolbar-title>Keepr</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items class="hidden-sm-and-down">
                <v-btn @click="loginForm = !loginForm" flat>Login</v-btn>
                <v-btn @click="registerForm = !registerForm" flat>Register</v-btn>
            </v-toolbar-items>
        </v-toolbar>
        <v-layout>
            <v-form ref="form" v-if="loginForm">
                <v-text-field v-model="creds.email" @submit.prevent="loginUser" :rules="emailRules" label="E-mail" required></v-text-field>
                <v-text-field v-model="creds.password" :rules="passwordRules" :counter="20" label="Password" required></v-text-field>
                <v-btn :disabled="!valid" @click="loginUser">submit</v-btn>
                <v-btn @click="clear">clear</v-btn>
            </v-form>
            <v-form ref="form" v-if="registerForm">
                <v-text-field v-model="newUser.username" :rules="usernameRules" label="Username" required></v-text-field>
                <v-text-field v-model="newUser.email" :rules="emailRules" label="E-mail" required></v-text-field>
                <v-text-field v-model="newUser.password" :rules="passwordRules" :counter="20" label="Password" required></v-text-field>
                <v-btn :disabled="!valid" @click="register">Create Account</v-btn>
                <v-btn @click="clear">clear</v-btn>
            </v-form>
        </v-layout>
    </div>
</template>

<script>
export default {
  name: "login",
  mounted() {
    //checks for valid session
    this.$store.dispatch("authenticate");
  },
  data() {
    return {
      loginForm: false,
      registerForm: false,
      creds: {
        email: "",
        password: ""
      },
      newUser: {
        email: "",
        password: "",
        username: ""
      },
      valid: true,
      usernameRules: [
        v => !!v || "Username is Required",
        v => (v && v.length <= 25) || "Username must be less then 25 characters"
      ],
      emailRules: [
        v => !!v || "E-mail is Required",
        v => /.+@.+/.test(v) || "E-mail must be valid"
      ],
      passwordRules: [
        v => !!v || "Password is Required",
        v => (v && v.length <= 25) || "Password must be less then 25 characters"
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
      this.$refs.form.reset();
    }
  }
};
</script>