using Amazon.CDK;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SecretsManager;
using Constructs;
using Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Kms;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Secrets
{
    public class AwsCdkSecretHandler : AwsCdkBaseHandler, IAwsCdkSecretHandler
    {
        private string AccountId { get; }
        private AwsCdkKmsHandler AwsCdkKmsHandler { get; }
        private TagHandler TagHandler { get; }

        public AwsCdkSecretHandler(Construct scope, string applicationName, string environmentName, AwsCdkKmsHandler awsCdkKmsHandler, string region, string accountId) : base(scope, applicationName, environmentName, region)
        {
            if (string.IsNullOrEmpty(region) || string.IsNullOrEmpty(accountId))
            {
                throw new ArgumentException("The accountId or region can not be null for the secrethandler constructor");
            }

            TagHandler = new TagHandler();
            AwsCdkKmsHandler = awsCdkKmsHandler;
            Region = region;
            AccountId = accountId;
        }

        public ISecret Create(string secretName, string encryptionKeyId, IKeyProps encryptionKeyProperties, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            return Create(secretName, string.IsNullOrEmpty(encryptionKeyId) ? null : new Key(Scope, encryptionKeyId, encryptionKeyProperties), charsToExclude, passwordLength);
        }

        /// <summary>
        /// Creates a secret using an existing KMS key from the existing KMS id
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="encryptionKeyId"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret Create(string secretName, string encryptionKeyId, string charsToExclude = "^{}^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            if (string.IsNullOrEmpty(encryptionKeyId))
            {
                throw new ArgumentException("The provided encryptionKeyId can not be null");
            }

            var key = AwsCdkKmsHandler.Locate(encryptionKeyId, null);

            if (key == null)
            {
                throw new ArgumentException("The provided encryptionKeyId does not belong to a valid encryption key");
            }

            return Create(secretName, key, charsToExclude, passwordLength);
        }

        public ISecret Create(string secretName, IKey encryptionKey, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            if (encryptionKey == null)
            {
                return Create(secretName, charsToExclude, passwordLength);
            }

            var result = new Secret(Scope, secretName, new SecretProps
            {
                SecretName = secretName,
                GenerateSecretString = new SecretStringGenerator
                {
                    ExcludeCharacters = charsToExclude,
                    PasswordLength = passwordLength
                },
                EncryptionKey = encryptionKey,
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            TagHandler.LogTag(secretName, result);

            return result;
        }

        public ISecret Create(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16, Duration rotationPeriod = null, IFunction rotationLambda = null)
        {
            var result = new Secret(Scope, secretName, new SecretProps
            {
                SecretName = secretName,
                GenerateSecretString = new SecretStringGenerator
                {
                    ExcludeCharacters = charsToExclude,
                    PasswordLength = passwordLength
                },
                RemovalPolicy = RemovalPolicy.DESTROY
            });

            if (rotationPeriod != null && rotationLambda != null)
            {
                result.AddRotationSchedule($"{secretName}-rotationSchedule", new RotationScheduleOptions
                {
                    AutomaticallyAfter = rotationPeriod,
                    RotationLambda = rotationLambda
                });
            }

            TagHandler.LogTag(secretName, result);

            return result;
        }

        public CfnSecret CreatePlainSecret(string secretName, string secretValue)
        {
            return new CfnSecret(Scope, secretName, new CfnSecretProps
            {
                Name = secretName,
                SecretString = secretValue
            });
        }

        public string GetSecretValue(string secretId, string secretName, string secretManagerSuffix)
        {
            if (string.IsNullOrEmpty(Region) || string.IsNullOrEmpty(AccountId))
            {
                throw new ArgumentException("The accountId or region can not be null");
            }

            ISecret secret;

            if (string.IsNullOrEmpty(secretManagerSuffix))
            {
                secret = Secret.FromSecretAttributes(Scope, secretId, new SecretAttributes
                {
                    SecretPartialArn = $"arn:aws:secretsmanager:{Region}:{AccountId}:secret:{secretName}",
                });
            }
            else
            {
                secret = Secret.FromSecretAttributes(Scope, secretId, new SecretAttributes
                {
                    SecretCompleteArn = $"arn:aws:secretsmanager:{Region}:{AccountId}:secret:{secretName}-{secretManagerSuffix}",

                });
            }

            return GetSecretValue(secret);
        }

        public string GetSecretValue(ISecret secret)
        {
            if (secret == null)
            {
                throw new ArgumentException("Can not retrieve a secret value from a null secret instance");
            }

            return secret.SecretValue.ToJSON().ToString();
        }

    }
}
