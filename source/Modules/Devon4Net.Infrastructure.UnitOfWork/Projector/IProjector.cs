using System.Linq.Expressions;

namespace Devon4Net.Infrastructure.UnitOfWork.Projector;

public interface IProjector
{
    /// <summary>
    /// Get Projection from entity
    /// </summary>
    /// <param name="projection"></param>
    /// <param name="filter"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<TResult>> GetProjection<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
}