using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Exceptions;
using Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Queries;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service;

namespace Devon4Net.Application.WebAPI.Implementation.Business.MediatRManagement.Handlers
{
    /// <summary>
    /// GetTodoHandler
    /// </summary>
    public class GetTodoHandler : MediatrRequestHandler<GetTodoQuery, TodoResultDto>
    {
        private ITodoService TodoService { get; set; }

        /// <summary>
        /// GetTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupService"></param>
        /// <param name="mediatRBackupLiteDbService"></param>
        public GetTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupService, mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        /// <summary>
        /// GetTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupLiteDbService"></param>
        public GetTodoHandler(ITodoService todoService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        /// <summary>
        /// GetTodoHandler
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="mediatRBackupService"></param>
        public GetTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService) : base(mediatRBackupService)
        {
            Setup(todoService);
        }

        private void Setup(ITodoService todoService)
        {
            TodoService = todoService;
        }


        /// <summary>
        /// Handles the received message to perform the query
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<TodoResultDto> HandleAction(GetTodoQuery request, CancellationToken cancellationToken)
        {
            var result = await TodoService.GetTodoById(request.TodoId).ConfigureAwait(false);

            if (result == null)
            {
                throw new TodoNotFoundException("The TODO item was not found");
            }

            return new TodoResultDto
            {
                Id = result.Id,
                Description = result.Description,
                Done = result.Done
            };
        }
    }
}
