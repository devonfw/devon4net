using System.Threading.Tasks;
using Devon4Net.Infrastructure.MediatR.Common;
using MediatR;

namespace Devon4Net.Infrastructure.MediatR.Handler
{
    public interface IMediatRHandler
    {
        Task<TResult> QueryAsync<TResult>(ActionBase<TResult> query) where TResult : class;
        Task<TResult> CommandAsync<TResult>(ActionBase<TResult> query) where TResult : class;
    }
}