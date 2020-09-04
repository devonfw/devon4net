using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public interface IKafkaAdminHandler
    {
        Task<bool> DeleteTopic(string adminId, List<string> topicsName);
        Task<bool> CreateTopic(string adminId, string topicName, short replicationFactor = 1, int numPartitions = 1);
    }
}