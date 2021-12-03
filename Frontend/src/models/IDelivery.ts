
export interface IDelivery{
  id: number,
  plannedDate: Date,
  factDate: Date,
  providerId: number,
  providerName: string,
  rawMaterialId: number,
  rawMaterialName: string
}
