using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Amazon.SecretsManager.Model;
using Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager.Interfaces;
using Devon4Net.Infrastructure.Common.Helpers;

namespace Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager.Handlers
{
    public class AwsSecretsHandler : IAwsSecretsHandler, IDisposable
    {
        private JsonHelper JsonHelper { get; }
        private IAmazonSecretsManager SecretsManager { get; }
        private SecretsManagerCache Cache { get; }

        public AwsSecretsHandler(AWSCredentials awsCredentials, RegionEndpoint regionEndpoint)
        {
            if (awsCredentials == null) return;
            JsonHelper = new JsonHelper();
            SecretsManager = GetAmazonSecretsManagerClient(awsCredentials, regionEndpoint);
            Cache = new SecretsManagerCache(SecretsManager);
        }

        private static AmazonSecretsManagerClient GetAmazonSecretsManagerClient(AWSCredentials awsCredentials, RegionEndpoint regionEndpoint)
        {
            if (awsCredentials != null && regionEndpoint != null)
            {
                return new AmazonSecretsManagerClient(awsCredentials, regionEndpoint);
            }

            return awsCredentials != null ? new AmazonSecretsManagerClient(awsCredentials) : new AmazonSecretsManagerClient();
        }

        public async Task<T> GetSecretString<T>(string secretId)
        {
            var sec = await Cache.GetSecretString(secretId).ConfigureAwait(false);
            return JsonHelper.Deserialize<T>(sec);
        }

        public Task<byte[]> GetSecretBinary(string secretId)
        {
            return Cache.GetSecretBinary(secretId);
        }

        public Task<GetSecretValueResponse> GetSecretValue(GetSecretValueRequest request, CancellationToken cancellationToken = default)
        {
            return SecretsManager.GetSecretValueAsync(request, cancellationToken);
        }

        public async Task<IReadOnlyList<SecretListEntry>> GetAllSecrets(CancellationToken cancellationToken)
        {
            var result = new List<SecretListEntry>();
            var query = default(ListSecretsResponse);

            do
            {
                var nextToken = query?.NextToken;
                var request = new ListSecretsRequest() { NextToken = nextToken };

                query = await SecretsManager.ListSecretsAsync(request, cancellationToken).ConfigureAwait(false);
                result.AddRange(query.SecretList);
            } while (query.NextToken != null);

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            SecretsManager.Dispose();
            Cache.Dispose();
        }
    }
}
