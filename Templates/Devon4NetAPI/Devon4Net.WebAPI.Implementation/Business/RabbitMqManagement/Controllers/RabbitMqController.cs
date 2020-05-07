using System.Linq;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Common.Options.RabbitMq;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Samples.Commads;
using Devon4Net.Infrastructure.RabbitMQ.Samples.Handllers;
using Devon4Net.WebAPI.Implementation.Business.UserManagement.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Devon4Net.WebAPI.Implementation.Business.RabbitMqManagement.Controllers
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
        private UserSampleRabbitMqHandler UserSampleRabbitHandler { get; set; }
        private RabbitMqOptions RabbitMqOptions { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="userSampleRabbitHandler">THe handler is injected via DI</param>
        /// <param name="rabbitMqOptions">The RabbitMq options to check if there is any instance set up</param>
        public RabbitMqController(UserSampleRabbitMqHandler userSampleRabbitHandler, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            UserSampleRabbitHandler = userSampleRabbitHandler;
            RabbitMqOptions = rabbitMqOptions?.Value;
        }

        /// <summary>
        /// Sends a message to the RabbitMq server queue
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="surname">Surname of the user</param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/v1/auth/sendrabbitmqmessage")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendRabbitMqMessage(string name, string surname)
        {
            Devon4NetLogger.Debug("Executing SendRabbitMqMessage from controller RabbitMqController");

            if (RabbitMqOptions?.Hosts == null || !RabbitMqOptions.Hosts.Any())
                return StatusCode(StatusCodes.Status500InternalServerError, "No RabbitMq instance set up");

            var userCommand = new UserSampleCommand {Name = name, SurName = surname};
            var published = await UserSampleRabbitHandler.Publish(userCommand).ConfigureAwait(false);
            return Ok(published);
        }
    }
}
