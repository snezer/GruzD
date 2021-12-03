export const LOCAL_STORAGE_KEY = 'FSSP.IO';
export const DATE_TIME_FORMAT_ORIGIN = 'yyyy-MM-ddTHH:mm:ss';
export const DATE_TIME_FORMAT_INPUT = 'YYYY-MM-DD';
export const FRACTION_PART_DECIMAL_NUMBERS_COUNT = 2;
export const ITEMS_PER_PAGE = 20;
export const PROJECT_NAME = 'ГрузДЪ';

export enum WorkspaceDisplay {
  LeftOnly = 1,
  RightOnly = 2,
  Both = 3,
}

export enum DisplayItems {
  Block = 1,
  Table = 2,
}

export enum FilterFieldType {
  String = 1,
  Number,
  Date,
  Enum,
}

export enum LayoutMode {
  Static = 1,
  Overlay,
}
