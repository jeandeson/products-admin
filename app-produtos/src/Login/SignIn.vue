<template>
  <div class="selection:bg-gray-500 selection:text-white">
    <div
      class="
        absolute
        h-full
        inset-0
        bg-gray-100
        flex
        justify-center
        items-center
      "
    >
      <div class="p-5 flex-1">
        <div
          class="w-80 bg-white rounded-3xl mx-auto overflow-hidden shadow-xl"
        >
          <div class="relative h-48 bg-gray-500 rounded-bl-4xl">
            <svg
              class="absolute bottom-0"
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 1440 320"
            >
              <path
                fill="#ffffff"
                fill-opacity="1"
                d="M0,64L48,80C96,96,192,128,288,128C384,128,480,96,576,85.3C672,75,768,85,864,122.7C960,160,1056,224,1152,245.3C1248,267,1344,245,1392,234.7L1440,224L1440,320L1392,320C1344,320,1248,320,1152,320C1056,320,960,320,864,320C768,320,672,320,576,320C480,320,384,320,288,320C192,320,96,320,48,320L0,320Z"
              ></path>
            </svg>
          </div>
          <div class="px-10 pt-4 pb-8 bg-white rounded-tr-4xl">
            <h1 class="text-2xl font-semibold text-gray-900">API Pessoas</h1>
            <form class="mt-12" @submit.prevent="handleLogin">
              <EmailInput
                v-model="signInForm.email"
                @invalid-input="validateField($event)"
              />

              <PasswordInput
                v-model="signInForm.password"
                @invalid-input="validateField($event)"
              />

              <input
                :class="{ 'pointer-events-none': !isValidInputs }"
                type="submit"
                value="Entrar"
                class="
                  mt-20
                  px-4
                  py-2
                  rounded
                  bg-gray-500
                  hover:bg-gray-400
                  text-white
                  font-semibold
                  text-center
                  block
                  w-full
                  focus:outline-none
                  focus:ring-gray-500 focus:ring-opacity-80
                  cursor-pointer
                "
              />
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SignIn from "../../domain/SignIn/signin.js";
import EmailInput from "../../components/FormControl/EmailInput.vue";
import PasswordInput from "../../components/FormControl/PasswordInput.vue";
import useLoginAuth from "../../composables/SignIn/UseLoginAuth";
import { reactive, computed } from "vue";
import { useRouter } from "vue-router";
export default {
  components: {
    EmailInput,
    PasswordInput,
  },

  setup() {
    const router = useRouter();
    const signInForm = reactive(new SignIn());
    const errors = reactive({
      email: "start",
      password: "start",
    });
    const { login } = useLoginAuth();
    const validateField = ({ field, value }) => (errors[field] = value);

    const isValidInputs = computed(() =>
      Object.values(errors).every((error) => !error)
    );

    const handleLogin = async () => {
      const isValidUser = await login(signInForm);
      isValidUser && router.push("/");
    };

    return { signInForm, errors, isValidInputs, validateField, handleLogin };
  },
};
</script>

<style lang="postcss" scoped>
</style>