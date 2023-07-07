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
        var employeeDtos = await GetProjection((IQueryable<Employee> employee) => employee
            .Select(responseDto => new EmployeeDto
            {
                Name = responseDto.Name,
                Surname = responseDto.Surname,
                Mail = responseDto.Mail
            })
            .Where(x => x.Id == id)
        );

        return employeeDtos.FirstOrDefault() ?? throw new EmployeeNotFoundException();
    }
}