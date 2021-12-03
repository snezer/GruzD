export interface PagedItems<T> {
  Total: number;
  Items: T[];
  ErrMess?: string;
}
