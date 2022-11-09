using Amazon.CDK;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SecretsManager;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Secrets
{
    public interface IAwsCdkSecretHandler
    {
        ISecret Create(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16, Duration rotationPeriod = null, IFunction rotationLambda = null);
        ISecret Create(string secretName, IKey encryptionKey, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16);
        ISecret Create(string secretName, string encryptionKeyId, string charsToExclude = "^{}^{}\"@/;-+=&\\/", int passwordLength = 16);
        ISecret Create(string secretName, string encryptionKeyId, IKeyProps encryptionKeyProperties, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16);
        CfnSecret CreatePlainSecret(string secretName, string secretValue);
        string GetSecretValue(ISecret secret);
        string GetSecretValue(string secretId, string secretName, string secretManagerSuffix);
    }
}