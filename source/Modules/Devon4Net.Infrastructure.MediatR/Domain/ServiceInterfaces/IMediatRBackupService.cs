using System.Threading.Tasks;
using Devon4Net.Infrastructure.MediatR.Command;
using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.Entities;
using Devon4Net.Infrastructure.MediatR.Query;
using LiteDB;

namespace Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces
{
    public interface IMediatRBackupService
    {
        bool UseExternalDatabase { get; }
        Task<MediatRBackup> CreateMessageBackup<T>(ActionBase<T> command, MediatRActionsEnum action = MediatRActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class;
        Task<MediatRBackup> CreateResponseMessageBackup(object command, MediatRActionsEnum action = MediatRActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}