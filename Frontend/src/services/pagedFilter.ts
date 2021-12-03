import { nameof } from '@/helpers/form.helper';
import { ITEMS_PER_PAGE } from '@/config';
import { FilterType } from '@/models/enum/FilterType';

export interface PagedFilter {
  filters?: PropertyFilter[];
  page: number;
  itemsPerPage: number;
  sortBy?: string;
  sortDirection?: SortDirection;
}

export type SortDirection = 'ASC' | 'DESC';

export interface PropertyFilter {
  name: string;
  value: string;
  type?: FilterType;
}

export function buildUrlParams(filter: PagedFilter): string {
  if (!filter) {
    return '';
  }

  const urlParams = new URLSearchParams();
  urlParams.append(nameof<PagedFilter>('page'), filter.page.toString());
  urlParams.append(
    nameof<PagedFilter>('itemsPerPage'),
    filter.itemsPerPage.toString()
  );
  if (filter.sortBy) {
    urlParams.append(nameof<PagedFilter>('sortBy'), filter.sortBy);
    urlParams.append(
      nameof<PagedFilter>('sortDirection'),
      filter.sortDirection as string
    );
  }
  if (filter.filters) {
    filter.filters.forEach((item, index) => {
      urlParams.append(`filters[${index}].Name`, item.name);
      urlParams.append(`filters[${index}].Value`, item.value);
      if (item.type) {
        urlParams.append(`filters[${index}].Type`, item.type.toString());
      }
    });
  }

  return urlParams.toString();
}

export const DefaultPagedFilter: PagedFilter = {
  page: 0,
  itemsPerPage: ITEMS_PER_PAGE,
  sortBy: '1',
  sortDirection: 'ASC',
};
