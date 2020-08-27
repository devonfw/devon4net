using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Common.Options
{
    public static class OptionsHandler
    {
        public static T GetTypedOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class, new()
        {
            services.Configure<T>(configuration.GetSection(sectionName));
            using var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>()?.Value;
        }
    }
}
