<template>
  <div class="workspace">
    <div
      class="workspace-left-panel"
      v-if="!isDisplayedRightOnly"
      :class="isDisplayedLeftOnly ? 'fullscreen' : ''"
    >
      <div class="workspace-toolbar">
        <div class="workspace-toolbar__start">
          <Button
            class="p-button-sm"
            :icon="toolbarSwitchIconClass"
            @click="changeDisplayMode('left')"
          />
        </div>

        <div class="workspace-toolbar__end">
          <slot
            name="change-view-toolbar"
            class="workspace-toolbar__end"
          ></slot>
        </div>
      </div>

      <div><slot name="toolbar"></slot></div>

      <slot name="items-list"></slot>
    </div>

    <div class="workspace-gutter" v-if="isDisplayedBoth" />

    <div
      class="workspace-right-panel"
      v-if="!isDisplayedLeftOnly"
      :class="isDisplayedRightOnly ? 'fullscreen' : ''"
    >
      <Button
        class="p-button-sm"
        :icon="toolbarSwitchIconClass"
        @click="changeDisplayMode('right')"
      />

      <div class="workspace-content">
        <slot name="item-preview"></slot>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { WorkspaceDisplay } from '@/config';

@Component
export default class Workspace extends Vue {
  // data
  mode: WorkspaceDisplay = WorkspaceDisplay.Both;

  // computed
  get isDisplayedRightOnly(): boolean {
    return this.mode === WorkspaceDisplay.RightOnly;
  }

  get isDisplayedLeftOnly(): boolean {
    return this.mode === WorkspaceDisplay.LeftOnly;
  }

  get isDisplayedBoth(): boolean {
    return this.mode === WorkspaceDisplay.Both;
  }

  get toolbarSwitchIconClass() {
    return [
      'pi',
      this.isDisplayedBoth ? 'pi-window-maximize' : 'pi-window-minimize',
    ].join(' ');
  }

  // methods
  changeDisplayMode(val: string): void {
    switch (val) {
      case 'left':
        this.mode = this.isDisplayedBoth
          ? WorkspaceDisplay.LeftOnly
          : WorkspaceDisplay.Both;
        break;

      case 'right':
        this.mode = this.isDisplayedBoth
          ? WorkspaceDisplay.RightOnly
          : WorkspaceDisplay.Both;
        break;

      case 'both':
        this.mode = WorkspaceDisplay.Both;
        break;

      case 'leftonly':
        this.mode = WorkspaceDisplay.LeftOnly;
        break;

      case 'rightonly':
        this.mode = WorkspaceDisplay.RightOnly;
        break;
    }
  }
}
</script>

<style lang="scss">
.workspace {
  display: flex;
  flex-direction: row;
  height: 100%;
}

.workspace-left-panel,
.workspace-right-panel {
  padding: 10px;
  border: 1px solid #ded2e6;
  background: #ffffff;
  border-radius: 3px;
  color: #496057;
}

.workspace-left-panel {
  min-width: 35%;
}

.fullscreen {
  width: 100%;
}

.workspace-gutter {
  width: 6px;
  background: #f8f9fa;
}

.workspace-toolbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;

  &__start {
    align-self: flex-start;
  }

  &__end {
    align-self: flex-end;

    & button {
      margin-top: 0;
      min-width: 33px;
      min-height: 30px;
    }
  }
}

.workspace-right-panel {
  display: flex;
  flex-direction: column;
  width: 100%;

  & > button {
    align-self: flex-end;
    min-width: 33px;
    min-height: 30px;
  }
}

.workspace-content {
  margin-top: 10px;
  height: 100%;
}
</style>
