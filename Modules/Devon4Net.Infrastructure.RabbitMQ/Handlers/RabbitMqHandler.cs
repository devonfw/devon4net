using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using EasyNetQ;

namespace Devon4Net.Infrastructure.RabbitMQ.Handlers
{
    public abstract class RabbitMqHandler<T> where T : Command
    {
        private protected IBus ServiceBus { get; set; }

        protected RabbitMqHandler(IBus serviceBus, bool subscribeToChannel = false)
        {
            ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus), "The RabbitMQ bus is not present. Please check your configuration");
            if (subscribeToChannel) ServiceBus.SubscribeAsync<T>(typeof(T).Name, HandleCommandBd);
        }

        private async Task HandleCommandBd(T command)
        {
            // todo: Set db operations
            await HandleCommand(command);
        }

        public async Task Publish(T command)
        {
            await ServiceBus.PublishAsync(command).ContinueWith(task =>
            {
                // todo: set db operations
                // this only checks that the task finished
                // IsCompleted will be true even for tasks in a faulted state
                // we use if (task.IsCompleted && !task.IsFaulted) to check for success
                if (task.IsCompleted)
                {
                    Devon4NetLogger.Information($"Message {command.MessageType} with id '{command.InternalMessageIdentifier}' published");
                }
                if (task.IsFaulted)
                {
                    Devon4NetLogger.Error(task.Exception);
                }
            });
        }

        public abstract Task HandleCommand(T command);
    }
}
