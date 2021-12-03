export interface IZoneMini{
  Id: number,
  Name: string,
  CurrentWeight: number,
}

export interface IZoneState {
  ZoneId: number,
  ZoneName: string,
  SupplyTransportNumber: string,
  CompanyTransportNumber: string,
  SupplyTransportWeight: number,
  CompanyTransportWeight: number,
  ZoneWeight: number,
  TransitWeight: number,
}
