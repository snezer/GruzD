<template>
  <div class="control-wrapper">
    <div class="select-zone">
      <SelectButton v-model="selectedZone" :options="zones" option-label="Name" ></SelectButton>
    </div>
    <div class="charge-discharge-zone-wrapper">
      <div class="charge-discharge-zone">
        <div class="train-car-wrapper">
          <div class="train-car" v-if="zoneState.SupplyTransportNumber">
            <div>
              Номер вагона поставщика {{zoneState.SupplyTransportNumber}}
            </div>
            <div style="margin-top: 25px">
              {{zoneState.SupplyTransportWeight}} тонн
            </div>
          </div>
        </div>
        <div class="zone">
          <div class="control">
<!--            кнопки управления площадки с вагоном поставщика-->
            <Button
              icon="pi pi-arrow-up"
              :disabled="!zoneState.SupplyTransportNumber"
              @click="handleMoveWeight(event.getTransitive, event.putIncoming)"
            />
            <Button
              icon="pi pi-arrow-down"
              :disabled="!zoneState.SupplyTransportNumber"
              @click="handleMoveWeight(event.getIncoming, event.putTransitive)"
            />
          </div>
          <div class="place">
            <div class="title">
              {{zoneState.ZoneName}}
            </div>
            <div style="margin-top: 150px;">
              {{zoneState.ZoneWeight}} тонн
            </div>
          </div>
          <div class="control">
            <Button icon="pi pi-arrow-up" :disabled="!zoneState.CompanyTransportNumber"
                    @click="handleMoveWeight(event.getOutgoing, event.putTransitive)"
            />
            <Button icon="pi pi-arrow-down" :disabled="!zoneState.CompanyTransportNumber"
                    @click="handleMoveWeight(event.getTransitive, event.putOutgoing)"
            />
          </div>
        </div>
        <div class="train-car-wrapper">
          <div class="train-car" v-show="zoneState.CompanyTransportNumber">
            <div>
               Номер вагона компании {{zoneState.CompanyTransportNumber}}
            </div>
            <div style="margin-top: 25px">
              {{zoneState.CompanyTransportWeight}} тонн
            </div>
          </div>
        </div>
      </div>
      <div class="control-train-car">
        <Button icon="pi pi-arrow-up" class="p-button" style="height: 650px" v-if="visibleControlTrainCar"
            @click="handleMoveWeight(event.getOutgoing, event.putIncoming)"
        />
        <Button icon="pi pi-arrow-down" class="p-button"  style="height: 650px" v-if="visibleControlTrainCar"
            @click="handleMoveWeight(event.getIncoming, event.putOutgoing)"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { IZoneState, IZoneMini } from "@/models/IZoneState";
import {EventType} from "@/models/enum/EventType";

@Component({

})
export default class Control extends Vue{
  selectedZone: IZoneMini | null = null
  idIntervalGetZoneState: any = null
  event = EventType

  get zoneState(){
    return this.$store.state.zone.activeZoneState
  }

  get zones(){
    return this.$store.state.zone.zones
  }
  get visibleControlTrainCar(){
    return  this.zoneState.SupplyTransportNumber || this.zoneState.CompanyTransportNumber
  }
  async getZoneState(){
    await this.$store.dispatch('zone/get_zone_state', this.selectedZone?.Id)
  }
  async handleMoveWeight(from, to){
    console.log(from)
    console.log(to)
    await this.$store.dispatch('zone/move_weight', {from: from, to: to, weight: this.zoneState.TransitWeight, zoneId: this.zoneState.ZoneId})
  }
  @Watch('selectedZone')
  async handleGetZoneState(){
    // clearInterval(this.idIntervalGetZoneState)
    // this.idIntervalGetZoneState = setInterval(await this.getZoneState, 1000)
    await this.getZoneState()
  }

  async created(){
    await this.$store.dispatch('zone/get_zones')
    this.selectedZone = this.zones[0]
    this.$router.afterEach((to, from)=>{
      clearInterval(this.idIntervalGetZoneState)
    })
  }

  destroy() {

    clearInterval(this.idIntervalGetZoneState)
  }

};
</script>

<style scoped>
.control-wrapper{
  height: 100%;
  display: grid;
  grid-template-rows: 1fr 11fr;
  color: white;
  text-align: center;
}
.select-zone{
  align-self: center;
  justify-self: center;
}
.charge-discharge-zone-wrapper{
  /*display: grid;*/
  /*grid-template-columns: 10fr 2fr;*/
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 25px;
}
.charge-discharge-zone{
  display: grid;
  grid-template-rows: repeat(3, 1fr);
  height: 80vh;
  margin-left: 100px;
}
.train-car-wrapper{
  display: grid;
  grid-template-rows: 4fr 1fr;
  padding: 10px;
}

.train-car{
  border: 5px solid #ea6e73;
  height: 70%;
  width: 350px;
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
  width: 500px;
  border: 3px solid #ea6e73;
  border-radius: 10px;
  justify-self: center;
}
.control{
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
  gap: 10px;
}
.control-train-car{
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
  gap: 10px
}
.discharge-zone .title{
  text-align: center;
  font-size: 1.5rem;
  font-weight: 700;
}
</style>
