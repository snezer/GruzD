import { ISupplier } from "@/models/ISupplier";
import { IProduct } from "@/models/IProduct";

// тут даже точнее будет сказано что не разгрузка а вагон с разгрузки
export interface IAcceptance {
  id: string,
  date: Date,
  supplier: ISupplier,
  product: IProduct,
  status: string, // степень разгрузки вагона
  dischargeZone: string,
  numberTrainCar: string,
  defective?: string,
  result?: string,
  photos? : Array<string>
}
