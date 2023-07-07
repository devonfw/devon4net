using Devon4Net.Domain.UnitOfWork.Enums;
using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.MediatR.Behaviors;
using FluentValidation;
using MediatR;
using System.Reflection;
using Devon4Net.Infrastructure.Persistence;
using Devon4Net.Infrastructure.UnitOfWork.Common;

namespace Devon4Net.Presentation;

public static class Configuration
{
    public static void SetupCustomDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        SetupDatabase(services, configuration);
        SetupMediatRHandlers(services);
        SetUpAutoRegisterClasses(services);
    }

    private static void SetUpAutoRegisterClasses(IServiceCollection services)
    {
        List<Assembly> assemblyNamespaceToScan = new()
        {
            typeof(Application.AssemblyReference).Assembly,
            typeof(Infrastructure.AssemblyReference).Assembly,
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

    private static void SetupMediatRHandlers(IServiceCollection services)
    {
        var assembly = typeof(Application.AssemblyReference).Assembly;

        services.AddMediatR(assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    /// <summary>
    /// Setup here your database connections.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private static void SetupDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.SetupDatabase<EmployeeContext>(configuration, "Employee", DatabaseType.InMemory).ConfigureAwait(false);
    }
}