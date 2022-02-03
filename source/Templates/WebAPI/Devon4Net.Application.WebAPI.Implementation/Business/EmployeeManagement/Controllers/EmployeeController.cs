using System.ComponentModel.DataAnnotations;
using Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Service;
using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.EmployeeManagement.Controllers
{
    /// <summary>
    /// Employees controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeService"></param>
        public EmployeeController( IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        /// <summary>
        /// Gets the entire list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetEmployee()
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            return Ok(await _employeeService.GetEmployee().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(EmployeeDto employeeDto)
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            var result = await _employeeService.CreateEmployee(employeeDto.Name, employeeDto.Surname, employeeDto.Mail).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the employee provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([Required]long employeeId)
        {
            Devon4NetLogger.Debug("Executing GetEmployee from controller EmployeeController");
            return Ok(await _employeeService.DeleteEmployeeById(employeeId).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the employee provided the data of the employee
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyEmployee(EmployeeDto employeeDto)
        {
            Devon4NetLogger.Debug("Executing ModifyEmployee from controller EmployeeController");
            if (employeeDto == null || employeeDto.Id == 0)
            {
                return BadRequest("The id of the employee must be provided");
            }
            return Ok(await _employeeService.ModifyEmployeeById(employeeDto.Id, employeeDto.Name, employeeDto.Surname, employeeDto.Mail).ConfigureAwait(false));
        }
    }
}
