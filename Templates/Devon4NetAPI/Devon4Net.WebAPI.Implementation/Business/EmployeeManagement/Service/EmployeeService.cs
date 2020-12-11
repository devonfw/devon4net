    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Exceptions;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;

namespace Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Service
{
    /// <summary>
    /// Employee service implementation
    /// </summary>
    public class EmployeeService: Service<EmployeeContext>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uoW"></param>
        public EmployeeService(IUnitOfWork<EmployeeContext> uoW) : base(uoW)
        {
            _employeeRepository = uoW.Repository<IEmployeeRepository>();
        }

        /// <summary>
        /// Gets the Employee
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeeDto>> GetEmployee(Expression<Func<Employee, bool>> predicate = null)
        {
            Devon4NetLogger.Debug("GetEmployee method from service Employeeervice");
            var result = await _employeeRepository.GetEmployee(predicate).ConfigureAwait(false);
            return result.Select(EmployeeConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Employee> GetEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"GetEmployeeById method from service Employeeervice with value : {id}");
            return _employeeRepository.GetEmployeeById(id);
        }

        /// <summary>
        /// Creates the Employee
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public Task<Employee> CreateEmployee(string name, string surName, string mail)
        {
            Devon4NetLogger.Debug($"SetEmployee method from service Employeeervice with value : {name}, {surName}, {mail}");

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The 'name' field can not be null.");
            }

            if (string.IsNullOrEmpty(surName) || string.IsNullOrWhiteSpace(surName))
            {
                throw new ArgumentException("The 'surName' field can not be null.");
            }

            if (string.IsNullOrEmpty(mail) || string.IsNullOrWhiteSpace(mail))
            {
                throw new ArgumentException("The 'mail' field can not be null.");
            }

            return _employeeRepository.Create(name, surName, mail);
        }
        
        /// <summary>
        /// Deletes the Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> DeleteEmployeeById(long id)
        {
            Devon4NetLogger.Debug($"DeleteEmployeeById method from service Employeeervice with value : {id}");
            var employee = await _employeeRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (employee == null)
            {
                throw new ArgumentException($"The provided Id {id} does not exists");
            }

            return await _employeeRepository.DeleteEmployeeById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies te state of the Employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="surName"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<Employee> ModifyEmployeeById(long id, string name, string surName, string mail)
        {
            Devon4NetLogger.Debug($"ModifyEmployeeById method from service Employeeervice with value : {id}");
            var employee = await _employeeRepository.GetFirstOrDefault(t => t.Id == id).ConfigureAwait(false);

            if (employee == null)
            {
                throw new EmployeeNotFoundException($"The employee with id {id} does not exists and is not possible to delete.");
            }

            employee.Name= name;
            employee.Surname = surName;
            employee.Mail= mail;

            return await _employeeRepository.Update(employee).ConfigureAwait(false);
        }
    }
}
