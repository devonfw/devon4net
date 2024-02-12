using System.Data;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.UnitOfWork.Exceptions;
using Devon4Net.Infrastructure.UnitOfWork.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Devon4Net.Infrastructure.UnitOfWork.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private const int DefaultSecondsTimeout = 30;

        protected UnitOfWork(TContext context)
        {
            Context = context ?? throw new ContextNullException(nameof(context));
        }

        private TContext Context { get; }

        public async Task<IDbTransaction> GetDbTransaction(int secondsTimeout = DefaultSecondsTimeout)
        {
            Context.Database.SetCommandTimeout(secondsTimeout);

            var dbContextTransaction = await Context.Database.BeginTransactionAsync();
            return dbContextTransaction.GetDbTransaction();
        }

        public Task SaveChanges()
        {
            //Auditable entities
            return Context.SaveChangesAsync();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }

        public async Task CommitTransaction(IDbTransaction transaction)
        {
            if (transaction == null)
                throw new TransactionNullException("Transaction cannot be null to perform transaction operations.");

            try
            {
                await SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                transaction.Rollback();
                throw;
            }
            finally
            {
                Context.Database.SetCommandTimeout(DefaultSecondsTimeout);
                transaction.Dispose();
            }
        }
    }
}