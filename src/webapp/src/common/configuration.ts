export class Configuration {
  static get CONFIG(): { [propName: string]: string } {
    return {
      webAppUrl: '$VUE_APP_WEB_URL',
      workboardWebApiUrl: '$VUE_APP_WORKBOARD_WEB_API_URL',
    };
  }

  static value(name: string): string {
    if (!(name in this.CONFIG)) {
      return '';
    }

    const value = this.CONFIG[name] as string;

    if (!value) {
      return '';
    }

    if (value.startsWith('$VUE_APP_')) {
      const envName = value.substring(1);
      const envValue = this.getProcessValue(envName);
      if (envValue) {
        return envValue;
      }
      return '';
    } else {
      return value;
    }
  }

  private static getProcessValue(key: string): string | undefined {
    key = `$${key}`;
    switch (key) {
      case Configuration.CONFIG.webAppUrl:
        return process.env.VUE_APP_WEB_URL;
      case Configuration.CONFIG.workboardWebApiUrl:
        return process.env.VUE_APP_WORKBOARD_WEB_API_URL;
    }
    return '';
  }
}

const config = {
  webAppUrl: Configuration.value('webAppUrl'),
  workboardWebApiUrl: Configuration.value('workboardWebApiUrl'),
};

export default config;
