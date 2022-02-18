using Devon4Net.Domain.UnitOfWork.Exceptions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Globalization;

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

        public Task<IDbContextTransaction> GetTransaction()
        {
            return Context.Database.BeginTransactionAsync();
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }

        public Task Commit(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new TransactionNullException("Transaction cannot be null to perform transaction operations.");
            }

            var transactionSavePointName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            return Task.Run(async () =>
              {
                  try
                  {
                      transaction.CreateSavepoint(transactionSavePointName);
                      _ = await SaveChanges().ConfigureAwait(false);
                      await transaction.CommitAsync().ConfigureAwait(false);
                  }
                  catch (DbUpdateConcurrencyException ex)
                  {
                      Devon4NetLogger.Error(ex);
                      RollbackTransaction(transaction, transactionSavePointName);
                      throw;
                  }
                  catch (Exception ex)
                  {
                      Devon4NetLogger.Error(ex);
                      RollbackTransaction(transaction, transactionSavePointName);
                      throw;
                  }
              });
        }

        private async Task<bool> SaveChanges()
        {
            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (OperationCanceledException ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    var databaseValues = entry.GetDatabaseValues();

                    // Refresh original values to bypass next concurrency check
                    entry.OriginalValues.SetValues(databaseValues);
                }

                Devon4NetLogger.Error(ex);
                throw;
            }
            catch (DbUpdateException ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
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

        private static void RollbackTransaction(IDbContextTransaction transaction, string savePoint)
        {
            if (transaction == null)
            {
                const string message = "Transaction cannot be null to perform transaction operations";
                Devon4NetLogger.Error(message);
                throw new TransactionNullException(message);
            }

            transaction.RollbackToSavepoint(savePoint);
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
