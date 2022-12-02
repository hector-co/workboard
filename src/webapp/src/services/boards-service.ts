import { api } from 'src/boot/axios';
import { Board } from 'src/models';
import {
  filterToQueryString,
  QueryType,
  pageToQueryString,
  Result,
  orderByToQueryString,
} from 'src/services/helpers';
import config from 'src/common/configuration';

const ApiUrl = 'workboard/boards';
const BaseUrl = config.workboardWebApiUrl;

export default {
  async get(id: number): Promise<Result<Board>> {
    const response = await api.get(`${ApiUrl}/${id}`, {
      baseURL: BaseUrl,
    });

    return {
      data: createBoards([response.data.data])[0],
      totalCount: response.data.totalCount,
      meta: response.data.meta,
    };
  },
  async list(query?: QueryType): Promise<Result<Array<Board>>> {
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
      data: createBoards(response.data.data),
      totalCount: response.data.totalCount,
      meta: response.data.meta,
    };
  },
  async register(model: any): Promise<void> {
    await api.post(`${ApiUrl}`, JSON.stringify(model), {
      baseURL: BaseUrl,
    });
  },
};

function createBoards(data: Array<any>): Board[] {
  if (!data) return [];

  return data.map((a) => Board.create(a));
}
