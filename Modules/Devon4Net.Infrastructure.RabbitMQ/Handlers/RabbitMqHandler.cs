using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using EasyNetQ;

namespace Devon4Net.Infrastructure.RabbitMQ.Handlers
{
    public abstract class RabbitMqHandler<T> where T : Command
    {
        private IBus ServiceBus { get; set; }
        private IRabbitMqBackupService RabbitMqBackupService { get; set; }
        private IRabbitMqBackupLiteDbService RabbitMqBackupLiteDbService { get; set; }

        protected RabbitMqHandler(IBus serviceBus, bool subscribeToChannel = false)
        {
            BasicSetup(serviceBus, subscribeToChannel);
        }

        protected RabbitMqHandler(IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, bool subscribeToChannel = false)
        {
            BasicSetup(serviceBus, subscribeToChannel, rabbitMqBackupService);
        }

        protected RabbitMqHandler(IBus serviceBus, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false)
        {
            BasicSetup(serviceBus, subscribeToChannel, null, rabbitMqBackupLiteDbService);
        }

        protected RabbitMqHandler(IBus serviceBus, IRabbitMqBackupService rabbitMqBackupService, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService, bool subscribeToChannel = false)
        {
            BasicSetup(serviceBus, subscribeToChannel, rabbitMqBackupService, rabbitMqBackupLiteDbService);
        }

        public async Task<bool> Publish(T command)
        {
            var status = QueueActionsEnum.SetUp;

            try
            {
                await ServiceBus.PublishAsync(command).ContinueWith(async task =>
                {
                        status = QueueActionsEnum.Sent;

                        if (task.IsCompleted)
                        {
                            status = QueueActionsEnum.Sent;
                            Devon4NetLogger.Information($"Message {command.MessageType} with identifier '{command.InternalMessageIdentifier}' published");
                        }

                        if (task.IsFaulted)
                        {
                            status = QueueActionsEnum.Error;
                            Devon4NetLogger.Error(task.Exception);
                        }

                        await BackUpMessage(command, status, false, string.Empty, $"{task.Exception?.Message} : {task.Exception?.InnerExceptions}").ConfigureAwait(false);
                        return status == QueueActionsEnum.Sent;

                }).ConfigureAwait(false);

                return false;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Error publishing message: {ex.Message}/{ex.InnerException}");
                Devon4NetLogger.Error(ex);
            }

            return status == QueueActionsEnum.Sent;
        }

        private void BasicSetup(IBus serviceBus, bool subscribeToChannel, IRabbitMqBackupService rabbitMqBackupService = null, IRabbitMqBackupLiteDbService rabbitMqBackupLiteDbService = null)
        {
            ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus), "The RabbitMQ bus is not present. Please check your configuration");
            RabbitMqBackupService = rabbitMqBackupService;
            RabbitMqBackupLiteDbService = rabbitMqBackupLiteDbService;

            if (subscribeToChannel)
            {
                ServiceBus.SubscribeAsync<T>(typeof(T).Name, BackupAndHandleCommand);
            }
        }

        private Task<Task> BackupAndHandleCommand(T message)
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    await HandleCommand(message);
                }
                catch (Exception ex)
                {
                    await BackUpMessage(message, QueueActionsEnum.Error, false, string.Empty, $"{ex.Message} : {ex.InnerException}").ConfigureAwait(false);
                    Devon4NetLogger.Error($"Error handling message: {ex.Message}/{ex.InnerException}");
                    Devon4NetLogger.Error(ex);
                }

            }).ContinueWith(async task =>
            {
                QueueActionsEnum status;

                if (task.IsCompleted && !task.IsFaulted)
                {
                    status = QueueActionsEnum.Handled;
                }
                else
                {
                    status = QueueActionsEnum.Error;
                    Devon4NetLogger.Error("Message processing exception - look in the default error queue (broker)");
                }

                await BackUpMessage(message, status, false, string.Empty, $"{task.Exception?.Message} : {task.Exception?.InnerExceptions}").ConfigureAwait(false);
            });
        }

        private async Task BackUpMessage(T command, QueueActionsEnum queueAction = QueueActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            RabbitMqBackupLiteDbService?.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData);

            if (RabbitMqBackupService != null && RabbitMqBackupService.UseExternalDatabase)
            {
                await RabbitMqBackupService.CreateMessageBackup(command, queueAction,increaseRetryCounter,additionalData,errorData).ConfigureAwait(false);
            }
        }

        public abstract Task HandleCommand(T command);
    }
}
