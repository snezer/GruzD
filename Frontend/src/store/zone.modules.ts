import { IZoneState, IZoneMini } from "@/models/IZoneState";
import { zoneService } from "@/services/zone.service";
import { zoneStateService } from "@/services/zoneState.service";
import { EventType } from "@/models/enum/EventType";
import { eventService } from "@/services/event.service";

interface ZoneState {
  zones: Array<IZoneMini>,
  activeZoneState: IZoneState | null
}
const zone = {
  namespaced: true,
  state: {
    zones: [],
    activeZoneState: {
      ZoneId: 0,
      ZoneName: '',
      SupplyTransportNumber: '',
      CompanyTransportNumber: '',
      SupplyTransportWeight: 0,
      CompanyTransportWeight: 0,
      ZoneWeight: 0,
      TransitWeight: 0,
    }
  } as ZoneState,
  getters: {},
  actions: {
    async get_zones({commit}){
        const zones = await zoneService.getZones()
      commit('SAVE_ZONES', zones)
    },
    async get_zone_state({commit}, zoneId: number){
      const zoneState = await zoneStateService.getZoneState(zoneId)
      console.log(zoneState)
      commit('SAVE_ZONE_STATE', zoneState)
    },
    async move_weight({commit}, {from, to, weight, zoneId}){
      const moveWeight = await eventService.moveWeight(from, to, weight, zoneId)
    }

  },
  mutations: {
    SAVE_ZONES(state, zones:Array<IZoneMini>){
      state.zones = zones
    },
    SAVE_ZONE_STATE(state, zoneState: IZoneState){
      state.activeZoneState = zoneState
    }
  }
}

export default zone
