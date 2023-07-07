using Devon4Net.Application.Converters;
using Devon4Net.Application.Dtos;
using Devon4Net.Application.Ports.Repositories;
using Devon4Net.Infrastructure.Common;
using MediatR;

namespace Devon4Net.Application.Features.Queries.GetAllEmployees;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        Devon4NetLogger.Debug("Started GetAllEmployeesHandler");
        var result = await _employeeRepository.GetEmployee().ConfigureAwait(false);
        return result.Select(EmployeeConverter.ModelToDto);
    }
}