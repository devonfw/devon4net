using Devon4Net.Infrastructure.MediatR.Command;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Query;
using LiteDB;

namespace Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces
{
    public interface IMediatRBackupLiteDbService
    {
        BsonValue CreateMessageBackup<T>(ActionBase<T> command, MediatRActionsEnum action = MediatRActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class;
        BsonValue CreateResponseMessageBackup(object command, MediatRActionsEnum action = MediatRActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}