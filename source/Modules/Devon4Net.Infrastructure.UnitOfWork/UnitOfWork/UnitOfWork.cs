using Devon4Net.Domain.UnitOfWork.Exceptions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Devon4Net.Domain.UnitOfWork.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private TContext Context { get; }
        private IServiceProvider ServiceProvider { get; }

        public UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            Context = context ?? throw new ContextNullException(nameof(context));
            ServiceProvider = serviceProvider;
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return Context.Database.BeginTransactionAsync();
        }

        public Task Rollback(IDbContextTransaction transaction)
        {
            return Task.Run(() => RollbackTransaction(transaction));
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }

        public Task Commit(IDbContextTransaction transaction)
        {
            return Task.Run(() =>
             {
                 if (transaction == null)
                 {
                     throw new TransactionNullException($"Transaction cannot be null to perform transaction operations.");
                 }
                 try
                 {
                     transaction.Commit();
                 }
                 catch (DbUpdateConcurrencyException ex)
                 {
                     Console.WriteLine($"{ex.Message}:{ex.InnerException}");
                     RollbackTransaction(transaction);
                     throw;
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"{ex.Message}:{ex.InnerException}");
                     RollbackTransaction(transaction);
                     throw;
                 }
             });
        }

        /// <summary>
        /// ets the typed repository class
        /// </summary>
        /// <typeparam name="T">The inherited repository class</typeparam>
        /// <returns>The instantiated repository class</returns>
        public T Repository<T>() where T : class
        {
            return GetRepository<T>();
        }

        /// <summary>
        /// Gets the typed repository class and sets the database context
        /// </summary>
        /// <typeparam name="T">The inherited repository class</typeparam>
        /// <typeparam name="TS">The entity class</typeparam>
        /// <returns>The instantiated repository class</returns>
        public T Repository<T,TS>() where T : class where TS : class
        {
            var repository = GetRepository<T>();

            (repository as Repository<TS>)?.SetContext(Context);

            return repository;
        }

        private static void RollbackTransaction(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new TransactionNullException($"Transaction cannot be null to perform transaction operations.");
            }

            transaction.Rollback();
            transaction.Dispose();
        }

        private T GetRepository<T>() where T : class
        {
            var repositoryType = typeof(T);
            var repository = ServiceProvider.GetService(repositoryType);

            if (repository == null)
            {
                throw new RepositoryNotFoundException($"The repository {repositoryType.Name} was not found in the IOC container. Plase register the repository during startup.");
            }

            return repository as T;
        }
    }
}
