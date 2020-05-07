using System.Threading.Tasks;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using Devon4Net.Infrastructure.RabbitMQ.Domain.Entities;

namespace Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces
{
    public interface IRabbitMqBackupService
    {
        bool UseExternalDatabase { get; }
        Task<RabbitBackup> CreateMessageBackup(Command command, QueueActionsEnum action = QueueActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}