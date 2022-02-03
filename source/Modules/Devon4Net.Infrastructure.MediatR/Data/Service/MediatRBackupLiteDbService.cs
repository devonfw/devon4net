using Devon4Net.Infrastructure.Extensions;
using Devon4Net.Infrastructure.LiteDb.Repository;
using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.Entities;
using Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces;
using LiteDB;
using IJsonHelper = Devon4Net.Infrastructure.Extensions.Helpers.IJsonHelper;

namespace Devon4Net.Infrastructure.MediatR.Data.Service
{
    public class MediatRBackupLiteDbService : IMediatRBackupLiteDbService
    {
        private IRepository<MediatRBackup> MediatRBackupLiteDbRepository { get; }
        private IJsonHelper JsonHelper { get; }

        public MediatRBackupLiteDbService(IRepository<MediatRBackup> mediatRBackupLiteDbRepository, IJsonHelper jsonHelper)
        {
            MediatRBackupLiteDbRepository = mediatRBackupLiteDbRepository;
            JsonHelper = jsonHelper;
        }

        public BsonValue CreateMessageBackup<T>(ActionBase<T> command, MediatrActions action = MediatrActions.Sent,
            bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class
        {
            try
            {
                if (command?.InternalMessageIdentifier == null || command.InternalMessageIdentifier.IsNullOrEmptyGuid())
                {
                    throw new ArgumentException("The provided command  and the command identifier cannot be null");
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

        public BsonValue CreateResponseMessageBackup(object command, MediatrActions action = MediatrActions.Sent,
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
            var typedCommand = Convert.ChangeType(command, command.GetType());
            var serializedContent = JsonHelper.Serialize(typedCommand, false);
            return serializedContent;
        }
    }
}
