using Devon4Net.Application.Converters;
using Devon4Net.Application.Dtos;
using Devon4Net.Application.Ports.Repositories;
using Devon4Net.Infrastructure.Common;
using MediatR;

namespace Devon4Net.Application.Features.Command.CreateEmployee;

public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Devon4NetLogger.Debug("Started CreateEmployeeHandler");

        CheckRequestParams(request);

        var employee = await _employeeRepository.Create(request.Name!, request.Surname!, request.Mail!);

        return EmployeeConverter.ModelToDto(employee);
    }

    private static void CheckRequestParams(CreateEmployeeCommand request)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("The 'name' field can not be null.");
        }

        if (string.IsNullOrEmpty(request.Surname) || string.IsNullOrWhiteSpace(request.Surname))
        {
            throw new ArgumentException("The 'surName' field can not be null.");
        }

        if (string.IsNullOrEmpty(request.Mail) || string.IsNullOrWhiteSpace(request.Mail))
        {
            throw new ArgumentException("The 'mail' field can not be null.");
        }
    }
}