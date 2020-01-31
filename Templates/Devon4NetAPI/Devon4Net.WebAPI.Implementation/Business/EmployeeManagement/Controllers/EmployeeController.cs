using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.EmployeeManagement.Controllers
{
    /// <summary>
    /// Employees controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _EmployeeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="EmployeeService"></param>
        public EmployeeController( IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }


        /// <summary>
        /// Gets the entire list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetEmployee()
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            return Ok(await _EmployeeService.GetEmployee().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Create(EmployeeDto employeeDto)
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            var result = await _EmployeeService.CreateEmployee(employeeDto.Name, employeeDto.Surname, employeeDto.Mail).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the employee provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(long), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete([Required]long EmployeeId)
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            return Ok(await _EmployeeService.DeleteEmployeeById(EmployeeId).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the employee provided the data of the employee
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> ModifyEmployee(EmployeeDto employeeDto)
        {
            Devon4NetLogger.Debug("Executing ModifyEmployee from controller EmployeeController");
            if (employeeDto == null || employeeDto.Id == 0)
            {
                return BadRequest("The id of the employee must be provided");
            }
            return Ok(await _EmployeeService.ModifyEmployeeById(employeeDto.Id, employeeDto.Name, employeeDto.Surname, employeeDto.Mail).ConfigureAwait(false));
        }
    }
}
