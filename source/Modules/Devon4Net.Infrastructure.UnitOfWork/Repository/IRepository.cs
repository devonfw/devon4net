using System.ComponentModel;
using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Pagination;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.UnitOfWork.Repository;

public interface IRepository<T> where T : class
{
    Task<T> Create(T entity, bool autoSaveChanges = true, bool detach = true);

    Task<T> Update(T entity, bool autoSaveChanges = true, bool detach = true);

    Task<T> Delete(T entity, bool autoSaveChanges = true, bool detach = true);

    Task<bool> Delete(Expression<Func<T, bool>> predicate = null, bool autoSaveChanges = true);

    Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate = null);

    Task<T> GetLastOrDefault(Expression<Func<T, bool>> predicate = null);

    Task<IList<T>> Get(Expression<Func<T, bool>> predicate = null);

    Task<IList<T>> Get(IList<string> include, Expression<Func<T, bool>> predicate = null);

    Task<PaginationResult<T>> Get(int currentPage, int pageSize, IList<string> includedNestedFiels,
        Expression<Func<T, bool>> predicate = null);

    Task<PaginationResult<T>> Get(int currentPage, int pageSize, Expression<Func<T, bool>> predicate = null);

    Task<IList<T>> Get<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector,
        ListSortDirection sortDirection);

    Task<PaginationResult<T>> Get<TKey>(int currentPage, int pageSize, Expression<Func<T, bool>> predicate,
        Expression<Func<T, TKey>> keySelector, ListSortDirection sortDirection);

    Task<long> Count(Expression<Func<T, bool>> predicate = null);

    Task<List<T>> ExecuteSpGetList(string spName, params SqlParameter[] parameters);

    Task<int> ExecuteSp(string spName, params SqlParameter[] parameters);
}