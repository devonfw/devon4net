using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Devon4Net.Infrastructure.AWS.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AWS.Cognito.Handlers
{
    public class CognitouserHandler
    {
        private JsonHelper JsonHelper { get; }
        private AmazonCognitoIdentityProviderClient AmazonCognitoIdentityProviderClient { get; }

        public CognitouserHandler(AWSCredentials awsCredentials, RegionEndpoint regionEndpoint)
        {
            if (awsCredentials == null) return;
            JsonHelper = new JsonHelper();
            AmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(awsCredentials, regionEndpoint);
        }

        public CognitouserHandler()
        {
            JsonHelper = new JsonHelper();
            AmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient();
        }
    }
}
