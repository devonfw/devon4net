using Devon4Net.Infrastructure.Common.Helpers;
using Devon4Net.Infrastructure.UnitOfWork.Repository;
using Devon4Net.Infrastructure.UnitOfWork.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.UnitOfWork;

public static class UnitOfWorkConfiguration
{
    public static void SetupUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    }
}