import axios, { AxiosResponse } from 'axios';
import { PagedItems } from './pagedItems';
import { PagedFilter, buildUrlParams } from './pagedFilter';

export class BaseService {
  private readonly _endpoint: string;

  constructor(endpoint: string) {
    this._endpoint = endpoint;
  }

  protected get apiEndpoint(): string {
    return `/api/v1/${this._endpoint}`;
  }
}

export class RestService<T> extends BaseService {
  public async getAll(filter?: PagedFilter): Promise<PagedItems<T>> {
    let fullUrl = `${this.apiEndpoint}`;

    if (filter) {
      const urlParams = buildUrlParams(filter);
      fullUrl = `${this.apiEndpoint}?${urlParams.toString()}`;
    }
    const response = await axios.get<any, AxiosResponse<PagedItems<T>>>(
      fullUrl
    );

    return response.data;
  }

  public async getById(modelId: string | number): Promise<T> {
    const response = await axios.get<any, AxiosResponse<T>>(
      `${this.apiEndpoint}/${modelId}`
    );

    return response.data;
  }

  public async create(model: T): Promise<number> {
    const response = await axios.post(this.apiEndpoint, model);

    return response.data;
  }

  public async update(modelId: string | number, model: T): Promise<any> {
    const response = await axios.put(`${this.apiEndpoint}/${modelId}`, model);

    return response.data;
  }

  public async updateExtended(
    model: T,
    modelId?: string | number
  ): Promise<any> {
    if (modelId) {
      return await this.update(modelId, model);
    }

    const response = await axios.put(`${this.apiEndpoint}`, model);
    return response.data;
  }

  public async delete(modelId: string | number): Promise<T> {
    const response = await axios.delete(`${this.apiEndpoint}/${modelId}`);

    return response.data;
  }
}
