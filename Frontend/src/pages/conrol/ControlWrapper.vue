<template>
  <div>
    <Control />
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
import Control from "@/pages/conrol/Control.vue";
@Component({
  components: { Control }
})
export default class ControlWrapper extends Vue{

};
</script>

<style scoped>

</style>
