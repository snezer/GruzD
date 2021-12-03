import { ApplicationUser } from '@/models';
import { RestService } from './base.service';
import { PagedFilter, PropertyFilter } from './pagedFilter';

class UserService extends RestService<ApplicationUser> {
  constructor() {
    super('users');
  }

  async getUserByLogin(login: string) {
    const filter: PropertyFilter = {
      name: 'username',
      value: login,
    };

    const query: PagedFilter = {
      filters: [filter],
      page: 0,
      itemsPerPage: 1,
    };

    return await userService.getAll(query);
  }
}

export const userService = new UserService();
