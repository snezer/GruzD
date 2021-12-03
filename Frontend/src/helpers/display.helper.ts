// Форматирование строки по маске. Элементы value должны быть описаны как #, например, ##-##-####
export const formatByMask = (value: string, mask: string) => {
  let i = 0;
  // eslint-disable-next-line
  return mask.replace(/#/g, (_) => value[i++]);
};
