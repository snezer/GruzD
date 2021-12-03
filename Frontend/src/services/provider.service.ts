
import { RestService } from './base.service';
import { IProvider } from "@/models/IProvider";

class ProviderService extends RestService<IProvider> {
  constructor() {
    super('Providers');
  }
  async getProviders(){
    return await providerService.getAll()
  }
}

export const providerService = new ProviderService();
