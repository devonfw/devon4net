namespace Devon4Net.Infrastructure.Kafka.Options
{
    public class KafkaOptions
    {
        public bool EnableKafka { get; set; }
        public List<AdministrationOptions> Administration { get; set; }
        public List<ProducerOptions> Producers { get; set; }
        public List<ConsumerOptions> Consumers { get; set; }
        public List<StreamOptions> Streams { get; set; }


    }
}
