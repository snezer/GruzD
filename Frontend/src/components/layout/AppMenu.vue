<template>
  <div class="layout-menu-container">
    <AppSubmenu
      :items="model"
      class="layout-menu"
      :root="true"
      @menuitem-click="onMenuItemClick"
    />
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import AppSubmenu from './AppSubmenu.vue';

@Component({
  components: {
    AppSubmenu,
  },
})
export default class AppMenu extends Vue {
  // props
  @Prop({ type: Array, default: () => [] }) readonly model: any[];

  // methods
  mounted(){
    console.log("thisss")
  }
  onMenuItemClick(event: MouseEvent): void {
    this.$emit('menuitem-click', event);
  }
}
</script>
