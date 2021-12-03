using Amazon.CDK;
using Amazon.CDK.AWS.KMS;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkKmsHandler:  AwsCdkDefaultHandler
    {
        public AwsCdkKmsHandler(Construct scope, string applicationName, string environmentName) : base(scope, applicationName, environmentName)
        {
        }

        public IKey Create(string id, IKeyProps keyProps = null)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("The provided identifier for the KMS key can not be null or empty");
            }

            return new Key(Scope,id, keyProps);
        }

        public IKey Locate(string id, string keyArn)
        {
            return Key.FromKeyArn(Scope, id, keyArn);
        }
    }
}
