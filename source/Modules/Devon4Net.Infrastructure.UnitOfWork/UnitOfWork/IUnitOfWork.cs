using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Devon4Net.Infrastructure.UnitOfWork.UnitOfWork
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task<IDbContextTransaction> GetTransaction();
        Task Commit(IDbContextTransaction transaction);
        T Repository<T>() where T : class;
        T Repository<T,TS>() where T : class where TS : class;
        IExecutionStrategy CreateExecutionStrategy();
    }
}
