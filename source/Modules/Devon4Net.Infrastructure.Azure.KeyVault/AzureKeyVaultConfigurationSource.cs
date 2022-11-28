using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.Azure.KeyVault
{
    public class AzureKeyVaultConfigurationSource : IConfigurationSource
    {
        private Uri KeyVaultUri { get; set; }
        private DefaultAzureCredential Credentials { get; set; } 
        
        public AzureKeyVaultConfigurationSource(Uri keyVaultUri, DefaultAzureCredential credentials)
        {
            KeyVaultUri = keyVaultUri;
            Credentials = credentials;
        }
        
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureKeyVaultConfigurationProvider(KeyVaultUri, Credentials);
        }
    }
}
