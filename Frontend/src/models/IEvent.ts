import { EventType } from "@/models/enum/EventType";

export interface IEvent {
  unloadingZoneId: number,
  occured: Date,
  auxData: string,
  classifiedType: EventType,
  weight: number
}
