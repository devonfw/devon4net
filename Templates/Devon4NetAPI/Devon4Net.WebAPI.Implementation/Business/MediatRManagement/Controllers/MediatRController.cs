using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Infrastructure.MediatR.Samples.Model;
using Devon4Net.Infrastructure.MediatR.Samples.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Controllers
{
    /// <summary>
    /// Controller sample to implement the mediator pattern
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class MediatRController : ControllerBase
    {
        private IMediatRHandler MediatRHandler { get; set; }

        /// <summary>
        /// Mediator sample controller
        /// </summary>
        /// <param name="mediatRHandler"></param>
        public MediatRController(IMediatRHandler mediatRHandler)
        {
            MediatRHandler = mediatRHandler;
        }

        /// <summary>
        /// Gets a User via a command
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetCustomerAsync(Guid id)
        {
            var query = new GetUserQuery(id);
            return Ok(await MediatRHandler.QueryAsync(query));
        }
    }
}
