export enum SettingsCode {
  TestSettins = 'TEST',
}

export const getAsSettingsCode = (val: string): SettingsCode =>
  SettingsCode[val as keyof typeof SettingsCode];
