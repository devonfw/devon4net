using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.TodoManagement.Service;
using Devon4Net.WebAPI.Implementation.Data.Repositories;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
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
        /// </summary>
        /// <param name="services"></param>
        public static void SetupDevonDependencyInjection(this IServiceCollection services)
        {
            //Services
            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            //Repositories
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
