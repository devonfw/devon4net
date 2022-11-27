using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Devon4Net.Infrastructure.Azure.KeyVault.Exception;

namespace Devon4Net.Infrastructure.Azure.KeyVault.Handlers
{
    /// <summary>
    /// Key Vault Handler
    /// </summary>
    public class KeyVaultHandler : IKeyVaultHandler
    {
        private readonly SecretClient _secretClient;

        public KeyVaultHandler(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }
        
        public KeyVaultHandler(Uri keyVaultUri, DefaultAzureCredential credentials)
        {
            _secretClient = new SecretClient(keyVaultUri, credentials);
        }

        /// <summary>
        /// Get secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AzureKeyVaultSecretNotFoundException"></exception>
        /// <exception cref="AzureKeyVaultSecretException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetSecret(string secretName, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckGetSecretsParameters(secretName);

                KeyVaultSecret keyValueSecret = await _secretClient.GetSecretAsync(secretName, cancellationToken: cancellationToken);

                if(keyValueSecret == null || string.IsNullOrWhiteSpace(keyValueSecret.Name) || string.IsNullOrWhiteSpace(keyValueSecret.Value))
                {
                    throw new AzureKeyVaultSecretNotFoundException($"The secret {secretName} could not be retrieved from the Key Vault");
                }

                return keyValueSecret.Value;
            }
            catch
            {
                throw new AzureKeyVaultSecretException("Error getting secret from Key Vault");
            }
        }
        
        /// <summary>
        /// Get all secrets
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AzureKeyVaultSecretException"></exception>
        public async Task<IDictionary<string, string>> GetSecrets(CancellationToken cancellationToken = default)
        {
            try
            {
                var secretDictionary = new Dictionary<string, string>();

                await foreach (var secretProperties in _secretClient.GetPropertiesOfSecretsAsync(cancellationToken))
                {
                    var secret = await _secretClient.GetSecretAsync(secretProperties.Name, cancellationToken: cancellationToken);
                    secretDictionary.Add(secretProperties.Name, secret.Value.Value);
                }

                return secretDictionary;
            }
            catch
            {
                throw new AzureKeyVaultSecretException("Error getting all secret from Key Vault");
            }
        }

        /// <summary>
        /// Create secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="secretValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AzureKeyVaultSecretException"></exception>
        public async Task<KeyVaultSecret> CreateSecret(string secretName, string secretValue, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckCreateSecretParameters(secretName, secretValue);

                KeyVaultSecret createdSecret = await _secretClient.SetSecretAsync(secretName, secretValue, cancellationToken).ConfigureAwait(false);

                if (createdSecret == null)
                {
                    throw new AzureKeyVaultSecretException($"The secret with key {secretName} could not be created from the Key Vault");
                }

                return createdSecret;
            }
            catch
            {
                throw new AzureKeyVaultSecretException($"Error creating secret with key: {secretName} in Key Vault");
            }
        }

        /// <summary>
        /// Delete secret
        /// </summary>
        /// <param name="secretName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AzureKeyVaultSecretException"></exception>
        public async Task<bool> DeleteSecret(string secretName, CancellationToken cancellationToken = default)
        {
            try
            {
                CheckDeleteSecretParameters(secretName);

                var deletedSecret = await _secretClient.StartDeleteSecretAsync(secretName, cancellationToken).ConfigureAwait(false);

                if (deletedSecret == null)
                {
                    throw new AzureKeyVaultSecretException($"Error deleting secret with key: {secretName} in Key Vault");
                }

                return true;
            }
            catch
            {
                throw new AzureKeyVaultSecretException($"Error deleting secret with key: {secretName} in Key Vault");
            }
        }
        
        private static void CheckGetSecretsParameters(string secretName)
        {
            if (string.IsNullOrWhiteSpace(secretName))
            {
                throw new ArgumentException("Parameter secretName can not be null");
            }
        }
        
        private static void CheckCreateSecretParameters(string secretName, string secretValue)
        {
            if (string.IsNullOrWhiteSpace(secretName) || string.IsNullOrWhiteSpace(secretValue))
            {
                throw new ArgumentException("Parameter secretName or secretValue, can not be null");
            }
        }
        
        private static void CheckDeleteSecretParameters(string secretName)
        {
            if (string.IsNullOrWhiteSpace(secretName))
            {
                throw new ArgumentException($"Parameter secretName can not be null,{nameof(secretName)}");
            }
        }
    }
}