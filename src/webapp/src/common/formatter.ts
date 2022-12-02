import moment from 'moment';

const formatter = {
  formatDate(date: string | Array<string>): string {
    if (!date) return '';
    let dateStr = '';
    if (date instanceof Array) dateStr = date.join(', ');
    else dateStr = date;
    const dates = dateStr.split(',');
    const result = dates
      .map((d) => {
        if (!moment(d).isValid()) return d;
        return moment(d).format('L');
      })
      .join(', ');
    return result;
  },
};

export { formatter };
