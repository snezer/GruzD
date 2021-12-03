<template>
  <a class="nav-item-iconic" @click="$emit('click')">
    <slot name="content"></slot>
  </a>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class NavIconIconic extends Vue {}
</script>

<style lang="scss" scoped>
.nav-item-iconic {
  color: white;
  cursor: pointer;
  max-height: 58px;
  padding: 18px 15px;
  line-height: 1;
  position: relative;
  margin: 0;
  border: 0;
  display: flex;

  & > .pi {
    margin-right: 5px;
  }

  &:hover {
    background-color: #3945a1;
  }
}
</style>
