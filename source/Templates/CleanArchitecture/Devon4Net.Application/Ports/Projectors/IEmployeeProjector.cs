using Devon4Net.Application.Dtos;

namespace Devon4Net.Application.Ports.Projectors;

public interface IEmployeeProjector
{
    public Task<EmployeeDto> GetEmployeeById(long id);

    public Task<IEnumerable<EmployeeDto>> GetEmployees();
}