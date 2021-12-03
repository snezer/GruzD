<template>
  <div class="layout-topbar">
    <button class="p-link layout-menu-button" @click="onMenuToggle">
      <span class="pi pi-bars"></span>
    </button>

    <div class="layout-topbar__items">
      <router-link to="/" :exact="true" class="brand-name">
        {{ projectName }}
      </router-link>

      <ul class="control-elements">
        <li>&nbsp;&nbsp;</li>
        <li>
          <nav-icon-iconic @click="openProfile">
            <template slot="content">
              <span class="pi pi-user"></span>
              <span>{{ username }}</span>
            </template>
          </nav-icon-iconic>
        </li>
        <li>
          <nav-icon-iconic @click="logout">
            <template slot="content">
              <span class="pi pi-sign-in"></span>
              <span>Выход</span>
            </template>
          </nav-icon-iconic>
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator';
import { Getter, Action } from 'vuex-class';
import { accountService } from '@/services';
import Dropdown from 'primevue/dropdown';
import { PROJECT_NAME } from '@/config';
import {ApplicationUser, Settings, SettingsCode } from '@/models';
import NavIconIconic from './NavIconIconic.vue';

@Component({
  components: {
    Dropdown,
    NavIconIconic,
  },
})
export default class AppTopbar extends Vue {
  // data
  username = '';
  scenarioId: number | null = null;
  scenarioChanged = false;

  // computed
  get isTest(): boolean {
    return (window as any).isTest;
  }

  get projectName(): string {
    return this.isTest ? `${PROJECT_NAME}-ТЕСТ` : PROJECT_NAME;
  }

  // vuex
  @Action('dictionary/LOAD_CURRENTUSER')
  LOAD_CURRENTUSER;

  @Action('dictionary/LOAD_SCENARIOS')
  LOAD_SCENARIOS;

  @Action('dictionary/LOAD_SETTINGS')
  LOAD_SETTINGS;

  @Getter('dictionary/currentUser')
  user: ApplicationUser;

  @Getter('dictionary/settings')
  settings: Settings[];

  // methods
  onMenuToggle(event: MouseEvent): void {
    this.$emit('menu-toggle', event);
  }

  async loadUser() {
    await this.LOAD_CURRENTUSER();
    this.username = accountService.currentUserName();
  }

  openProfile(): void {
    if (!this.user) {
      return;
    }

    this.$router.push({
      name: 'user.edit',
      params: { id: this.user.Id.toString() },
    });
  }

  logout(): void {
    this.$store.dispatch('account/logout');
  }

  // hooks
  async mounted() {
    await this.loadUser();
  }
}
</script>
