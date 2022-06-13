using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.LiteDb;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Devon4Net.Infrastructure.LiteDb.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class LiteDbConfiguration
    {
        public static void SetupLiteDb(this IServiceCollection services, IConfiguration configuration)
        {
            var liteDbOptions = services.GetTypedOptions<LiteDbOptions>(configuration, "LiteDb");
            if (liteDbOptions == null || string.IsNullOrEmpty(liteDbOptions?.DatabaseLocation) || !liteDbOptions.EnableLiteDb) return;

            services.AddSingleton<ILiteDbContext, LiteDbContext>();
            services.AddTransient(typeof(ILiteDbRepository<>),typeof(LiteDbRepository<>));
        }
    }
}
