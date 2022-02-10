using System;
using Amazon.CDK;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SecretsManager;

namespace Devon4Net.Infrastructure.AWS.CDK.Handlers
{
    public class AwsCdkSecretHandler: AwsCdkDefaultHandler
    {
        private string Region { get; }
        private string AccountId { get; }
        private AwsCdkKmsHandler AwsCdkKmsHandler { get; }
        private TagHandler TagHandler { get; }

        public AwsCdkSecretHandler(Construct scope, string applicationName, string environmentName, AwsCdkKmsHandler awsCdkKmsHandler, string region, string accountId) : base(scope, applicationName, environmentName)
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

        /// <summary>
        /// Creates a secret using an existing KMS key from the existing KMS id
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="encryptionKeyId"></param>
        /// <param name="charsToExclude"></param>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        public ISecret Create(string secretName, string encryptionKeyId, string charsToExclude = "^{}^{}\"@/;-+=&\\/", int passwordLength = 16, IKeyProps encryptionKeyProperties = null)
        {
            if (string.IsNullOrEmpty(encryptionKeyId))
            {
                throw new ArgumentException("The provided encryptionKeyId can not be null");
            }

            var key = AwsCdkKmsHandler.Locate(encryptionKeyId, null) ?? new Key(Scope, encryptionKeyId, encryptionKeyProperties);

            return Create(secretName, key, charsToExclude, passwordLength);
        }

        public ISecret Create(string secretName, IKey encryptionKey, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16  )
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
                EncryptionKey = encryptionKey
            });

            TagHandler.LogTag(secretName, result);

            return result;
        }

        public ISecret Create(string secretName, string charsToExclude = "^{}\"@/;-+=&\\/", int passwordLength = 16)
        {
            var result = new Secret(Scope, secretName, new SecretProps
            {
                SecretName = secretName,
                GenerateSecretString = new SecretStringGenerator
                {
                    ExcludeCharacters = charsToExclude,
                    PasswordLength = passwordLength
                }
            });

            TagHandler.LogTag(secretName, result);

            return result;
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
