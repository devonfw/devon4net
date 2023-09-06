using Devon4Net.Application.Dtos;
using Devon4Net.Application.Ports.Projectors;
using Devon4Net.Infrastructure.Common;
using MediatR;

namespace Devon4Net.Application.Features.Queries.GetAllEmployees;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeProjector _employeeProjector;

    public GetAllEmployeesHandler(IEmployeeProjector employeeProjector)
    {
        _employeeProjector = employeeProjector;
    }

    public Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        Devon4NetLogger.Debug("Started GetAllEmployeesHandler");
        return _employeeProjector.GetEmployees();
    }
}