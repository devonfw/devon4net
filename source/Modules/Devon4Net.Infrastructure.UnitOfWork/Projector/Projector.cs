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
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<TResult>> GetProjection<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> projection,
        int? page = null,
        int? pageSize = null) where TEntity : class
    {
        var dbSet = _context.Set<TEntity>();

        var query = dbSet.AsQueryable();

        if (page != null && pageSize != null)
        {
            query = query.Skip((int)(page - 1) * (int)pageSize).Take((int)pageSize);
        }

        var projectionQuery = projection(query);

        return await projectionQuery.ToListAsync();
    }
}