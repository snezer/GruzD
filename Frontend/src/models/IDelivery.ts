import { ISupplier } from "@/models/ISupplier";
import { IProduct } from "@/models/IProduct";

export interface IDelivery{
  id: number,
  date: Date,
  supplier: ISupplier,
  product: IProduct
}
