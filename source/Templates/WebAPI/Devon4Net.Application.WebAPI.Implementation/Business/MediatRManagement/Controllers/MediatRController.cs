using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Commands;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Controllers
{
    /// <summary>
    /// Controller sample to implement the mediator pattern
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class MediatRController : ControllerBase
    {
        private IMediatRHandler MediatRHandler { get; }

        /// <summary>
        /// Mediator sample controller
        /// </summary>
        /// <param name="mediatRHandler"></param>
        public MediatRController(IMediatRHandler mediatRHandler)
        {
            MediatRHandler = mediatRHandler;
        }

        /// <summary>
        /// Gets a TO-DO item given the Id via CQRS pattern via a MediatR query
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpOptions]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TodoResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/v1/MediatR/gettodobyid")]
        public async Task<IActionResult> GetTodoById(long todoId)
        {
            Devon4NetLogger.Debug("Executing GetTodoById from controller MediatRController");
            var query = new GetTodoQuery(todoId);
            return Ok(await MediatRHandler.QueryAsync(query).ConfigureAwait(false));
        }

        /// <summary>
        /// Creates a TO-DO item sending a MediatR via a message command
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
        [Route("/v1/MediatR/createtodo")]
        public async Task<IActionResult> CreateTodo(string todoDescription)
        {
            if (string.IsNullOrEmpty(todoDescription))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a valid description for the TO-DO");
            }

            Devon4NetLogger.Debug("Executing CreateTodo from controller MediatRController");
            var command = new CreateTodoCommand(todoDescription);
            return Ok(await MediatRHandler.QueryAsync(command).ConfigureAwait(false));
        }
    }
}
