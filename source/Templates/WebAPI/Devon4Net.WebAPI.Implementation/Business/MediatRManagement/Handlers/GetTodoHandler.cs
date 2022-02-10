using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Exceptions;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Queries;
using Devon4Net.WebAPI.Implementation.Business.TodoManagement.Service;

namespace Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Handlers
{
    public class GetTodoHandler : MediatrRequestHandler<GetTodoQuery, TodoResultDto>
    {
        private ITodoService TodoService { get; set; }

        public GetTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupService, mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        public GetTodoHandler(ITodoService todoService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

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
            if (TodoService == null)
            {
                throw new ArgumentException("The service 'TodoService' is not ready. Please check your dependency injection declaration for this service");
            }

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
