using System.Threading.Tasks;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Log;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public class KafkaProducerHandler<T, TV> : IKafkaProducerHandler<T, TV> where T : class where TV : class
    {
        protected IServiceCollection Services { get; set; }
        private IKakfkaHandler KafkaHandler { get; set; }
        private string ProducerId { get; set; }

        public KafkaProducerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string producerId)
        {
            Services = services;
            KafkaHandler = kafkaHandler;
            ProducerId = producerId;
        }

        public Task<DeliveryResult<T, TV>> SendMessage(T key, TV value) 
        {
            var result = KafkaHandler.DeliverMessage(key, value, ProducerId);
            var date = result.Result.Timestamp.UtcDateTime;
            Devon4NetLogger.Information($"Message delivered. Key: {result.Result.Key} | Value : {result.Result.Value} | Topic: {result.Result.Topic} | UTC TimeStamp : {date.ToShortDateString()}-{date.ToLongTimeString()} | Status: {result.Result.Status}");
            return result;
        }

        public TS GetInstance<TS>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<TS>();
        }
    }
}