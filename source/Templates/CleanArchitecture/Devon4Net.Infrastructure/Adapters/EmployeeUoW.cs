using Devon4Net.Application.Ports;
using Devon4Net.Infrastructure.Persistence;
using Devon4Net.Infrastructure.UnitOfWork.UnitOfWork;

namespace Devon4Net.Infrastructure.Adapters;

public class EmployeeUoW : UnitOfWork<EmployeeContext>, IEmployeeUoW
{
    public EmployeeUoW(EmployeeContext context) : base(context)
    {
    }
}