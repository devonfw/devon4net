using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
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
        /// Get TODO method
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IList<Employee>> GetEmployee(Expression<Func<Employee, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetTodo method from TodoRepository Employeeervice");
            return await Get(predicate).ConfigureAwait(false);
        }

        /// <summary>
        /// Geto the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"GetTodoById method from repository Employeeervice with value : {id}");
            return await GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the TODO
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<Employee> Create(string name, string surName, string mail)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository Employeeervice with value : {name}");
            return await Create(new Employee{Name = name, Surname = surName, Mail = mail}).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the TODO by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"DeleteTodoById method from repository Employeeervice with value : {id}");
            var deleted = await Delete(t => t.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw  new ApplicationException($"The Todo entity {id} has not been deleted.");
        }
    }
}
