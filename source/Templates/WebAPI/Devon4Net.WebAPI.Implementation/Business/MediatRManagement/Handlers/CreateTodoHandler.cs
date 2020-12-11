using System;
using System.Threading;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.MediatR.Handler;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Commands;
using Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.TodoManagement.Service;

namespace Devon4Net.WebAPI.Implementation.Business.MediatRManagement.Handlers
{
    public class CreateTodoHandler : MediatrRequestHandler<CreateTodoCommand, TodoResultDto>
    {
        private ITodoService TodoService { get; set; }

        public CreateTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupService, mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        public CreateTodoHandler(ITodoService todoService, IMediatRBackupLiteDbService mediatRBackupLiteDbService) : base(mediatRBackupLiteDbService)
        {
            Setup(todoService);
        }

        public CreateTodoHandler(ITodoService todoService, IMediatRBackupService mediatRBackupService) : base(mediatRBackupService)
        {
            Setup(todoService);
        }

        private void Setup(ITodoService todoService)
        {
            TodoService = todoService;
        }


        public override async Task<TodoResultDto> HandleAction(CreateTodoCommand request, CancellationToken cancellationToken)
        {

            if (TodoService == null)
            {
                throw new ArgumentException("The service 'TodoService' is not ready. Please check your dependency injection declaration for this service");
            }

            var result = await TodoService.CreateTodo(request.Description);

            return new TodoResultDto
            {
                Id = result.Id,
                Done = result.Done,
                Description = result.Description
            };

        }
    }
}
