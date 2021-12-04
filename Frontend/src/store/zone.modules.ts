import { IZoneState, IZoneMini } from "@/models/IZoneState";
import { zoneService } from "@/services/zone.service";
import { zoneStateService } from "@/services/zoneState.service";
import { EventType } from "@/models/enum/EventType";
import { eventService } from "@/services/event.service";

interface ZoneState {
  zones: Array<IZoneMini>,
  activeZoneState: IZoneState | null
  eventListActiveZoneState: any
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
    },
    eventListActiveZoneState: null
  } as ZoneState,
  getters: {},
  actions: {
    async get_zones({commit}){
        const zones = await zoneService.getZones()
      commit('SAVE_ZONES', zones)
    },
    async get_zone_state({commit, dispatch}, zoneId: number){
      const zoneState = await zoneStateService.getZoneState(zoneId)
      await commit('SAVE_ZONE_STATE', zoneState)
      await dispatch('get_event_list_active_zone_state')
    },
    async move_weight({commit}, {from, to, weight, zoneId}){
      const moveWeight = await eventService.moveWeight(from, to, weight, zoneId)
    },
    async get_event_list_active_zone_state({commit, state}){
      const listEvent = await eventService.getEventListsZoneState(state.activeZoneState.ZoneId)
      commit('SAVE_LIST_EVENT_ACTIVE_ZONE_STATE', listEvent)
    }

  },
  mutations: {
    SAVE_ZONES(state, zones:Array<IZoneMini>){
      state.zones = zones
    },
    SAVE_ZONE_STATE(state, zoneState: IZoneState){
      state.activeZoneState = zoneState
    },
    SAVE_LIST_EVENT_ACTIVE_ZONE_STATE(state, listEvent){
      state.eventListActiveZoneState = listEvent
    }
  }
}

export default zone
