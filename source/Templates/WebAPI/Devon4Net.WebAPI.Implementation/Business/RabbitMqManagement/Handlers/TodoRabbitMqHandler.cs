using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.RabbitMQ.Handlers;
using Devon4Net.WebAPI.Implementation.Business.RabbitMqManagement.Commands;
using Devon4Net.WebAPI.Implementation.Business.TodoManagement.Service;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.WebAPI.Implementation.Business.RabbitMqManagement.Handlers
{
    public class TodoRabbitMqHandler: RabbitMqHandler<TodoCommand>
    {
        private ITodoService TodoService { get; set; }

        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, bool subscribeToChannel = false) : base(services, serviceBus, subscribeToChannel)
        {
        }

        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, subscribeToChannel)
        {
        }

        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        public override async Task<bool> HandleCommand(TodoCommand command)
        {
            TodoService = GetInstance<ITodoService>();
            
            if (TodoService == null)
            {
                throw new ArgumentException("The service 'TodoService' is not ready. Please check your dependency injection declaration for this service");
            }

            var result = await TodoService.CreateTodo(command.Description).ConfigureAwait(false);
            return result!=null;
        }


    }
}
