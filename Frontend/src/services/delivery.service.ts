import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';
import { IDelivery } from "@/models/IDelivery";

class DeliveryService extends RestService<IDelivery> {
  constructor() {
    super('Supplies');
  }
  async getDeliverys(){
    return await deliveryService.getAll()
  }
}

export const deliveryService = new DeliveryService();
