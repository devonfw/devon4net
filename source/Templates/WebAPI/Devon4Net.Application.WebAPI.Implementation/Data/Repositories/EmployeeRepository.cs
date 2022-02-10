using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Application.WebAPI.Implementation.Domain.Database;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;
using Devon4Net.Application.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.Application.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the Employee
    /// </summary>
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
            Devon4NetLogger.Debug("GetEmployee method from EmployeeRepository Employeeervice");
            return Get(predicate);
        }

        /// <summary>
        /// Gets the Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Employee> GetEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"GetEmployeeById method from repository Employeeervice with value : {id}");
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
            Devon4NetLogger.Debug($"SetEmployee method from repository Employeeervice with value : {name}");
            return Create(new Employee{Name = name, Surname = surName, Mail = mail});
        }

        /// <summary>
        /// Deletes the Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"DeleteEmployeeById method from repository Employeeervice with value : {id}");
            var deleted = await Delete(t => t.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw  new ArgumentException($"The Employee entity {id} has not been deleted.");
        }
    }
}
