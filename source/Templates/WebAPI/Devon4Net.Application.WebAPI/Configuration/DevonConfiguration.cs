using System.Reflection;
using Devon4Net.Application.WebAPI.Business.EmployeeManagement.Dto;
using Devon4Net.Application.WebAPI.Business.EmployeeManagement.Validators;
using Devon4Net.Application.WebAPI.Business.MediatRManagement.Commands;
using Devon4Net.Application.WebAPI.Business.MediatRManagement.Dto;
using Devon4Net.Application.WebAPI.Business.MediatRManagement.Handlers;
using Devon4Net.Application.WebAPI.Business.MediatRManagement.Queries;
using Devon4Net.Application.WebAPI.Business.RabbitMqManagement.Handlers;
using Devon4Net.Application.WebAPI.Business.TodoManagement.Dto;
using Devon4Net.Application.WebAPI.Business.TodoManagement.Validators;
using Devon4Net.Application.WebAPI.Domain.Database;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.FluentValidation;
using Devon4Net.Infrastructure.JWT.Common;
using Devon4Net.Infrastructure.MediatR.Options;
using Devon4Net.Infrastructure.RabbitMQ.Options;
using Devon4Net.Infrastructure.RabbitMQ.Samples.Handllers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.MediatR.Behaviors;
using Devon4Net.Infrastructure.RabbitMQ;
using Devon4Net.Infrastructure.UnitOfWork.Common;
using Devon4Net.Infrastructure.UnitOfWork.Enums;

namespace Devon4Net.Application.WebAPI.Configuration
{
    /// <summary>
    /// DevonConfiguration class
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
        /// <param name="configuration"></param>
        public static void SetupCustomDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            SetupDatabase(services, configuration);
            SetUpAutoRegisterClasses(services);
            SetupJwtPolicies(services);
            SetupFluentValidators(services);

            using var serviceProvider = services.BuildServiceProvider();

            var mediatR = serviceProvider.GetService<IOptions<MediatROptions>>();
            var rabbitMq = serviceProvider.GetService<IOptions<RabbitMqOptions>>();

            if (rabbitMq?.Value != null && rabbitMq.Value.EnableRabbitMq)
            {
                SetupRabbitHandlers(services);
            }

            if (mediatR?.Value != null && mediatR.Value.EnableMediatR)
            {
                SetupMediatRHandlers(services);
            }
        }
        
        private static void SetUpAutoRegisterClasses(IServiceCollection services)
        {
            List<Assembly> assemblyNamespaceToScan = new()
            {
                Assembly.GetExecutingAssembly(),
            };

            var suffixNamesToRegister = new List<string>
            {
                "Projector",
                "Repository",
                "Service",
                "QueryBuilder"
            };

            services.AutoRegisterClasses(assemblyNamespaceToScan, suffixNamesToRegister, ServiceLifetime.Transient);
        }

        private static void SetupRabbitHandlers(IServiceCollection services)
        {
            services.AddRabbitMqHandler<UserSampleRabbitMqHandler>(true);
            services.AddRabbitMqHandler<TodoRabbitMqHandler>(true);
        }

        private static void SetupMediatRHandlers(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void SetupFluentValidators(IServiceCollection services)
        {
            services.AddFluentValidation< IValidator<TodoDto>, TodosFluentValidator>();
            services.AddFluentValidation<IValidator<EmployeeDto>, EmployeeFluentValidator>();
        }

        /// <summary>
        /// Setup here your database connections.
        /// To use RabbitMq message backup declare the 'RabbitMqBackupContext' database setup
        /// PE: services.SetupDatabase&lt;RabbitMqBackupContext&gt;($"Data Source={FileOperations.GetFileFullPath("RabbitMqBackupSqLite.db")}", DatabaseType.Sqlite);
        /// Please add the connection strings to enable the backup messaging for MediatR abd RabbitMq using MediatRBackupContext and RabbitMqBackupContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void SetupDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.SetupDatabase<TodoContext>(configuration, "Default", DatabaseType.InMemory).ConfigureAwait(false);
            services.SetupDatabase<EmployeeContext>(configuration, "Employee", DatabaseType.InMemory).ConfigureAwait(false);
        }

        private static void SetupJwtPolicies(IServiceCollection services)
        {
            services.AddJwtPolicy(AuthConst.DevonSamplePolicy, ClaimTypes.Role, AuthConst.DevonSampleUserRole);
        }
    }
}
