using System;
using Devon4Net.Infrastructure.Extensions;
using Devon4Net.Infrastructure.LiteDb.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.Entities;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using LiteDB;
using IJsonHelper = Devon4Net.Infrastructure.Extensions.Helpers.IJsonHelper;

namespace Devon4Net.Infrastructure.MediatR.Data.Service
{
    public class MediatRBackupLiteDbService : IMediatRBackupLiteDbService
    {
        private IRepository<MediatRBackup> MediatRBackupLiteDbRepository { get; set; }
        private IJsonHelper JsonHelper { get; set; }

        public MediatRBackupLiteDbService(IRepository<MediatRBackup> mediatRBackupLiteDbRepository, IJsonHelper jsonHelper)
        {
            MediatRBackupLiteDbRepository = mediatRBackupLiteDbRepository;
            JsonHelper = jsonHelper;
        }

        public BsonValue CreateMessageBackup<T>(ActionBase<T> command, MediatRActionsEnum action = MediatRActionsEnum.Sent,
            bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class
        {
            try
            {
                if (command?.InternalMessageIdentifier == null || command.InternalMessageIdentifier.IsNullOrEmptyGuid())
                {
                    throw new ArgumentException($"The provided command  and the command identifier cannot be null ");
                }

                var backUp = new MediatRBackup
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

                return MediatRBackupLiteDbRepository.Create(backUp);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public BsonValue CreateResponseMessageBackup(object command, MediatRActionsEnum action = MediatRActionsEnum.Sent,
            bool increaseRetryCounter = false, string additionalData = null, string errorData = null)
        {
            try
            {
                var backUp = new MediatRBackup
                {
                    Id = Guid.NewGuid(),
                    Retries = increaseRetryCounter ? 1 : 0,
                    AdditionalData = string.IsNullOrEmpty(additionalData) ? string.Empty : additionalData,
                    IsError = false,
                    MessageContent = GetSerializedContent(command), //System.Text.Json.JsonSerializer.Serialize(command),
                    TimeStampUTC = DateTime.UtcNow.ToUniversalTime(),
                    Action = action.ToString(),
                    Error = string.IsNullOrEmpty(errorData) ? string.Empty : errorData
                };

                return MediatRBackupLiteDbRepository.Create(backUp);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        private string GetSerializedContent(object command)
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
