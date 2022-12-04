import { merge } from 'lodash';
import { GenericError } from './exceptions';
import { Board, Column, Card } from '.';

export class BoardItem {
  id = 0;
  board: Board = new Board();
  column: Column | undefined = undefined;
  card: Card = new Card();
  order = 0;

  get columnId(): number | undefined {
    return this.column?.id;
  }

  static create(data: any): BoardItem {
    if (!data) throw new GenericError('data expected');

    const model = new BoardItem();

    merge(model, data, {
      board: Board.create(data.board),
      column: data.column ? Column.create(data.column) : undefined,
      card: Card.create(data.card),
    });

    return model;
  }
}
