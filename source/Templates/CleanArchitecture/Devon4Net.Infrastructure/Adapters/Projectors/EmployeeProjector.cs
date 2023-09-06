using Devon4Net.Application.Dtos;
using Devon4Net.Application.Exceptions;
using Devon4Net.Application.Ports.Projectors;
using Devon4Net.Domain.Entities;
using Devon4Net.Infrastructure.Persistence;
using Devon4Net.Infrastructure.UnitOfWork.Projector;

namespace Devon4Net.Infrastructure.Adapters.Projectors;

public class EmployeeProjector : Projector, IEmployeeProjector
{
    public EmployeeProjector(EmployeeContext context) : base(context)
    {
    }

    public async Task<EmployeeDto> GetEmployeeById(long id)
    {
        var query = (IQueryable<Employee> employeeQuery) => employeeQuery
        .Select(employee => new EmployeeDto
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Mail = employee.Mail
        })
        .Where(employee => employee.Id == id);

        var employeeDtos = await GetProjection(query);

        return employeeDtos.FirstOrDefault() ?? throw new EmployeeNotFoundException();
    }

    public Task<IEnumerable<EmployeeDto>> GetEmployees()
    {
        var query = (IQueryable<Employee> employeeQuery) => employeeQuery.Select(employee => new EmployeeDto
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Mail = employee.Mail
        });

        return GetProjection(query);
    }
}