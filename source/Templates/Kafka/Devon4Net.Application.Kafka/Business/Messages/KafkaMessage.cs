using System;

namespace Devon4Net.Application.Kafka.Business.Messages
{
    public class KafkaMessage
    {
        public string MessageId { get; set; }
        public string MessageContent { get; set; }

        public KafkaMessage(string messageContent = "")
        {
            MessageId = Guid.NewGuid().ToString();
        }
    }
}
