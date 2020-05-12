using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.RabbitMQ.Handlers;
using Devon4Net.Infrastructure.RabbitMQ.Samples.Commads;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.RabbitMQ.Samples.Handllers
{
    public class UserSampleRabbitMqHandler : RabbitMqHandler<UserSampleCommand>
    {
        public UserSampleRabbitMqHandler(IServiceCollection services, IBus serviceBus, bool subscribeToChannel = false) : base(services, serviceBus, subscribeToChannel)
        {
        }

        public UserSampleRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, subscribeToChannel)
        {
        }

        public UserSampleRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        public UserSampleRabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false) : base(services, serviceBus, rabbitMqBackupService, rabbitMqBackupLiteDbService, subscribeToChannel)
        {
        }

        /// <summary>
        /// Sample class. Put  your async methods in logic
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override async Task<bool> HandleCommand(UserSampleCommand command)
        {
            Devon4NetLogger.Debug($"User {command.Name} {command.SurName} handled!!");
            return true;
        }


    }
}
