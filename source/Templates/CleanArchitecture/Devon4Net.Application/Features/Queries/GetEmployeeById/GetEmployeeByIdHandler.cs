using Devon4Net.Application.Dtos;
using Devon4Net.Application.Ports.Projectors;
using Devon4Net.Infrastructure.Common;
using MediatR;

namespace Devon4Net.Application.Features.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IEmployeeProjector _employeeProjector;

    public GetEmployeeByIdHandler(IEmployeeProjector employeeProjector)
    {
        _employeeProjector = employeeProjector;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Devon4NetLogger.Debug("Started GetAllEmployeesHandler");
        return await _employeeProjector.GetEmployeeById(request.Id);
    }
}