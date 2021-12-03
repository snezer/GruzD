import { IZoneState } from "@/models/IZoneState";
import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';

class ZoneStateService extends RestService<IZoneState> {
  constructor() {
    super('Zone/state');
  }

  async  getZoneState(zoneId: number){
    return await zoneStateService.getById(`?zoneId=${zoneId}`)
  }
}

export const zoneStateService = new ZoneStateService();
