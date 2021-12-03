using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.RabbitMQ.Handlers;
using Devon4Net.Application.WebAPI.Implementation.Business.RabbitMqManagement.Commands;
using Devon4Net.Application.WebAPI.Implementation.Business.TodoManagement.Service;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Implementation.Business.RabbitMqManagement.Handlers
{
    /// <summary>
    /// TodoRabbitMqHandler 
    /// </summary>
    public class TodoRabbitMqHandler: RabbitMqHandler<TodoCommand>
    {
        private ITodoService TodoService { get; set; }

        /// <summary>
        /// TodoRabbitMqHandler constructor to ensure the DI needs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBus"></param>
        /// <param name="subscribeToChannel"></param>
        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, bool subscribeToChannel = false) : base(services, serviceBus, subscribeToChannel)
        {
        }

        /// <summary>
        /// TodoRabbitMqHandler constructor to ensure the DI needs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBus"></param>
        /// <param name="rabbitMqBackupService"></param>
        /// <param name="subscribeToChannel"></param>
        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, subscribeToChannel)
        {
        }

        /// <summary>
        /// TodoRabbitMqHandler constructor to ensure the DI needs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBus"></param>
        /// <param name="rabbitMqBackupLiteDbService"></param>
        /// <param name="subscribeToChannel"></param>
        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        /// <summary>
        /// TodoRabbitMqHandler constructor to ensure the DI needs
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBus"></param>
        /// <param name="rabbitMqBackupService"></param>
        /// <param name="rabbitMqBackupLiteDbService"></param>
        /// <param name="subscribeToChannel"></param>
        public TodoRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        /// <summary>
        /// TodoRabbitMqHandler handler command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override async Task<bool> HandleCommand(TodoCommand command)
        {
            TodoService = GetInstance<ITodoService>();

            var result = await TodoService.CreateTodo(command.Description).ConfigureAwait(false);
            return result!=null;
        }
    }
}
