using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.Nexus;
using Devon4Net.Infrastructure.Nexus.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Nexus
{
    public static class NexusConfiguration
    {
        public static void SetupNexus(this IServiceCollection services, IConfiguration configuration)
        {
            services.GetTypedOptions<NexusOptions>(configuration, "Nexus");
            services.AddTransient<INexusHandler, NexusHandler>();
        }
    }
}