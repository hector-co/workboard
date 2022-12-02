import { merge } from 'lodash';
import { GenericError } from './exceptions';

export class Column {
  id = 0;
  name = '';
  cardState = 0;
  order = 0;

  static create(data: any): Column {
    if (!data) throw new GenericError('data expected');

    const model = new Column();

    merge(model, data);

    return model;
  }
}
