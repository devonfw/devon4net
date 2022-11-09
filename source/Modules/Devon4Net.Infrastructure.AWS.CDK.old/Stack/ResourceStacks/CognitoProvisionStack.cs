using Devon4Net.Infrastructure.AWS.CDK.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Stack.ResourceStacks
{
    public partial class ProvisionStack
    {
        public void CreateCognito()
        {
            if (CdkOptions.Cognito == null || CdkOptions.Cognito?.UserPools?.Any() != true) return;

            var cognitoParameterStore = new CognitoParameterStoreEntity();
            cognitoParameterStore.UserPools = new List<UserPool>();

            foreach (var userPoolOption in CdkOptions.Cognito.UserPools)
            {
                if (string.IsNullOrWhiteSpace(userPoolOption.Id))
                {
                    throw new ArgumentException($"All Cognito UserPools must have a {nameof(userPoolOption.Id)}");
                }

                if (string.IsNullOrWhiteSpace(userPoolOption.UserPoolName))
                {
                    throw new ArgumentException($"All Cognito UserPools must have a {nameof(userPoolOption.UserPoolName)}");
                }

                var (userPool, userPoolClients) = AwsCdkHandler.AddCognitoUserPool(userPoolOption.Id, userPoolOption.UserPoolName, userPoolOption.UserPoolResourceServers, userPoolOption.UserPoolClients);
                StackResources.CognitoUserPools.Add(userPoolOption.Id, userPool);

                if (!string.IsNullOrWhiteSpace(CdkOptions.Cognito.ParameterStoreName))
                {
                    var userPoolClientsParameterStore = userPoolClients.Select(userPoolClient => new AppClient { Id = userPoolClient.UserPoolClientId, Name = userPoolClient.UserPoolClientName });

                    cognitoParameterStore.UserPools.Add(new UserPool { Id = userPool.UserPoolId, Name = userPoolOption.UserPoolName, AppClients = userPoolClientsParameterStore.ToList() });

                    var jsonCognitoParameterStore = JsonConvert.SerializeObject(cognitoParameterStore);
                    AwsCdkHandler.AddParameter(CdkOptions.Cognito.ParameterStoreName, CdkOptions.Cognito.ParameterStoreName, jsonCognitoParameterStore);
                }                
            }
        }
    }
}
