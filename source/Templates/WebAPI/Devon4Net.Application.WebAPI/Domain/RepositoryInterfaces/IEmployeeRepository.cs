using System.Linq.Expressions;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Application.WebAPI.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Domain.RepositoryInterfaces
{
    /// <summary>
    /// EmployeeRepository interface
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// GetEmployee
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IList<Employee>> GetEmployee(Expression<Func<Employee, bool>> predicate = null);

        /// <summary>
        /// GetEmployeeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeById(long id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<Employee> Create(string name, string surName, string mail);

        /// <summary>
        /// DeleteEmployeeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteEmployeeById(long id);
    }
}
