<template>
  <div class="control-wrapper">
    <div class="charge-discharge-zone">
      <div class="train-car-wrapper">
        <div class="train-car">

        </div>
        <div class="control">
          <Button icon="pi pi-arrow-left"></Button>
          <Button icon="pi pi-arrow-right"></Button>
        </div>
      </div>
      <div class="discharge-zone">

      </div>
      <div class="train-car-wrapper right">
        <div class="control">
          <Button icon="pi pi-arrow-left"></Button>
          <Button icon="pi pi-arrow-right"></Button>
        </div>
        <div class="train-car">

        </div>
      </div>
    </div>
    <div class="control-train-car">
      <Button icon="pi pi-arrow-left" class="p-button p-button-outlined" style="width: 100%"></Button>
      <Button icon="pi pi-arrow-right" class="p-button p-button-outlined" style="width: 100%"></Button>
    </div>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
import {IZone} from '@/models/IZone'
export default class Control extends Vue{
  zone: IZone | null = null;

};
</script>

<style scoped>
.control-wrapper{
  height: 100%;
  display: grid;
  grid-template-rows: 10fr 2fr;
}
.charge-discharge-zone{
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  height: 80vh;
}
.train-car-wrapper{
  display: grid;
  grid-template-columns: 4fr 1fr;
  padding: 10px;
}
.train-car-wrapper.right{
  grid-template-columns: 1fr 4fr;
}
.train-car{
  border: 2px solid #ea6e73;
  height: 70%;
  width: 80%;
  border-radius: 10px;
  align-self: center;
  justify-self: center;
}

.discharge-zone {
  border: 1px solid #ea6e73;
  height: 100%;
  width: 100%;
  border-radius: 10px;
}
.control{
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
}
.control-train-car{
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
}
</style>
