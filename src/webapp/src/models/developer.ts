import { merge } from 'lodash';
import { GenericError } from './exceptions';

export class Developer {
  id = 0;
  name = '';

  static create(data: any): Developer {
    if (!data) throw new GenericError('data expected');

    const model = new Developer();

    merge(model, data);

    return model;
  }
}
