using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.UnitOfWork.Projector;

public class Projector : IProjector
{
    private readonly DbContext _context;

    protected Projector(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get Projection from entity
    /// </summary>
    /// <param name="projection"></param>
    /// <param name="filter"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<TResult>> GetProjection<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        Expression<Func<TEntity, bool>> filter = null) where TEntity : class
    {
        var dbSet = _context.Set<TEntity>();

        var query = dbSet.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        var projectionQuery = projection(query);

        return await projectionQuery.ToListAsync();
    }
}