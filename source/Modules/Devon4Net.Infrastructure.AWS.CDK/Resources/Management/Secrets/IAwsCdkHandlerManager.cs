using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.KMS;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public interface IAwsCdkHandlerManager
    {
        ISecret AddSecret(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16, Duration rotationPeriod = null, IFunction rotationLambda = null);
        ISecret AddSecret(string secretName, string encryptionKeyId, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16);
        ISecret AddSecret(string secretName, IKey key, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16);
        CfnSecret CreateSecret(string secretName, string secretValue);
        string GetSecretValue(string secretId, string secretName, string secretManagerSuffix);
    }
}