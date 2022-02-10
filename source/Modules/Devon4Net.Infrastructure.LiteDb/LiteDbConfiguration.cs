using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.LiteDb;
using Devon4Net.Infrastructure.LiteDb.LiteDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class LiteDbConfiguration
    {
        public static void SetupLiteDb(this IServiceCollection services, ref IConfiguration configuration)
        {
            var liteDbOptions = services.GetTypedOptions<LiteDbOptions>(configuration, "LiteDb");
            if (liteDbOptions == null || string.IsNullOrEmpty(liteDbOptions?.DatabaseLocation)) return;

            services.AddSingleton<ILiteDbContext, LiteDbContext>();
        }
    }
}
