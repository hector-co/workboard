export type FilterType =
  | 'equals'
  | 'equalsCi'
  | 'notEquals'
  | 'notEqualsCi'
  | 'lessThan'
  | 'lessThanOrEquals'
  | 'greaterThan'
  | 'greaterThanOrEquals'
  | 'contains'
  | 'containsCi'
  | 'startsWith'
  | 'startsWithCi'
  | 'endsWith'
  | 'endsWithCi'
  | 'in'
  | 'inCi'
  | 'notIn'
  | 'notInCi';

export type ConditionType = 'and' | 'or';

const filterTypeToQuery = {
  equals: '==',
  equalsCi: '==*',
  notEquals: '!=',
  notEqualsCi: '==*',
  lessThan: '<',
  lessThanOrEquals: '<=',
  greaterThan: '>',
  greaterThanOrEquals: '>=',
  contains: '-=-',
  containsCi: '-=-*',
  startsWith: '=-',
  startsWithCi: '=-*',
  endsWith: '-=',
  endsWithCi: '-=*',
  in: '|=',
  inCi: '|=*',
  notIn: '!|=',
  notInCi: '!|=*',
};

const conditionTypeToQuery = {
  and: ';',
  or: '|',
};

export abstract class NodeBase {
  abstract toString(): string;
}

export class AndNode extends NodeBase {
  constructor(public left: NodeBase, public right: NodeBase) {
    super();
  }

  toString(): string {
    return `${this.left.toString()} ; ${this.right.toString()}`;
  }
}

export class OrNode extends NodeBase {
  constructor(public left: NodeBase, public right: NodeBase) {
    super();
  }

  toString(): string {
    return `${this.left.toString()} | ${this.right.toString()}`;
  }
}

export class Filter extends NodeBase {
  constructor(
    public field: string,
    public op: FilterType,
    public value?: string
  ) {
    super();
  }

  toString(): string {
    return `${this.field}${filterTypeToQuery[this.op]}${
      this.value == undefined ? 'null' : `'${this.value}'`
    }`;
  }
}

export class FilterGroup extends NodeBase {
  constructor(
    public nodes: NodeBase[],
    public condition: ConditionType = 'and'
  ) {
    super();
  }

  toString(): string {
    if (this.nodes.length == 0) return '';
    if (this.nodes.length == 1) return this.nodes[0].toString();
    return `(${this.nodes
      .map((n) => n.toString())
      .join(conditionTypeToQuery[this.condition])})`;
  }
}

export class CollectionFilter extends NodeBase {
  constructor(public field: string, public filter: NodeBase) {
    super();
  }

  toString(): string {
    return `${this.field}(${this.filter.toString()})`;
  }
}

export type SortDirection = 'asc' | 'desc';

export interface Result<TModel> {
  data: TModel;
  totalCount?: number;
  meta?: any;
}

export function filterToQueryString(
  filter?: NodeBase | string,
  addAmpersand = false
) {
  if (filter == undefined) return '';
  if (typeof filter == 'string')
    return 'filter=' + filter + (addAmpersand ? '&' : '');
  return 'filter=' + filter.toString() + (addAmpersand ? '&' : '');
}

export function orderByToQueryString(
  orderBy: string | { direction: SortDirection; property: string } | undefined,
  addAmpersand = false
): string {
  if (!orderBy) return '';
  if (typeof orderBy == 'string') {
    if (!orderBy.startsWith('orderBy'))
      return 'orderBy=' + orderBy + (addAmpersand ? '&' : '');
    else return orderBy + (addAmpersand ? '&' : '');
  }
  if (!orderBy.direction || !orderBy.property) return '';
  const dirStr = orderBy.direction == 'asc' ? '' : '-';
  return `orderBy=${dirStr}${orderBy.property}${addAmpersand ? '&' : ''}`;
}

export function pageToQueryString(
  page: string | { number: number; size: number } | undefined,
  addAmpersand = false
): string {
  if (!page) return '';
  if (typeof page == 'string') return page;
  let result = page.number ? `offset=${(page.number - 1) * page.size}` : '';
  result += page.size ? (result ? '&' : '') + `limit=${page.size}` : '';
  return result + (addAmpersand ? '&' : '');
}

export function objectToQueryString(obj: any): string {
  if (obj == undefined) return '';
  if (typeof obj == 'string') return obj;

  let result = '';
  for (const [key, value] of Object.entries(obj)) {
    result += `${key}=${value}&`;
  }

  return result.substring(0, result.length - 1);
}

export class QueryType {
  filter?: NodeBase | string;
  orderBy?: { direction: SortDirection; property: string };
  page?: { number: number; size: number };
}
