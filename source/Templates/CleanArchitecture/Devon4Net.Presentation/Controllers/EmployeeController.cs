using Devon4Net.Application.Dtos;
using Devon4Net.Application.Features.Command.CreateEmployee;
using Devon4Net.Application.Features.Queries.GetAllEmployees;
using Devon4Net.Application.Features.Queries.GetEmployeeById;
using Devon4Net.Infrastructure.MediatR.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediatRHandler _mediator;

    public EmployeeController(IMediatRHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateEmployee")]
    public async Task<EmployeeDto> CreateEmployee([FromBody] EmployeeDto employeeDto)
    {
        var command = new CreateEmployeeCommand(employeeDto.Name, employeeDto.Surname, employeeDto.Mail);
        return await _mediator.CommandAsync(command);
    }

    [HttpGet("GetEmployees")]
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
    {
        var query = new GetAllEmployeesQuery();
        return await _mediator.QueryAsync(query);
    }

    [HttpGet("GetEmployeeById")]
    public async Task<EmployeeDto> GetAllEmployees([FromQuery] long id)
    {
        var query = new GetEmployeeByIdQuery(id);
        return await _mediator.QueryAsync(query);
    }
}