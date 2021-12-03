import { IDelivery } from "@/models/IDelivery";
import {deliveryService} from '@/services/delivery.service'

interface DeliveryState{
  deliverys: Array<IDelivery> | null
}
const delivery  = {
  namespaced: true,
  state: {
    deliverys: null
  },
  actions: {
    async get_deliverys({commit}){
      const deliverys = await deliveryService.getAll();
      commit('SAVE_DELIVERYS', deliverys)
    }
  },
  mutations: {
    SAVE_DELIVERYS(state, deliverys: Array<IDelivery>){
      state.deliverys = deliverys
    }
  }
}

export default delivery
