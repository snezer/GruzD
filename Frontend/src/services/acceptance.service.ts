import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';

class AcceptanceService extends RestService<any> {
  constructor() {
    super('acceptance');
  }
  async getAcceptance(){
    return await acceptanceService.getAll()
  }

  async  getAcceptInfo(acceptId: number){
    const filter: PropertyFilter ={
      name: 'acceptId',
      value: acceptId.toString()
    };

  }
}

export const acceptanceService = new AcceptanceService();
