<template>
  <Card class="login-card">
    <template #header>
      <h2>{{ projectName }}</h2>
      <h3>Вход в систему</h3>
    </template>

    <template #content>
      <form name="form-login">
        <div class="p-field">
          <span class="p-float-label">
            <InputText
              id="login-field"
              type="text"
              class="login-field"
              v-model="username"
              :class="{ 'p-invalid': submitted && !username }"
            />
            <label for="login-field">Логин</label>
            <div v-show="submitted && !username" class="invalid-feedback">
              <InlineMessage>Поле обязательно для заполнения</InlineMessage>
            </div>
          </span>
        </div>

        <div class="p-field">
          <span class="p-float-label">
            <InputText
              id="password-field"
              type="password"
              class="login-field"
              v-model="password"
              :class="{ 'p-invalid': submitted && !password }"
            />
            <label for="password-field">Пароль</label>
            <div v-show="submitted && !password" class="invalid-feedback">
              <InlineMessage>Поле обязательно для заполнения</InlineMessage>
            </div>
          </span>
        </div>

        <Button
          label="Войти"
          @click="handleLogin"
          class="login-button p-button-sm"
        />

        <div class="p-field" v-if="message">
          <div class="feedback-message">
            <InlineMessage>{{ message }}</InlineMessage>
          </div>
        </div>
      </form>
    </template>
  </Card>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { accountService, userService } from '@/services';
import { tokenStorage } from '@/helpers';
import { PROJECT_NAME } from '@/config';

@Component
export default class Login extends Vue {
  username = '';
  password = '';
  submitted = false;
  message = '';

  // computed
  get isTest(): boolean {
    return (window as any).isTest;
  }

  get loggingIn() {
    return this.$store.state.account.status.loggingIn;
  }

  get projectName(): string {
    return this.isTest ? `${PROJECT_NAME}-ТЕСТ` : PROJECT_NAME;
  }

  handleLogin() {
    this.submitted = true;

    const { username, password } = this;
    if (username && password) {
      this.$store.dispatch('account/login', { username, password }).then(
        (result) => {
          this.setCurrentUser(username);
          tokenStorage.set(result);

          this.$router.push('/', () => {});
        },
        (error) => {
          this.message =
            (error.response && error.response.data) ||
            error.message ||
            this.parseError(error.data) ||
            error.toString();
        }
      );
    }
  }

  async setCurrentUser(username: string) {
    const user = await userService.getUserByLogin(username);
    if (user != null && user.Items.length > 0) {
      accountService.user = user.Items[0];
    }
  }

  parseError(data) {
    if (!data) {
      return undefined;
    }

    switch (data.error_description) {
      case 'invalid_username_or_password':
        return 'Неверный логин или пароль';
    }

    console.error(data);
    return 'Ошибка. Повторите операцию позже';
  }

  // hooks
  async mounted() {
    const token = tokenStorage.get();
    if (token) {
      this.$router.push('/');
    }
  }
}
</script>

<style lang="scss" scoped>
.login-card {
  max-width: 550px;
  padding: 40px 40px;
  margin: 0 auto 25px;
  margin-top: 50px;
  border-radius: 2px;
  box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);

  & h2,
  & h3 {
    text-align: center;
  }

  &h2 {
    margin-bottom: 20px;
  }
}

.login-field,
.login-button {
  width: 100%;
}

.p-field {
  margin-bottom: 20px;
}

.invalid-feedback {
  width: 100%;
  margin-top: 0.25rem;

  & .p-inline-message {
    width: 100%;
    justify-content: flex-start;
  }
}

.feedback-message {
  width: 100%;
  margin-top: 10px;

  & .p-inline-message {
    width: 100%;
  }
}
</style>
