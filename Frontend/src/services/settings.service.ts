import { Settings } from '../models';
import { RestService } from './base.service';

class SettingsService extends RestService<Settings> {
  constructor() {
    super('settings');
  }
}

export const settingsService = new SettingsService();
