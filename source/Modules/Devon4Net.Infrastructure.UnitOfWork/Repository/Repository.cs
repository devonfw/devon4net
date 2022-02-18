using System.ComponentModel;
using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Exceptions;
using Devon4Net.Domain.UnitOfWork.Pagination;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Domain.UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }
        private bool DbContextBehaviour  { get; }

        private IQueryable<T> Queryable => SetQuery<T>();

        /// <summary>
        /// Initialization class
        /// </summary>
        /// <param name="context">The data base context to work with</param>
        /// <param name="dbContextBehaviour">Sets the AutoDetectChangesEnabled, LazyLoadingEnabled and QueryTrackingBehavior flag to true or false</param>
        public Repository(DbContext context, bool dbContextBehaviour = false)
        {
            DbContext = context;
            DbContextBehaviour = dbContextBehaviour;
        }

        public async Task<T> Create(T entity, bool autoSaveChanges= true, bool detach = true)
        {
            try
            {
                var result = await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
                result.State = EntityState.Added;
                if (autoSaveChanges) await DbContext.SaveChangesAsync().ConfigureAwait(false);
                if (detach) result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<T> Update(T entity, bool autoSaveChanges = true, bool detach = true)
        {
            try
            {
                var result = DbContext.Set<T>().Update(entity);
                result.State = EntityState.Modified;
                if (autoSaveChanges) await DbContext.SaveChangesAsync().ConfigureAwait(false);
                if (detach) result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<T> Delete(T entity, bool autoSaveChanges = true, bool detach = true)
        {
            try
            {
                var result = DbContext.Set<T>().Remove(entity);
                result.State = EntityState.Deleted;
                if (autoSaveChanges) await DbContext.SaveChangesAsync().ConfigureAwait(false);
                if (detach) result.State = EntityState.Detached;
                return result.Entity;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> predicate = null, bool autoSaveChanges = true)
        {
            try
            {
                var entities = await Get(predicate).ConfigureAwait(false);
                DbContext.Set<T>().RemoveRange(entities);
                if (autoSaveChanges) await DbContext.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return GetQueryFromPredicate(predicate).FirstOrDefaultAsync();
        }

        public Task<T> GetLastOrDefault(Expression<Func<T, bool>> predicate = null)
        {
            return GetQueryFromPredicate(predicate).LastOrDefaultAsync();
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            return await GetQueryFromPredicate(predicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<T>> Get<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection)
        {
            return await GetSortedQueryFromPredicate(predicate, keySelector, sortDirection).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<T>> Get(IList<string> include, Expression<Func<T, bool>> predicate = null)
        {
            return await GetResultSetWithNestedProperties(include, predicate).ToListAsync().ConfigureAwait(false);
        }

        public Task<PaginationResult<T>> Get(int currentPage, int pageSize, IList<string> includedNestedFiels, Expression<Func<T, bool>> predicate = null)
        {
            return GetResultSetWithNestedPropertiesPaged(currentPage, pageSize, includedNestedFiels, predicate);
        }

        public Task<PaginationResult<T>> Get(int currentPage, int pageSize, Expression<Func<T, bool>> predicate = null)
        {
            return GetPagedResult(currentPage, pageSize, GetQueryFromPredicate(predicate));
        }

        public Task<PaginationResult<T>> Get<TKey>(int currentPage, int pageSize, Expression<Func<T, bool>> predicate , Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection)
        {
            return GetPagedResult(currentPage, pageSize, GetSortedQueryFromPredicate(predicate, keySelector, sortDirection));
        }

        public Task<long> Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? Queryable.LongCountAsync() : Queryable.LongCountAsync(predicate);
        }

        private IQueryable<T> GetQueryFromPredicate(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? Queryable.Where(predicate): Queryable;
        }

        private IQueryable<T> GetSortedQueryFromPredicate<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection)
        {
            if (sortDirection == ListSortDirection.Ascending)
            {
                return predicate != null ? Queryable.Where(predicate).OrderBy(keySelector) : Queryable.OrderBy(keySelector);
            }

            return predicate != null ? Queryable.Where(predicate).OrderByDescending(keySelector) : Queryable.OrderByDescending(keySelector);
        }

        private IQueryable<T> GetResultSetWithNestedProperties(IList<string> includedNestedFiels, Expression<Func<T, bool>> predicate = null)
        {
            return includedNestedFiels.Aggregate(GetQueryFromPredicate(predicate), (current, property) => current.Include(property));
        }

        private Task<PaginationResult<T>> GetResultSetWithNestedPropertiesPaged(int currentPage, int pageSize, IList<string> includedNestedFiels, Expression<Func<T, bool>> predicate = null)
        {
            return GetPagedResult(currentPage, pageSize, GetResultSetWithNestedProperties(includedNestedFiels, predicate));
        }

        private static async Task<PaginationResult<T>> GetPagedResult(int currentPage, int pageSize, IQueryable<T> resultList)
        {
            var pagedResult = new PaginationResult<T>() { CurrentPage = currentPage, PageSize = pageSize, RowCount = await resultList.CountAsync().ConfigureAwait(false) };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;
            pagedResult.Results = await resultList.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return pagedResult;
        }

        private IQueryable<S> SetQuery<S>() where S : class
        {
            SetContextBehaviour(DbContextBehaviour);
            return DbContext.Set<S>().AsNoTracking();
        }

        private void SetContextBehaviour( bool enabled)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = enabled;

            DbContext.ChangeTracker.LazyLoadingEnabled = enabled;

            DbContext.ChangeTracker.QueryTrackingBehavior = enabled ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
        }

        internal void SetContext(DbContext context)
        {
            DbContext = context ?? throw new ContextNullException(nameof(context));
        }
    }
}
