using Azure.Identity;
using Devon4Net.Infrastructure.Azure.KeyVault.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Azure.KeyVault
{
    public static class AzureKeyVaultConfiguration
    {
        public static IConfigurationBuilder SetAzureKeyVault(this IConfigurationBuilder configurationBuilder, IServiceCollection services, Uri keyVaultUri)
        {
            services.AddTransient<IKeyVaultHandler, KeyVaultHandler>();

            configurationBuilder.Add(new AzureKeyVaultConfigurationSource(keyVaultUri, new DefaultAzureCredential()));
            return configurationBuilder;
        }
    }
}