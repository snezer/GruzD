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

export const zoneService = new ZoneService();
