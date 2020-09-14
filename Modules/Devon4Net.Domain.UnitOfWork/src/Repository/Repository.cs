using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Exceptions;
using Devon4Net.Domain.UnitOfWork.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Domain.UnitOfWork.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext DbContext { get; set; }
        private bool DbContextBehaviour  { get; set; }

        private IQueryable<T> Queryable => SetQuery<T>();

        public Repository(DbContext context, bool dbContextBehaviour = false)
        {
            DbContext = context;
            DbContextBehaviour = dbContextBehaviour;
        }

        public async Task<T> Create(T entity, bool detach = true)
        {
            var result = await DbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            result.State = EntityState.Added;
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            if (detach) result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<T> Update(T entity, bool detach = true)
        {
            var result = DbContext.Set<T>().Update(entity);
            result.State = EntityState.Modified;
            await DbContext.SaveChangesAsync().ConfigureAwait(false);
            if (detach) result.State = EntityState.Detached;
            return result.Entity;
        }

        public async Task<bool> Delete(T entity, bool detach = true)
        {
            var result = DbContext.Set<T>().Remove(entity);
            result.State = EntityState.Deleted;
            var deleted = await DbContext.SaveChangesAsync().ConfigureAwait(false);
            if (detach) result.State = EntityState.Detached;
            return deleted > 0;
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            var result = new List<bool>();
            var entities = await Get(predicate).ConfigureAwait(false);
            foreach (var item in entities)
            {
                result.Add(await Delete(item).ConfigureAwait(false));
            }

            return result.All(r=> r);
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

        private async Task<PaginationResult<T>> GetPagedResult(int currentPage, int pageSize, IQueryable<T> resultList)
        {
            var pagedResult = new PaginationResult<T>() { CurrentPage = currentPage, PageSize = pageSize, RowCount = resultList.Count() };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (currentPage - 1) * pageSize;
            pagedResult.Results = await resultList.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return pagedResult;
        }

        private IQueryable<T> SetQuery<T>() where T : class
        {
            SetContextBehaviour(DbContextBehaviour);
            return DbContext.Set<T>().AsNoTracking();
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
