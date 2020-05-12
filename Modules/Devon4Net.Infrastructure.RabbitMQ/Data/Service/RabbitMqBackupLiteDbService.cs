using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Extensions;
using Devon4Net.Infrastructure.Extensions.Helpers;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Entities;
using Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces;
using LiteDB;

namespace Devon4Net.Infrastructure.RabbitMQ.Data.Service
{
    public class RabbitMqBackupLiteDbService : IRabbitMqBackupLiteDbService
    {
        private LiteDb.Repository.IRepository<RabbitBackup> RabbitMqBackupLiteDbRepository { get; set; }
        private IJsonHelper JsonHelper { get; set; }

        public RabbitMqBackupLiteDbService(LiteDb.Repository.IRepository<RabbitBackup> rabbitMqBackupLiteDbRepository, IJsonHelper jsonHelper)
        {
            RabbitMqBackupLiteDbRepository = rabbitMqBackupLiteDbRepository;
            JsonHelper = jsonHelper;
        }
        public async Task<BsonValue> CreateMessageBackup(Command command, QueueActionsEnum action = QueueActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) 
        {
            try
            {
                if (command?.InternalMessageIdentifier == null || command.InternalMessageIdentifier.IsNullOrEmptyGuid())
                {
                    throw new ArgumentException($"The provided command  and the command identifier cannot be null ");
                }

                var backUp = new RabbitBackup
                {
                    Id = Guid.NewGuid(),
                    InternalMessageIdentifier = command.InternalMessageIdentifier,
                    Retries = increaseRetryCounter ? 1 : 0,
                    AdditionalData = string.IsNullOrEmpty(additionalData) ? string.Empty : additionalData,
                    IsError = false,
                    MessageContent = GetSerializedContent(command), //System.Text.Json.JsonSerializer.Serialize(command),
                    MessageType = command.MessageType,
                    TimeStampUTC = command.Timestamp.ToUniversalTime(),
                    Action = action.ToString(),
                    Error = string.IsNullOrEmpty(errorData) ? string.Empty : errorData
                };

                var result = RabbitMqBackupLiteDbRepository.Create(backUp);
                
                return result;
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"Error storing data with LiteDb: {ex.Message} {ex.InnerException}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private string GetSerializedContent(Command command)
        {
            var typedCommand = CovertObjectFromClassName(command, command.GetType().FullName);
            var serializedContent = JsonHelper.Serialize(typedCommand);
            return serializedContent;
        }

        private object CovertObjectFromClassName(object objectInstance, string fullClassName)
        {
            if (string.IsNullOrEmpty(fullClassName)) throw new ArgumentException("The class name cannot be null");
            var classNameTarget = Type.GetType(fullClassName);
            if (classNameTarget == null) throw new ArgumentException("Cannot get the type of the provided class name");
            return Convert.ChangeType(objectInstance, classNameTarget);
        }
    }
}
