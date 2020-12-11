import { BackendType } from '../app/config';

export const environment: {production: boolean, backendType: BackendType, restPathRoot: string, restServiceRoot: string} = {
  production: false,
  backendType: BackendType.REST,
  restPathRoot: 'http://de-mucdevondepl01:9091/mythaistar/',
  restServiceRoot: 'http://de-mucdevondepl01:9091/mythaistar/services/rest/',
};
