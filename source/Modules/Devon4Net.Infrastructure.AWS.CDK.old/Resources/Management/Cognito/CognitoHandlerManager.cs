using Amazon.CDK.AWS.Cognito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management.Cognito
{
    public partial class AwsCdkHandlerManager : ICognitoHandlerManager
    {
        public (IUserPool userPool, List<UserPoolClient> userPoolClients) AddCognitoUserPool(string identification, string userPoolName, List<Options.Resources.UserPoolResourceServer> userPoolResourceServers, List<Options.Resources.UserPoolClient> userPoolClientOptions)
        {
            return HandlerResources.AwsCdkCognitoHandler.CreateUserPool(identification, userPoolName, userPoolResourceServers, userPoolClientOptions);
        }

        public IUserPool LocateCognitoUserPoolById(string identification, string userPoolId)
        {
            return HandlerResources.AwsCdkCognitoHandler.LocateFromId(identification, userPoolId);
        }

        public IUserPool LocateUserPoolByArn(string identification, string arn)
        {
            return HandlerResources.AwsCdkCognitoHandler.LocateFromArn(identification, arn);
        }
    }
}
