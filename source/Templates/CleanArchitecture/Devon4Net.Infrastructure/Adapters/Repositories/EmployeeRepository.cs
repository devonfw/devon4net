using System.Linq.Expressions;
using Devon4Net.Application.Ports.Repositories;
using Devon4Net.Domain.Entities;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.Persistence;
using Devon4Net.Infrastructure.UnitOfWork.Repository;

namespace Devon4Net.Infrastructure.Adapters.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public EmployeeRepository(EmployeeContext context) : base(context)
    {
    }

    /// <summary>
    /// Get Employee method
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Task<IList<Employee>> GetEmployee(Expression<Func<Employee, bool>> predicate = null)
    {
        Devon4NetLogger.Debug("GetEmployee method from EmployeeRepository EmployeeService");
        return Get(predicate);
    }

    /// <summary>
    /// Gets the Employee by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Employee> GetEmployeeById(long id)
    {
        Devon4NetLogger.Debug($"GetEmployeeById method from repository EmployeeService with value : {id}");
        return GetFirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Creates the Employee
    /// </summary>
    /// <param name="name"></param>
    /// <param name="surName"></param>
    /// <param name="mail"></param>
    /// <returns></returns>
    public Task<Employee> Create(string name, string surName, string mail)
    {
        Devon4NetLogger.Debug($"SetEmployee method from repository EmployeeService with value : {name}");
        return Create(new Employee { Name = name, Surname = surName, Mail = mail });
    }

    /// <summary>
    /// Deletes the Employee by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<long> DeleteEmployeeById(long id)
    {
        Devon4NetLogger.Debug($"DeleteEmployeeById method from repository EmployeeService with value : {id}");
        var deleted = await Delete(t => t.Id == id).ConfigureAwait(false);

        if (deleted)
        {
            return id;
        }

        throw new ArgumentException($"The Employee entity {id} has not been deleted.");
    }
}