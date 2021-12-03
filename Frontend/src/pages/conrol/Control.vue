<template>
  <div class="control-wrapper">
    <div class="select-zone">
      <SelectButton v-model="selectedZone" :options="zones" option-label="Name" ></SelectButton>
    </div>
    <div class="charge-discharge-zone-wrapper">
      <div class="control-train-car">
        <Button icon="pi pi-arrow-up" class="p-button p-button-outlined" style="height: 100%" v-if="visibleControlTrainCar"></Button>
      </div>
      <div class="charge-discharge-zone">
        <div class="train-car-wrapper">
          <div class="train-car" v-if="zoneState.SupplyTransportNumber">
            <div>
              Номер вагона поставщика {{zoneState.SupplyTransportNumber}}
            </div>
          </div>
        </div>
        <div class="zone">
          <div class="control">
            <Button icon="pi pi-arrow-up" :disabled="!zoneState.SupplyTransportNumber"></Button>
            <Button icon="pi pi-arrow-down" :disabled="!zoneState.SupplyTransportNumber"></Button>
          </div>
          <div class="place">
            <div class="title">
              {{zoneState.ZoneName}}
            </div>
          </div>
          <div class="control">
            <Button icon="pi pi-arrow-up" :disabled="!zoneState.CompanyTransportNumber"></Button>
            <Button icon="pi pi-arrow-down" :disabled="!zoneState.CompanyTransportNumber"></Button>
          </div>
        </div>
        <div class="train-car-wrapper">
          <div class="train-car" v-show="zoneState.CompanyTransportNumber">
            <div>
               Номер вагона компании {{zoneState.CompanyTransportNumber}}
            </div>
          </div>
        </div>
      </div>
      <div class="control-train-car">
        <Button icon="pi pi-arrow-down" class="p-button p-button-outlined"  style="height: 100%" v-if="visibleControlTrainCar"></Button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { IZoneState, IZoneMini } from "@/models/IZoneState";
@Component({

})
export default class Control extends Vue{
  selectedZone: IZoneMini | null = null

  get zoneState(){
    return this.$store.state.zone.activeZoneState
  }

  get zones(){
    return this.$store.state.zone.zones
  }
  get visibleControlTrainCar(){
    return  this.zoneState.SupplyTransportNumber || this.zoneState.CompanyTransportNumber
  }
  @Watch('selectedZone')
  async getZoneState(){
    await this.$store.dispatch('zone/get_zone_state', this.selectedZone?.Id)
  }

  async created(){
    await this.$store.dispatch('zone/get_zones')
    this.selectedZone = this.zones[0]
  }

};
</script>

<style scoped>
.control-wrapper{
  height: 100%;
  display: grid;
  grid-template-rows: 1fr 11fr;
}
.select-zone{
  align-self: center;
  justify-self: center;
}
.charge-discharge-zone-wrapper{
  display: grid;
  grid-template-columns: 1fr 10fr 1fr;
}
.charge-discharge-zone{
  display: grid;
  grid-template-rows: repeat(3, 1fr);
  height: 80vh;
}
.train-car-wrapper{
  display: grid;
  grid-template-rows: 4fr 1fr;
  padding: 10px;
}

.train-car{
  border: 2px solid #ea6e73;
  height: 70%;
  width: 80%;
  border-radius: 10px;
  align-self: center;
  justify-self: center;
}

.zone {
  display: grid;
  grid-template-rows: 1fr 10fr 1fr;
  gap: 10px;
}
.place{
  border: 2px solid #ea6e73;
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
.discharge-zone .title{
  text-align: center;
  font-size: 1.5rem;
  font-weight: 700;
}
</style>
