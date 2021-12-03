using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Common.Handlers
{
    public static class OptionsHandler
    {
        public static T GetTypedOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class, new()
        {
            var section = configuration.GetSection(sectionName);
            services.Configure<T>(section);
            using var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>()?.Value;
        }
    }
}
