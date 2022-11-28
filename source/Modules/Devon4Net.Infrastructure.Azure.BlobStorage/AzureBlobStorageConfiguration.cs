using Devon4Net.Infrastructure.Azure.BlobStorage.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Azure.BlobStorage
{
    public static class AzureBlobStorageConfiguration
    {
        public static void SetUpBlobStorage(this IServiceCollection services)
        {
            services.AddTransient<IBlobStorageHandler, BlobStorageHandler>();
        }
    }
}