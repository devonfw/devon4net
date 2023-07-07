using Devon4Net.Infrastructure.MediatR.Common;

namespace Devon4Net.Infrastructure.MediatR.Command
{
    public record CommandBase<T> : ActionBase<T> where T : class
    {
    }
}