export enum EventType{
  getIncoming = 7,
  putIncoming = 8,
  getTransitive = 9,
  putTransitive = 10,
  getOutgoing = 11,
  putOutgoing = 12,
}

export enum EventTypeWord {
  'IncomingArrived',
  'OutgoingArrived',
  'IncomingDispatched',
  'OutgoingDispatched',
  'IncomingDefect',
  'OutgoingDefect',
  'TransitiveDefect',
  'Материал выгружен с транспорта поставщика',
  'Материал загружен в транспорт поставщика',
  'Материал взят с площадки',
  'Материал выгружен на площадку',
  'Материал выгружен с транспорта компании',
  'Материал загружен в транспорт компании'
}
