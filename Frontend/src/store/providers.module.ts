import { IProvider } from "@/models/IProvider";
import { providerService } from "@/services/provider.service";

interface ProvidersState{
  providers: Array<IProvider> | null
}
const providers = {
  namespaced: true,
  state: {
    providers: null,
  } as ProvidersState,
  actions: {
    async get_providers({commit}){
      const providers = await providerService.getProviders()
      commit('SAVE_PROVIDERS', providers)
    }
  },
  mutations:{
    SAVE_PROVIDERS(state, providers: Array<IProvider>){
      state.providers = providers
    }
  }
}

export default providers
