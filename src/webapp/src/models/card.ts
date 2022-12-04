import { merge } from 'lodash';
import { GenericError } from './exceptions';
import { Developer } from './developer';

export class Card {
  id = 0;
  name = '';
  description = '';
  owners: Array<Developer> = [];
  priority = 0;
  estimatedPoints = 0;
  state = 0;

  static create(data: any): Card {
    if (!data) throw new GenericError('data expected');

    const model = new Card();

    merge(model, data, {
      owners:
        data.owners != undefined
          ? data.owners.map((o: any) => Developer.create(o))
          : [],
    });

    return model;
  }
}
