using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Management
{
    public partial class AwsCdkHandlerManager: IAwsCdkHandlerManager
    {

        /// <summary>
        /// Generates a secret with no KMS key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16, Duration rotationPeriod = null, IFunction rotationLambda = null)
        {
            return HandlerResources.AwsCdkSecretHandler.Create(secretName, charsToExclude, passwordLength, rotationPeriod, rotationLambda);
        }

        /// <summary>
        /// Creates a secret using an existing KMS key from the existing KMS id
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="encryptionKeyId"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, string encryptionKeyId, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return HandlerResources.AwsCdkSecretHandler.Create(secretName, encryptionKeyId, charsToExclude, passwordLength);
        }

        /// <summary>
        /// Creates a secret using an existing KMS key
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="key"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret AddSecret(string secretName, IKey key, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return HandlerResources.AwsCdkSecretHandler.Create(secretName, key, charsToExclude, passwordLength);
        }

        /// <summary>
        /// Creates a secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <returns></returns>
        public CfnSecret CreateSecret(string secretName, string secretValue)
        {
            return HandlerResources.AwsCdkSecretHandler.CreatePlainSecret(secretName, secretValue);
        }

        public string GetSecretValue(string secretId, string secretName, string secretManagerSuffix)
        {
            return HandlerResources.AwsCdkSecretHandler.GetSecretValue(secretId, secretName, secretManagerSuffix);
        }

    }
}
