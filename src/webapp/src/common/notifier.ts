import { Notify } from 'quasar';

export default {
  success(message: string) {
    Notify.create({
      type: 'positive',
      message: message,
      position: 'top-right',
      html: true,
      actions: [{ icon: 'close', color: 'white' }]
    });
  },
  info(message: string) {
    Notify.create({
      type: 'info',
      message: message,
      position: 'top-right',
      html: true,
      actions: [{ icon: 'close', color: 'white' }]
    });
  },
  warning(message: string) {
    Notify.create({
      type: 'warning',
      message: message,
      position: 'top-right',
      html: true,
      actions: [{ icon: 'close' }]
    });
  },
  error(message: string) {
    Notify.create({
      type: 'negative',
      message: message,
      position: 'top-right',
      html: true,
      actions: [{ icon: 'close', color: 'white' }]
    });
  }
}