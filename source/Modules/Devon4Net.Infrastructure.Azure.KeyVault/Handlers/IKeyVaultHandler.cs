using Azure.Security.KeyVault.Secrets;

namespace Devon4Net.Infrastructure.Azure.KeyVault.Handlers
{
    public interface IKeyVaultHandler
    {
        /// <summary>
        /// Get secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetSecret(string secretName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get all secrets
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IDictionary<string, string>> GetSecrets(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Create secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="secretValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<KeyVaultSecret> CreateSecret(string secretName, string secretValue, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Delete secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteSecret(string secretName, CancellationToken cancellationToken = default);
        
    }
}