<template>
  <Login v-if="!isLoggedIn" />
  <div :class="wrapperClass" @click="onWrapperClick" v-else>
    <AppTopbar @menu-toggle="onMenuToggle" />

    <transition name="layout-sidebar">
      <div
        class="layout-sidebar layout-sidebar-dark"
        v-show="isSidebarVisible"
        @click="onSidebarClick"
      >
        <AppMenu :model="sideMenu" @menuitem-click="onMenuItemClick" />
      </div>
    </transition>

    <div class="layout-main">
      <router-view />
    </div>

    <Toast position="top-right" />
    <ConfirmDialog />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import AppTopbar from '@/components/layout/AppTopbar.vue';
import AppMenu from '@/components/layout/AppMenu.vue';
import Login from '@/pages/security/Login.vue';
import { menu, LayoutMode } from '@/config';

@Component({
  components: {
    AppTopbar,
    AppMenu,
    Login,
  },
})
export default class App extends Vue {
  // data
  staticMenuHidden = false;
  overlayMenuActive = false;
  sideMenu = menu;
  layoutMode = LayoutMode.Overlay;
  menuClick = true;

  // computed
  get isLoggedIn(): boolean {
    // return this.$store.state.account.status.loggedIn;
    return true
  }

  get isSidebarVisible(): boolean {
    if (this.layoutMode === LayoutMode.Static) {
      return !this.staticMenuHidden;
    } else if (this.layoutMode === LayoutMode.Overlay) {
      return this.overlayMenuActive;
    } else {
      return true;
    }
  }

  get wrapperClass() {
    return [
      'layout-wrapper',
      {
        'layout-overlay': this.layoutMode === LayoutMode.Overlay,
        'layout-static': this.layoutMode === LayoutMode.Static,
        'layout-static-sidebar-hidden':
          this.layoutMode === LayoutMode.Static && this.staticMenuHidden,
        'layout-overlay-sidebar-active':
          this.layoutMode === LayoutMode.Overlay && this.overlayMenuActive,
      },
    ];
  }

  // methods


  onMenuToggle(event: MouseEvent): void {
    this.menuClick = true;

    if (this.layoutMode === LayoutMode.Static) {
      this.staticMenuHidden = !this.staticMenuHidden;
    } else if (this.layoutMode === LayoutMode.Overlay) {
      this.overlayMenuActive = !this.overlayMenuActive;
    }

    event.preventDefault();
  }

  onWrapperClick(): void {
    if (!this.menuClick) {
      this.overlayMenuActive = false;
    }

    this.menuClick = false;
  }

  onSidebarClick(): void {
    this.menuClick = true;
  }

  onMenuItemClick(event: any): void {
    if (event.item && !event.item.items) {
      this.overlayMenuActive = false;
    }
  }
}
</script>
