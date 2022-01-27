using System;
using Amazon.CDK.AWS.KMS;
using Constructs;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Kms
{
    public class AwsCdkKmsHandler : AwsCdkBaseHandler, IAwsCdkKmsHandler
    {
        public AwsCdkKmsHandler(Construct scope, string applicationName, string environmentName, string region) : base(scope, applicationName, environmentName, region)
        {
        }

        public IKey Create(string id, IKeyProps keyProps = null)
        {

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("The provided identifier for the KMS key can not be null or empty");
            }

            return new Key(Scope, id, keyProps);
        }

        public IKey Locate(string id, string keyArn)
        {
            return Key.FromKeyArn(Scope, id, keyArn);
        }
    }
}
