using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OASP4Net.Infrastructure.AOP.Configuration;

namespace OASP4Net.Application.Configuration.Startup
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjectionService(this IServiceCollection services)
        {
            services.AddAopDependencyInjectionService();
            services.AddAutoMapper();
        }
    }
}
