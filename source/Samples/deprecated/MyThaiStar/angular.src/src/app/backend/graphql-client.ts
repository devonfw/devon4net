import { createNetworkInterface, ApolloClient } from 'apollo-client';

// by default, this client will send queries to `/graphql` (relative to the URL of your app)
const client: ApolloClient = new ApolloClient({
   networkInterface: createNetworkInterface({
        uri: '/graphql',
        opts: {
            credentials: 'same-origin',
        },
    }),
    dataIdFromObject: (obj: any) => `${obj.__typename}-${obj.id},`,
});

export function provideClient(): ApolloClient {
  return client;
}
