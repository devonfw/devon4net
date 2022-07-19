using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Application.WebAPI.Implementation.Business.RabbitMqManagement.Commands;
using Devon4Net.Application.WebAPI.Implementation.Business.RabbitMqManagement.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.RabbitMQ.Options;

namespace Devon4Net.Application.WebAPI.Implementation.Business.RabbitMqManagement.Controllers
{
    /// <summary>
    /// Controller sample to show how RabbitMq works
    /// Please setup your RabbitMq server in the configuration file
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class RabbitMqController : ControllerBase
    {
        private TodoRabbitMqHandler TodoRabbitMqHandler { get; }
        private RabbitMqOptions RabbitMqOptions { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="todoRabbitMqHandler">The main handler injected via DI</param>
        /// <param name="rabbitMqOptions">The RabbitMq options to check if there is any instance set up</param>
        public RabbitMqController(TodoRabbitMqHandler todoRabbitMqHandler, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            TodoRabbitMqHandler = todoRabbitMqHandler;
            RabbitMqOptions = rabbitMqOptions?.Value;
        }

        /// <summary>
        /// Creates a TO-DO command sending a RabbitMq message
        /// </summary>
        /// <param name="todoDescription">The description of the TO-DO command. It cannot be empty</param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/RabbitMq/createtodo")]
        public async Task<IActionResult> CreateTodo(string todoDescription)
        {
            Devon4NetLogger.Debug("Executing CreateTodo from controller RabbitMqController");

            if (RabbitMqOptions?.Hosts == null || RabbitMqOptions.Hosts.Count == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No RabbitMq instance set up");
            }

            if (string.IsNullOrEmpty(todoDescription))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a valid description for the TO-DO");
            }

            var todoCommand = new TodoCommand { Description = todoDescription};
            var published = await TodoRabbitMqHandler.Publish(todoCommand).ConfigureAwait(false);
            return Ok(published);
        }
    }
}
