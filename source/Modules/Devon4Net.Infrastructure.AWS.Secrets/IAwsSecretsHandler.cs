using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SecretsManager.Model;

namespace Devon4Net.Infrastructure.AWS.Secrets
{
    public interface IAwsSecretsHandler : IDisposable
    {
        Task<IReadOnlyList<SecretListEntry>> GetAllSecrets(CancellationToken cancellationToken);
        Task<T> GetSecretString<T>(string secretId);
        Task<byte[]> GetSecretBinary(string secretId);
        Task<GetSecretValueResponse> GetSecretValue(GetSecretValueRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}