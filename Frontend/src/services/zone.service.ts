import { IZoneMini } from "@/models/IZoneState";
import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';

class ZoneService extends RestService<IZoneMini> {
  constructor() {
    super('Zone');
  }
  async getZones(){
    return await zoneService.getAll()
  }

  async  getZoneState(zoneId: number){
    const filter: PropertyFilter ={
      name: 'zoneId',
      value: zoneId.toString()
    };

  }
}

export const zoneService = new ZoneService();
