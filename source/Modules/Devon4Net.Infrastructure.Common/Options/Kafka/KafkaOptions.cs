﻿namespace Devon4Net.Infrastructure.Common.Options.Kafka
{
    public class KafkaOptions
    {
        public bool EnableKafka { get; set; }
        public List<Administration> Administration { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Consumer> Consumers { get; set; }
    }
}
