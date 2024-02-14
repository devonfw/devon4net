using Devon4Net.Application.Dtos;
using Devon4Net.Application.Features.Command.CreateEmployee;
using Devon4Net.Application.Features.Queries.GetAllEmployees;
using Devon4Net.Application.Features.Queries.GetEmployeeById;
using Devon4Net.Infrastructure.Common.Application.Attributes;
using Devon4Net.Infrastructure.Common.Exceptions;
using Devon4Net.Infrastructure.MediatR.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Presentation.Controllers;

[ApiController]
[Route("/employees")]
[ServiceFilter(typeof(ExceptionHandlingFilterAttribute))]
public class EmployeeController : ControllerBase
{
    private readonly IMediatRHandler _mediator;

    public EmployeeController(IMediatRHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<EmployeeDto> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        throw new HttpCustomRequestException("message");
        var command = new CreateEmployeeCommand(employeeDto.Name, employeeDto.Surname, employeeDto.Mail);
        return _mediator.CommandAsync(command);
    }

    [HttpGet]
    public Task<IEnumerable<EmployeeDto>> GetAllEmployees()
    {
        var query = new GetAllEmployeesQuery();
        return _mediator.QueryAsync(query);
    }

    [HttpGet("{employeeId}")]
    public Task<EmployeeDto> GetAllEmployees([FromRoute] long employeeId)
    {
        var query = new GetEmployeeByIdQuery(employeeId);
        return _mediator.QueryAsync(query);
    }
}