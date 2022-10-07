using Devon4Net.Infrastructure.Kafka.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public class KafkaHandler : IKafkaHandler
    {
        protected string HandlerId { get; }
        protected KafkaOptions KafkaOptions { get; }
        protected IServiceCollection Services { get; }


        public KafkaHandler(IServiceCollection services, KafkaOptions kafkaOptions, string handlerId)
        {
            KafkaOptions = kafkaOptions;
            Services = services;
            HandlerId = handlerId;
        }

        public TS GetInstance<TS>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<TS>();
        }
    }
}
