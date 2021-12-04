import { IZoneMini } from "@/models/IZoneState";
import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';
import { IEvent } from "@/models/IEvent";
import { EventType } from "@/models/enum/EventType";
import { zoneStateService } from "@/services/zoneState.service";

class EventService extends RestService<IEvent> {
  constructor() {
    super('Events');
  }
  async moveWeight(from: EventType, to: EventType, weight: number, zoneId: number){
    console.log(from)
    console.log(to)
    setTimeout(async function(){
      await eventService.create({
        unloadingZoneId: zoneId,
        occured: new Date(),
        auxData: '',
        classifiedType: from,
        weight: weight,
      })
    }, 2000)
    setTimeout(async function(){
      await eventService.create({
        unloadingZoneId: zoneId,
        occured: new Date(),
        auxData: '',
        classifiedType: to,
        weight: weight
      })
    }, 4000)
   return true
  };
  async getEventListsZoneState(zoneId: number){
    return await eventService.getById(`last?zoneId=${zoneId}&number=40`)
  }
}

export const eventService = new EventService();
