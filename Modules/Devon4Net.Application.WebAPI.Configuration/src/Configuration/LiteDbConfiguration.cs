using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class LiteDbConfiguration
    {
        public static void SetupLiteDb(this IServiceCollection services)
        {
            services.AddSingleton<ILiteDbContext, LiteDbContext>();
        }
    }
}
