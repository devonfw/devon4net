using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.UnitOfWork
{
    public static class UnitOfWorkConfiguration
    {
        public static void SetupUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        /// <summary>
        /// Auto registers the Service and Repository classes from the assembly that contains the assemblyContainerClass class
        /// </summary>
        /// <param name="services">dotner service collection</param>
        /// <param name="assemblyContainerClass">Type of the class that is located in the assembly to scan</param>
        /// <param name="serviceSufix">Sufix namme of Service class</param>
        /// <param name="repositorySufix">Sufix namme of Repository class</param>
        public static void SetupUnitOfWork(this IServiceCollection services, Type assemblyContainerClass, string serviceSufix = "Service", string repositorySufix = "Repository")
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            services.AutoRegisterClasses(new List<Type> { assemblyContainerClass }, new List<string> { serviceSufix, repositorySufix });
        }
    }
}
