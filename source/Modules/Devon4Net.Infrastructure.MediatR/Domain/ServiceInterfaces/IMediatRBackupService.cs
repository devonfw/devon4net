using Devon4Net.Infrastructure.MediatR.Common;
using Devon4Net.Infrastructure.MediatR.Domain.Entities;

namespace Devon4Net.Infrastructure.MediatR.Domain.ServiceInterfaces
{
    public interface IMediatRBackupService
    {
        bool UseExternalDatabase { get; }
        Task<MediatRBackup> CreateMessageBackup<T>(ActionBase<T> command, MediatrActions action = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null) where T : class;
        Task<MediatRBackup> CreateResponseMessageBackup(object command, MediatrActions action = MediatrActions.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}