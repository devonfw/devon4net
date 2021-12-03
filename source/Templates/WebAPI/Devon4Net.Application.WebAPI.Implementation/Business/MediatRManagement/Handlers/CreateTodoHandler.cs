using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Commands;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service;

namespace Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Handlers
{
    /// <summary>
    /// CreateTodoHandler
    /// </summary>
    public class CreateTodoHandler : MediatrRequestHandler<CreateTodoCommand, TodoResultDto>
    {
        private ITodoService TodoService { get; set; }

        /// <summary>
        /// CreateTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupService"></param>
        /// <param name="mediatRBackupLiteDbService"></param>
        public CreateTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupService, mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        /// <summary>
        /// CreateTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupLiteDbService"></param>
        public CreateTodoHandler(ITodoService todoService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        /// <summary>
        /// CreateTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupService"></param>
        public CreateTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService) : base(mediatRBackupService)
        {
            Setup(todoService);
        }

        private void Setup(ITodoService todoService)
        {
            TodoService = todoService;
        }


        /// <summary>
        /// HandleAction
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<TodoResultDto> HandleAction(CreateTodoCommand request, CancellationToken cancellationToken)
        {

            var result = await TodoService.CreateTodo(request.Description).ConfigureAwait(false);

            return new TodoResultDto
            {
                Id = result.Id,
                Done = result.Done,
                Description = result.Description
            };

        }
    }
}
