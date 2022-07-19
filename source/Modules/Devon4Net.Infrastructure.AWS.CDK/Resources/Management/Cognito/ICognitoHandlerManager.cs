using Amazon.CDK.AWS.Cognito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface ICognitoHandlerManager
    {
        (IUserPool userPool, List<UserPoolClient> userPoolClients) AddCognitoUserPool(string identification, string userPoolName, List<Options.Resources.UserPoolResourceServer> userPoolResourceServers, List<Options.Resources.UserPoolClient> userPoolClientOptions);
        IUserPool LocateCognitoUserPoolById(string identification, string userPoolId);
        IUserPool LocateUserPoolByArn(string identification, string arn);
    }
}
