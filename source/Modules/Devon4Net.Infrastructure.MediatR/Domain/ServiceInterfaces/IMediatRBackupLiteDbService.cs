using Devon4Net.Infrastructure.MediatR.Common;
using LiteDB;

namespace Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces
{
    public interface IMediatRBackupLiteDbService
    {
        BsonValue CreateMessageBackup<T>(ActionBase<T> command, MediatrActions action = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class;
        BsonValue CreateResponseMessageBackup(object command, MediatrActions action = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}