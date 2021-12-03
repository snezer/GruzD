export function nameof<T>(name: keyof T): string {
  return name as string;
}

export function isStringEmpty(value: string): boolean {
  return value === undefined || value === null || value === '';
}

export function isArrayEmpty(items: any[]): boolean {
  return items === undefined || items === null || items.length === 0;
}

export function now(): Date {
  const value = new Date();

  return new Date(
    Date.UTC(value.getFullYear(), value.getMonth(), value.getDate())
  );
}

export function addDays(date: Date, days: number) {
  const result = new Date(date);
  result.setDate(result.getDate() + days);
  return result;
}
