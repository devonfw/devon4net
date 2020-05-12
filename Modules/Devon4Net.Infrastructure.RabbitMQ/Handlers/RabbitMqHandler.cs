using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
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
            var status = QueueActionsEnum.SetUp;

            try
            {
                status = QueueActionsEnum.SetUp;

                await ServiceBus.PublishAsync(command).ContinueWith(async task =>
                {
                    if (task.IsCompleted)
                    {
                        status = QueueActionsEnum.Sent;
                        Devon4NetLogger.Information($"Message {command.MessageType} with identifier '{command.InternalMessageIdentifier}' published");
                    }

                    if (task.IsFaulted)
                    {
                        status = QueueActionsEnum.Error;
                        Devon4NetLogger.Error($"Message {command.MessageType} with identifier '{command.InternalMessageIdentifier}' NOT published");
                        Devon4NetLogger.Error(task.Exception);
                    }

                }).ConfigureAwait(false);

                await BackUpMessage(command, status).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await BackUpMessage(command, QueueActionsEnum.Error, false, string.Empty, $"{ex?.Message} : {ex?.InnerException}").ConfigureAwait(false);
                Devon4NetLogger.Error($"Error publishing message: {ex.Message}/{ex.InnerException}");
                Devon4NetLogger.Error(ex);
            }

            return status == QueueActionsEnum.Sent;
        }

        public T GetInstance<T>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<T>();
        }

        private async Task<bool> BackupAndHandleCommand(T message)
        {
            var status = QueueActionsEnum.SetUp;
            var errorMessage = string.Empty;
            try
            {
                await HandleCommand(message).ContinueWith(async task =>
                {
                    if (task.IsCompleted)
                    {
                        status = QueueActionsEnum.Handled;
                        Devon4NetLogger.Information($"Message {message.MessageType} with identifier '{message.InternalMessageIdentifier}' published");
                    }

                    if (task.IsFaulted)
                    {
                        status = QueueActionsEnum.Error;
                        errorMessage = $"Message {message.MessageType} with identifier '{message.InternalMessageIdentifier}' NOT published. {task.Exception?.Message} | {task.Exception?.InnerExceptions}";
                        Devon4NetLogger.Error(errorMessage);
                        Devon4NetLogger.Error(task.Exception);
                    }
                }).ConfigureAwait(false);

                await BackUpMessage(message, status,false,string.Empty, errorMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await BackUpMessage(message, QueueActionsEnum.Error, false, string.Empty,
                    $"{ex.Message} : {ex.InnerException}").ConfigureAwait(false);
                Devon4NetLogger.Error($"Error handling message: {ex.Message}/{ex.InnerException}");
                Devon4NetLogger.Error(ex);
                return false;
            }

            return status == QueueActionsEnum.Handled;
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
                ServiceBus.SubscribeAsync<T>(typeof(T).Name, BackupAndHandleCommand);
            }
        }

        private async Task BackUpMessage(T command, QueueActionsEnum queueAction = QueueActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            try
            {
                RabbitMqBackupLiteDbService?.CreateMessageBackup(command, queueAction, increaseRetryCounter, additionalData, errorData);

                if (RabbitMqBackupService != null && RabbitMqBackupService.UseExternalDatabase)
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

        public abstract Task<bool> HandleCommand(T command);
    }
}
