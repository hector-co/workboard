import { api } from 'src/boot/axios';
import { Developer } from 'src/models';
import {
  filterToQueryString,
  QueryType,
  pageToQueryString,
  Result,
  orderByToQueryString,
} from 'src/services/helpers';
import config from 'src/common/configuration';

const ApiUrl = 'workboard/developers';
const BaseUrl = config.workboardWebApiUrl;

export default {
  async get(id: number): Promise<Result<Developer>> {
    const response = await api.get(`${ApiUrl}/${id}`, {
      baseURL: BaseUrl,
    });

    return {
      data: createDevelopers([response.data.data])[0],
      meta: response.data.meta,
    };
  },
  async list(query?: QueryType): Promise<Result<Array<Developer>>> {
    const response = await api.get(
      `${ApiUrl}?` +
        filterToQueryString(query?.filter, true) +
        orderByToQueryString(query?.orderBy, true) +
        pageToQueryString(query?.page),
      {
        baseURL: BaseUrl,
      }
    );

    return {
      data: createDevelopers(response.data.data),
      totalCount: response.data.totalCount,
      meta: response.data.meta,
    };
  },
};

function createDevelopers(data: Array<any>): Developer[] {
  if (!data) return [];

  return data.map((a) => Developer.create(a));
}
