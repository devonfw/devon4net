using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Domain.UnitOfWork
{
    public static class Configure
    {
        public static void SetupUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}
