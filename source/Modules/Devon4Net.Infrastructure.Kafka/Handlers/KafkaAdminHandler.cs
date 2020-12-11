using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public class KafkaAdminHandler : IKafkaAdminHandler
    {
        private IKakfkaHandler KafkaHandler { get; set; }
        protected IServiceCollection Services { get; set; }


        public KafkaAdminHandler(IServiceCollection services, IKakfkaHandler kafkaHandler)
        {
            Services = services;
            KafkaHandler = kafkaHandler;
        }

        public async Task<bool> CreateTopic(string adminId, string topicName, short replicationFactor = 1, int numPartitions = 1)
        {
            if (string.IsNullOrEmpty(topicName))
            {
                throw new ArgumentException("The topic name can not be null or empty");
            }

            try
            {
                return await KafkaHandler.CreateTopic(adminId, topicName, replicationFactor, numPartitions);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic {topicName}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }

        public async Task<bool> DeleteTopic(string adminId, List<string> topicsName)
        {
            if (topicsName== null || !topicsName.Any())
            {
                throw new ArgumentException("The topics list can not be null or empty");
            }

            try
            {
                return await KafkaHandler.DeleteTopic(adminId, topicsName);
            }
            catch (Exception ex)
            {
                Devon4NetLogger.Error($"An error occured creating topic list {string.Join(",", topicsName)}");
                Devon4NetLogger.Error(ex);
                throw;
            }
        }
    }
}
