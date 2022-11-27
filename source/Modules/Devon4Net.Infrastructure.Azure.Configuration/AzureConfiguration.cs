using Azure.Identity;
using Devon4Net.Infrastructure.Azure.BlobStorage;
using Devon4Net.Infrastructure.Azure.Configuration.Options;
using Devon4Net.Infrastructure.Azure.KeyVault;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Azure.Configuration
{
    public static class AzureConfiguration
    {
        public static void ConfigureDevonfwAzure(this IServiceCollection services, IConfiguration configuration, bool setupDevon = false)
        {
            var azureOptions = services.GetTypedOptions<AzureOptions>(configuration, "Azure");
            var credentials = new DefaultAzureCredential();
            services.AddAzureClients(azureClientFactoryBuilder =>
            {
                azureClientFactoryBuilder.AddSecretClient(azureOptions.KeyVault.KeyVaultUri);
                azureClientFactoryBuilder.AddBlobServiceClient(azureOptions.BlobStorage.ConnectionString);
                azureClientFactoryBuilder.UseCredential(credentials);
            });

            (configuration as IConfigurationBuilder)?.SetAzureKeyVault(services, azureOptions.KeyVault.KeyVaultUri);

            services.SetUpBlobStorage();
        }
    }
}
