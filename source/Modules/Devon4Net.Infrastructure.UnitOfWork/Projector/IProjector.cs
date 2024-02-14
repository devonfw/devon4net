using System.Linq.Expressions;

namespace Devon4Net.Infrastructure.UnitOfWork.Projector;

public interface IProjector
{
    /// <summary>
    /// Get Projection from entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="projection"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<IEnumerable<TResult>> GetProjection<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        int? page = null,
        int? pageSize = null) where TEntity : class;
}