import {
  accountService,
} from '@/services';
import { Settings, ApplicationUser } from '@/models';
import { Module, VuexModule, MutationAction } from 'vuex-module-decorators';

/**
 * Модуль будет содержать в себе различные базовые справочники, которые по идее подгружаются один раз,
 * а затем используются без необходимости постоянных обновлений при подгрузке компонент
 */
@Module({ namespaced: true })
export default class DictionaryModule extends VuexModule {
  _settings: Settings[] = [];
  _currentUser: ApplicationUser | null = null;

  get currentUser() {
    return this._currentUser;
  }

  @MutationAction({ mutate: ['_currentUser'] })
  async LOAD_CURRENTUSER() {
    const result = accountService.user;
    return { _currentUser: result };
  }


}
