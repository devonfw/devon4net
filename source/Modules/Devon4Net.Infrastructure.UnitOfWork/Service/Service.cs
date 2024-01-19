using Devon4Net.Infrastructure.UnitOfWork.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Devon4Net.Infrastructure.UnitOfWork.Service
{
    public class Service<TContext> : IService where TContext : DbContext
    {
        public IUnitOfWork<TContext> UoW { get; }

        public Service(IUnitOfWork<TContext> uoW)
        {
            UoW = uoW;
        }
    }
}
