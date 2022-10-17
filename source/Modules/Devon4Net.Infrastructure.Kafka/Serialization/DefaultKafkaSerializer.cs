using Confluent.Kafka;
using System.Text.Json;

namespace Devon4Net.Infrastructure.Kafka.Serialization
{
    public class DefaultKafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, typeof(T));
        }
    }
}
