using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public abstract class KafkaConsumerHandler
    {
        public abstract Task HandleCommand<T, TV>(ConsumeResult<T, TV> consumeResult);
        private IKakfkaHandler KafkaHandler { get; set; }
        private IServiceCollection Services { get; set; }

        private string ConsumerId { get; set; }

        protected KafkaConsumerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string consumerId)
        {
            Services = services;
            KafkaHandler = kafkaHandler;
            ConsumerId = consumerId;
        }

        /// <summary>
        /// If commit is set to true, the Commit method sends a "commit offsets" request to the Kafka cluster and synchronously waits for the response.
        /// This is very slow compared to the rate at which the consumer is capable of consuming messages.
        /// A high performance application will typically commit offsets relatively infrequently and be designed handle duplicate messages in the event of failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="consumerId"></param>
        /// <param name="commit"></param>
        /// <param name="commitPeriod"></param>
        public async Task Consume<T, TV>(bool commit = false, int commitPeriod = 5) where T : class where TV : class
        {
            var cancellationToken = new CancellationTokenSource();

            using var consumer = KafkaHandler.GetConsumerBuilder<T, TV>(ConsumerId);
            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken.Token);
                        await HandleCommand(consumeResult);
                        if (consumeResult.IsPartitionEOF)
                        {
                            Console.WriteLine(
                                $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (!commit) return;

                        if (consumeResult.Offset % commitPeriod != 0) continue;
                        // The Commit method sends a "commit offsets" request to the Kafka
                        // cluster and synchronously waits for the response. This is very
                        // slow compared to the rate at which the consumer is capable of
                        // consuming messages. A high performance application will typically
                        // commit offsets relatively infrequently and be designed handle
                        // duplicate messages in the event of failure.
                        try
                        {
                            consumer.Commit(consumeResult);
                        }
                        catch (KafkaException e)
                        {
                            Console.WriteLine($"Commit error: {e.Error.Reason}");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Closing consumer.");
                consumer.Close();
            }
        }
    }
}
