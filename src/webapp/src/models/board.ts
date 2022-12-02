import { GenericError } from './exceptions';
import { merge } from 'lodash';
import { Column } from '.';

export class Board {
  id = 0;
  name = '';
  columns: Column[] = [];

  static create(data: any): Board {
    if (!data) throw new GenericError('data expected');

    const model = new Board();

    merge(model, data, {
      columns: data.columns
        ? data.columns.map((c: any) => Column.create(c))
        : [],
    });

    return model;
  }
}
