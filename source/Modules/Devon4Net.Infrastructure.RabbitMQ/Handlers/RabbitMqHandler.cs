using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.RabbitMQ.Handlers
{
    public abstract class RabbitMqHandler<T> where T : Command
    {
        private IBus ServiceBus { get; set; }
        private IRabbitMqBackupService RabbitMqBackupService { get; set; }
        private IRabbitMqBackupLiteDbService RabbitMqBackupLiteDbService { get; set; }
        private IServiceCollection Services { get; set; }

        public abstract Task<bool> HandleCommand(T command);

        protected RabbitMqHandler(IServiceCollection services, IBus serviceBus, bool subscribeToChannel = false)
        {
            BasicSetup(services, serviceBus, subscribeToChannel);
        }

        protected RabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribeToChannel = false)
        {
            BasicSetup(services, serviceBus, subscribeToChannel, rabbitMqBackupService);
        }

        protected RabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false)
        {
            BasicSetup(services, serviceBus, subscribeToChannel, null, rabbitMqBackupLiteDbService);
        }

        protected RabbitMqHandler(IServiceCollection services, IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false)
        {
            BasicSetup(services, serviceBus, subscribeToChannel, rabbitMqBackupService, rabbitMqBackupLiteDbService);
        }

        public async Task<bool> Publish(T command)
        {
            var status = QueueActions.SetUp;

            try
            {
                await ServiceBus.PubSub.PublishAsync(command).ContinueWith(task => status = PublishCommandTaskResult(command, task)).ConfigureAwait(false);
                await BackUpMessage(command, status).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await BackUpMessage(command, QueueActions.Error, false, string.Empty, $"{ex.Message} : {ex.InnerException}").ConfigureAwait(false);
                Devon4NetLogger.Error($"Error publishing message: {ex.Message}/{ex.InnerException}/{ex.StackTrace}");
                Devon4NetLogger.Error(ex);
            }

            return status == QueueActions.Sent;
        }

        public S GetInstance<S>() where S : class
        {
            return Services.BuildServiceProvider().GetService<S>();
        }

        private async Task<bool> BackupAndHandleCommand(T message)
        {
            var status = QueueActions.SetUp;
            var errorMessage = string.Empty;
            try
            {
                await HandleCommand(message).ContinueWith(task => status = HandleCommandTaskResult(message, task, out errorMessage)).ConfigureAwait(false);
                await BackUpMessage(message, status,false,string.Empty, errorMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await BackUpMessage(message, QueueActions.Error, false, string.Empty, $"{ex.Message} : {ex.InnerException}").ConfigureAwait(false);
                Devon4NetLogger.Error($"Error handling message: {ex.Message}/{ex.InnerException}");
                Devon4NetLogger.Error(ex);
                return false;
            }

            return status == QueueActions.Handled;
        }

        private static QueueActions HandleCommandTaskResult(T message, Task<bool> task, out string errorMessage)
        {
            var status = QueueActions.SetUp;
            errorMessage = string.Empty;

            if (task.IsCompleted)
            {
                status = QueueActions.Handled;
                Devon4NetLogger.Information($"Message {message.MessageType} with identifier '{message.InternalMessageIdentifier}' published");
            }

            if (!task.IsFaulted) return status;

            status = QueueActions.Error;
            errorMessage = $"Message {message.MessageType} with identifier '{message.InternalMessageIdentifier}' NOT published. {task.Exception?.Message} | {task.Exception?.InnerExceptions}";
            Devon4NetLogger.Error(errorMessage);
            Devon4NetLogger.Error(task.Exception);

            return status;
        }

        private static QueueActions PublishCommandTaskResult(T command, Task task)
        {
            var status = QueueActions.SetUp;

            if (task.IsCompleted)
            {
                status = QueueActions.Sent;
                Devon4NetLogger.Information($"Message {command.MessageType} with identifier '{command.InternalMessageIdentifier}' published");
            }

            if (!task.IsFaulted) return status;

            status = QueueActions.Error;
            Devon4NetLogger.Error($"Message {command.MessageType} with identifier '{command.InternalMessageIdentifier}' NOT published");
            Devon4NetLogger.Error(task.Exception);

            return status;
        }

        private void BasicSetup(IServiceCollection services, IBus serviceBus, bool subscribeToChannel, IRabbitMqBackupService rabbitMqBackupService = null, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService = null)
        {
            if (serviceBus == null)
            {
                Devon4NetLogger.Error("The RabbitMQ bus is not present. Please check your configuration");
                return;
            }

            ServiceBus = serviceBus;
            Services = services;

            RabbitMqBackupService = rabbitMqBackupService;
            RabbitMqBackupLiteDbService = rabbitMqBackupLiteDbService;

            if (subscribeToChannel)
            {
                ServiceBus.PubSub.SubscribeAsync<T>(typeof(T).Name, BackupAndHandleCommand);
            }
        }

        private async Task BackUpMessage(T command, QueueActions queueAction = QueueActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            try
            {
                RabbitMqBackupLiteDbService?.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData);

                if (RabbitMqBackupService?.UseExternalDatabase == true)
                {
                    await RabbitMqBackupService.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
