using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Devon4Net.Infrastructure.Kafka.Exceptions;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Logger.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Kafka.Handlers.Administration
{
    public class KafkaAdministrationHandler : IKafkaAdministrationHandler
    {
        private readonly KafkaOptions KafkaOptions;

        public KafkaAdministrationHandler(IOptions<KafkaOptions> kafkaOptions) 
        {
            KafkaOptions = kafkaOptions?.Value;
        }
        
        public async Task<bool> CreateTopic(string adminId, string topicName, short replicationFactor = 1, int numPartitions = 1)
        {
            using var adminClient = GetAdminClientBuilder(adminId);
            try
            {
                await adminClient.CreateTopicsAsync(new[] { new TopicSpecification { Name = topicName, ReplicationFactor = replicationFactor, NumPartitions = numPartitions } }).ConfigureAwait(false);
                return true;
            }
            catch (CreateTopicsException ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic {ex.Results[0].Topic}: {ex.Results[0].Error.Reason}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<bool> DeleteTopic(string adminId, List<string> topicsName)
        {
            using var adminClient = GetAdminClientBuilder(adminId);
            try
            {
                await adminClient.DeleteTopicsAsync(topicsName).ConfigureAwait(false);
                return true;
            }
            catch (CreateTopicsException ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic {ex.Results[0].Topic}: {ex.Results[0].Error.Reason}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        #region AdministrationConfiguration
        private IAdminClient GetAdminClientBuilder(string adminId)
        {
            var adminOptions = GetAdminOptions(adminId);
            var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = adminOptions.Servers }).Build();
            return adminClient;
        }

        private AdministrationOptions GetAdminOptions(string adminId)
        {
            if (string.IsNullOrEmpty(adminId))
            {
                throw new AdminNotFoundException("The adminId param can not be null or empty");
            }

            var adminOptions = KafkaOptions.Administration.Find(p => p.AdminId == adminId);

            if (adminOptions == null)
            {
                throw new AdminNotFoundException($"Could not find admin configuration with ConsumerId {adminId}");
            }

            return adminOptions;
        }

        #endregion
    }
}
