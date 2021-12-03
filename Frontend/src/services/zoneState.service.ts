import { IZoneState } from "@/models/IZoneState";
import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';

class ZoneStateService extends RestService<IZoneState> {
  constructor() {
    super('Zone/state');
  }

  async  getZoneState(zoneId: number){
    const filter: PropertyFilter ={
      name: 'zoneId',
      value: String(zoneId)
    };
    const query: PagedFilter = {
      filters: [filter],
      page: 0,
      itemsPerPage: 1
    }
    return await zoneStateService.getById(`?zoneId=${zoneId}`)

  }
  // async getUserByLogin(login: string) {
  //   const filter: PropertyFilter = {
  //     name: 'username',
  //     value: login,
  //   };
  //
  //   const query: PagedFilter = {
  //     filters: [filter],
  //     page: 0,
  //     itemsPerPage: 1,
  //   };
  //
  //   return await userService.getAll(query);
  // }
}

export const zoneStateService = new ZoneStateService();
