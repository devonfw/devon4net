using Confluent.Kafka;
using System.Text.Json;

namespace Devon4Net.Infrastructure.Kafka.Serialization
{
    public class DefaultKafkaDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return (T) JsonSerializer.Deserialize(new MemoryStream(data.ToArray()), typeof(T));
        }
    }
}
