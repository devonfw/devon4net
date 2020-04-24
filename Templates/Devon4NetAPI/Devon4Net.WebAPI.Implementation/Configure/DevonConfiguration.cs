using System.Reflection;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.WebAPI.Implementation.Business.AuthManagement.Controllers;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.WebAPI.Implementation.Configure
{
    /// <summary>
    /// DevonConfiguration
    /// </summary>
    public static class DevonConfiguration
    {
        /// <summary>
        /// Sets up the service dependency injection
        /// For example:
        /// services.AddTransient"ITodoService, TodoService"();
        /// services.AddTransient"ITodoRepository, TodoRepository"();
        /// Put your DI declarations here
        /// </summary>
        /// <param name="services"></param>
        public static void SetupDevonDependencyInjection(this IServiceCollection services)
        {
            var assemblyToScan = Assembly.GetAssembly(typeof(DevonConfiguration));

            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(x => x.Name.Contains("CQRSHandler"))
                .AsSingletonPublicImplementedClasses();

            //services.AddSingleton<UserTestCqrsHandler>();
        }
    }
}
