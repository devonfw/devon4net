using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager.Handlers;
using Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager
{
    public class AwsSecretsConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private IAwsSecretsHandler AwsSecretsHandler { get;}

        public AwsSecretsConfigurationProvider(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            AwsSecretsHandler = new AwsSecretsHandler(awsCredentials,regionEndpoint);
        }
        public override void Load()
        {
            base.Load();
            Data = GetAwsSecretsData(default).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            ((AwsSecretsHandler)AwsSecretsHandler).Dispose();
        }

        private async Task<Dictionary<string, string>> GetAwsSecretsData(CancellationToken cancellationToken)
        {
            var secrets = await AwsSecretsHandler.GetAllSecrets(cancellationToken).ConfigureAwait(false);
            var result = new Dictionary<string, string>();
            foreach (var secret in secrets)
            {
                try
                {
                    var secretValue = await AwsSecretsHandler.GetSecretValue(new GetSecretValueRequest { SecretId = secret.ARN }, cancellationToken).ConfigureAwait(false);
                    var secretValueString = secretValue.SecretString;

                    if (secretValueString is null)
                        continue;

                    result.Add(secret.Name, secretValueString);
                }
                catch (ResourceNotFoundException ex)
                {
                    throw new AmazonSecretsManagerException($"Error retrieving secret value (Secret: {secret.Name} Arn: {secret.ARN})", ex);
                }
            }
            return result;
        }
    }
}
