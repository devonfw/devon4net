using Devon4Net.Infrastructure.Azure.BlobStorage.Options;
using Devon4Net.Infrastructure.Azure.KeyVault.Options;

namespace Devon4Net.Infrastructure.Azure.Configuration.Options
{
    public class AzureOptions
    {
        public BlobStorageOptions BlobStorage { get; set; }
        public KeyVaultOptions KeyVault { get; set; }
    }
}