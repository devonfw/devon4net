using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Controllers
{
    /// <summary>
    /// TODOs controller
    /// </summary>
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class TodoController: ControllerBase
    {
        private readonly ITodoService _todoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="todoService"></param>
        public TodoController( ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Gets the entire list of TODOS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetTodo()
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            return Ok(await _todoService.GetTodo().ConfigureAwait(false));
        }

        /// <summary>
        /// Creates the object
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create(string todoDescription)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            var result = await _todoService.CreateTodo(todoDescription).ConfigureAwait(false);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Deletes the object provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(long todoId)
        {
            Devon4NetLogger.Debug("Executing GetTodo from controller TodoController");
            return Ok(await _todoService.DeleteTodoById(todoId).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the object provided the id
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpOptions]
        public async Task<ActionResult> ModifyTodo(long todoId)
        {
            Devon4NetLogger.Debug("Executing ModifyTodo from controller TodoController");
            return Ok(await _todoService.ModifyTodoById(todoId).ConfigureAwait(false));
        }
    }
}
