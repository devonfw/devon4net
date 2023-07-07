using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Devon4Net.Infrastructure.UnitOfWork.UnitOfWork;

public interface IUnitOfWork
{
    /// <summary>
    /// Begins the transaction
    /// </summary>
    Task<IDbTransaction> GetDbTransaction(int secondsTimeout = 30);

    IExecutionStrategy CreateExecutionStrategy();

    Task SaveChanges();

    /// <summary>
    /// Commit the transaction if is correct, else rollback. Both cases dispose.
    /// </summary>
    Task CommitTransaction(IDbTransaction transaction);
}