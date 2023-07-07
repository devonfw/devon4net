using Devon4Net.Infrastructure.MediatR.Common;

namespace Devon4Net.Infrastructure.MediatR.Query
{
    public record QueryBase<T> : ActionBase<T> where T : class
    {
    }
}