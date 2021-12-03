import { IAcceptance } from "@/models/IAcceptance";

interface AcceptanceState {
  acceptances: Array<IAcceptance>
}
const initialState: AcceptanceState = {
  acceptances: [
    {
      id: '1',
      date: new Date(),
      supplier: {
        id: '1',
        name: 'ООО Гречапром'
      },
      product: {
        name: 'Греча',
      },
      status: '80%',
      defective: '30%',
      dischargeZone: 'Б',
      numberTrainCar: '1',
      photos: [
        'https://i.ytimg.com/vi/uvkTKQzEbBw/maxresdefault.jpg',
        'https://i.ytimg.com/vi/uvkTKQzEbBw/maxresdefault.jpg',
        'https://i.ytimg.com/vi/uvkTKQzEbBw/maxresdefault.jpg'
      ]
    },
    {
      id: '2',
      date: new Date(),
      supplier: {
        id: '2',
        name: 'ООО Спичипром'
      },
      dischargeZone: 'B',
      status: 'В очереди',
      numberTrainCar: '2',
      product: {
        name: 'Спички',
      },
      result: 'Отклонен',
      photos: [
        'https://s0.rbk.ru/v6_top_pics/ampresize/media/img/9/44/755283020588449.jpg',
        'https://cement96.ru/media/uploads/%D1%89%D0%B5%D0%B1%D0%B5%D0%BD%D1%8C_%D0%BE%D0%BF%D1%82%D0%BE%D0%BC_%D0%B6%D0%B4_%D1%82%D1%80%D0%B0%D0%BD%D1%81%D0%BF%D0%BE%D1%80%D1%82%D0%BE%D0%BC.jpg',
        'https://www.business-vector.info/wp-content/uploads/2020/05/rzhd.jpg'
      ]
    },
  ]
}
const acceptance = {
  namespaced: true,
  state: initialState,
  actions:{

  },
  mutations: {

  }
};

export default acceptance;
