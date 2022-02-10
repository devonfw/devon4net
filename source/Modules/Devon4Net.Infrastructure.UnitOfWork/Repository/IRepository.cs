using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Pagination;

namespace Devon4Net.Domain.UnitOfWork.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity, bool detach = true);
        Task<T> Update(T entity, bool detach = true);
        Task<bool> Delete(T entity, bool detach = true);
        Task<bool> Delete(Expression<Func<T, bool>> predicate = null);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate = null);
        Task<T> GetLastOrDefault(Expression<Func<T, bool>> predicate = null);
        Task<IList<T>> Get(Expression<Func<T, bool>> predicate = null);
        Task<IList<T>> Get(IList<string> include, Expression<Func<T, bool>> predicate = null);
        Task<PaginationResult<T>> Get(int currentPage, int pageSize, IList<string> includedNestedFiels, Expression<Func<T, bool>> predicate = null);
        Task<PaginationResult<T>> Get(int currentPage, int pageSize, Expression<Func<T, bool>> predicate = null);
        Task<IList<T>> Get<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection);
        Task<PaginationResult<T>> Get<TKey>(int currentPage, int pageSize, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection);
        Task<long> Count(Expression<Func<T, bool>> predicate = null);
    }
}
