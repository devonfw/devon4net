using System.Threading.Tasks;
using Devon4Net.Infrastructure.RabbitMQ.Commands;
using Devon4Net.Infrastructure.RabbitMQ.Common;
using LiteDB;

namespace Devon4Net.Infrastructure.RabbitMQ.Domain.ServiceInterfaces
{
    public interface IRabbitMqBackupLiteDbService
    {
        BsonValue CreateMessageBackup(Command command, QueueActionsEnum action = QueueActionsEnum.Sent, bool increaseRetryCounter = false, string additionalData = null, string errorData = null);
    }
}