using Devon4Net.Infrastructure.MediatR.Common;

namespace Devon4Net.Infrastructure.MediatR.Query
{
    public abstract class QueryBase<T> : ActionBase<T> where T : class
    {
    }
}