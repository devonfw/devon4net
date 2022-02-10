using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Devon4Net.Domain.UnitOfWork.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task<IDbContextTransaction> BeginTransaction();
        Task Commit(IDbContextTransaction transaction);
        T Repository<T>() where T : class;
        T Repository<T,TS>() where T : class where TS : class;
        Task Rollback(IDbContextTransaction transaction);
        IExecutionStrategy CreateExecutionStrategy();
    }
}
