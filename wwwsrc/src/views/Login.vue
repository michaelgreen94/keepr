<template>
    <div class="login">
        <v-toolbar fixed>
            <v-toolbar-side-icon></v-toolbar-side-icon>
            <v-toolbar-title>Keepr</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items class="hidden-sm-and-down">
                <v-btn @click="showForms = !showForms" flat>Login/Register</v-btn>
            </v-toolbar-items>
        </v-toolbar>
        <v-layout row justify-center>
            <v-dialog v-model="showForms" scrollable width="45rem" transition="scale-transition">
                <v-card>
                    <v-container grid-list-md>
                        <v-layout wrap>
                            <v-flex xs12 sm6 md12>
                                <v-form ref="form" v-if="loginForm">
                                    <v-btn @click="showForms = false" flat>X</v-btn>
                                    <v-text-field v-model="creds.email" @submit.prevent="loginUser" :rules="emailRules" label="E-mail" required></v-text-field>
                                    <v-text-field v-model="creds.password" :rules="passwordRules" :counter="20" label="Password" required></v-text-field>
                                    <v-btn :disabled="!valid" @click="loginUser">Login</v-btn>
                                    <v-btn @click="clear">clear</v-btn>
                                </v-form>
                                <v-form ref="form" v-else>
                                    <v-btn @click="showForms = false" flat>X</v-btn>
                                    <v-text-field v-model="newUser.username" :rules="usernameRules" label="Username" required></v-text-field>
                                    <v-text-field v-model="newUser.email" :rules="emailRules" label="E-mail" required></v-text-field>
                                    <v-text-field v-model="newUser.password" :rules="passwordRules" :counter="20" label="Password" required></v-text-field>
                                    <v-btn :disabled="!valid" @click="register">Register</v-btn>
                                    <v-btn @click="clear">clear</v-btn>
                                </v-form>
                                <div @click="loginForm = !loginForm">
                                    <p v-if="loginForm">No account Click to Register</p>
                                    <p v-else>Already have an account click to Login</p>
                                </div>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card>
            </v-dialog>
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
      showForms: false,
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