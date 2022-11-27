using Azure.Identity;
using Devon4Net.Infrastructure.Azure.KeyVault.Handlers;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.Azure.KeyVault
{
    public class AzureKeyVaultConfigurationProvider : ConfigurationProvider
    {
        private IKeyVaultHandler KeyVaultHandler { get;}

        public AzureKeyVaultConfigurationProvider(Uri keyVaultUri, DefaultAzureCredential credentials)
        {
            KeyVaultHandler = new KeyVaultHandler(keyVaultUri, credentials);
        }
        
        public override void Load()
        {
            base.Load();
            Data = GetKeyVaultHandlerData(default).GetAwaiter().GetResult();
        }

        private async Task<IDictionary<string, string>> GetKeyVaultHandlerData(CancellationToken cancellationToken)
        {
            return await KeyVaultHandler.GetSecrets(cancellationToken).ConfigureAwait(false);
        }
    }
}
