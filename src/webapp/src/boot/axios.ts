import { boot } from 'quasar/wrappers';
import axios, { AxiosInstance } from 'axios';
import notifier from 'src/common/notifier';
import { GenericError, SecurityError } from 'src/models/exceptions';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance;
  }
}

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
  headers: {
    'content-type': 'application/json',
  },
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const response = error.response;
    if (!response) {
      if (error.constructor.name != 'Cancel')
        notifier.error('Error connecting to the server');
      throw error;
    }

    let message = '';
    const responseData = response.data;
    if (responseData.status == 400) {
      if (
        responseData.code == 'auth_error' ||
        responseData.code == 'invalid_user_or_password' ||
        responseData.code == 'not_confirmed_email' ||
        responseData.code == 'duplicated_user_name'
      ) {
        throw new SecurityError(responseData.code);
      }
      message =
        'Error while processing the request,<br>review the data and try again';
    } else if (responseData.status == 403) {
      message = 'Usuario no válido';
      window.location.href = '/';
    } else if (responseData.status == 404) message = 'Recurso no encontrado';
    else message = 'Error while processing the request';
    notifier.warning(message);
    throw new GenericError(message, error);
  }
);

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api

  app.config.globalProperties.$axios = axios;
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = api;
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
});

export { api };
