export interface ApplicationUser {
  Id: number;
  UserName: string;
  FullName: string;
  RoleId: string;
  Email: string;
  Phone?: string;
  Position?: string;
  Password?: string;
  IsDeleted: boolean;
}

export enum ApplicationUserProperties {
  'UserName' = 'Логин',
  'FullName' = 'ФИО',
  'Role' = 'Роль',
  'Email' = 'Email',
}
