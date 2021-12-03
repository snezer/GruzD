import { isArrayEmpty } from '@/helpers';
import { EnumValue } from '@/models';
import Vue from 'vue';
import moment from 'moment';

export function initVueFilters() {
  Vue.filter('enumDescription', function <
    T
  >(value: T, enumValues: EnumValue<T>[]) {
    if (isArrayEmpty(enumValues)) {
      return value;
    }

    const enumValue = enumValues.find((item) => item.Value === value);
    if (enumValue) {
      return enumValue.Description;
    }

    return value;
  });

  Vue.filter('formatDate', function (value?: Date | string, format?: string) {
    if (!value) {
      return '-';
    }

    return moment(value).format(format || 'DD.MM.YYYY');
  });

  Vue.filter(
    'formatDateTime',
    function (value?: Date | string, format?: string) {
      if (!value) {
        return '-';
      }

      return moment(value).format(format || 'DD.MM.YYYY hh:mm');
    }
  );
}
