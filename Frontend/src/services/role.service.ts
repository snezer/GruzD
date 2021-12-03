import { RestService } from './base.service';

export interface IdentityRole {
  Id: string;
  Name: string;
  Claims: string[];
}

export enum IdentityRoleProperties {
  'Name' = 'Роль',
}

class RoleService extends RestService<IdentityRole> {
  constructor() {
    super('roles');
  }
}

export const roleService = new RoleService();
