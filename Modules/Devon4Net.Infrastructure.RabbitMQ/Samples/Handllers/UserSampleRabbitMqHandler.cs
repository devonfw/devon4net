using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using Devon4Net.Infrastructure.RabbitMQ.Handlers;
using Devon4Net.Infrastructure.RabbitMQ.Samples.Commads;
using EasyNetQ;

namespace Devon4Net.Infrastructure.RabbitMQ.Samples.Handllers
{
    public class UserSampleRabbitMqHandler : RabbitMqHandler<UserSampleCommand>
    {
        public UserSampleRabbitMqHandler(IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribe) : base(serviceBus, rabbitMqBackupService, subscribe)
        {
        }

        public UserSampleRabbitMqHandler(IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribe) : base(serviceBus, rabbitMqBackupService, rabbitMqBackupLiteDbService, subscribe)
        {
        }

        public UserSampleRabbitMqHandler(IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService) : base(serviceBus, rabbitMqBackupService, rabbitMqBackupLiteDbService, false)
        {
        }

        public UserSampleRabbitMqHandler(IBus serviceBus,  IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribe) : base(serviceBus, rabbitMqBackupLiteDbService, subscribe)
        {
        }

        public UserSampleRabbitMqHandler(IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService) : base(serviceBus, rabbitMqBackupLiteDbService, false)
        {
        }


        /// <summary>
        /// Sample class. Put  your async methods in logic
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
#pragma warning disable 1998 
        public override async Task HandleCommand(UserSampleCommand command)
#pragma warning restore 1998
        {
            Devon4NetLogger.Debug($"User {command.Name} {command.SurName} handled!!");
        }
    }
}
