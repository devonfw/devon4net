using System.Linq.Expressions;
using Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Service
{
    /// <summary>
    /// IEmployeeService
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// GetEmployee
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeDto>> GetEmployee(Expression<Func<Employee, bool>> predicate = null);

        /// <summary>
        /// GetEmployeeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeById(long id);

        /// <summary>
        /// CreateEmployee
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<Employee> CreateEmployee(string name, string surName, string mail);

        /// <summary>
        /// DeleteEmployeeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> DeleteEmployeeById(long id);

        /// <summary>
        /// ModifyEmployeeById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<Employee> ModifyEmployeeById(long id, string name, string surName, string mail);
    }
}