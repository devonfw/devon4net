using Amazon.CDK.AWS.Cognito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Cognito
{
    public interface IAwsCdkCognitoHandler
    {
        (IUserPool userPool, List<UserPoolClient> userPoolClientsIds) CreateUserPool(string identification, string name, List<Options.Resources.UserPoolResourceServer> userPoolResourceServers, List<Options.Resources.UserPoolClient> userPoolClientOptions);
        IUserPool LocateFromId(string identification, string userPoolId);
        IUserPool LocateFromArn(string identification, string arn);
    }
}
