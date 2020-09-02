using System;
using System.Threading;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Log;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public abstract class KafkaConsumerHandler<T, TV> where T : class where TV : class
    {
        public abstract void HandleCommand(TV consumeResult);
        private IKakfkaHandler KafkaHandler { get; set; }
        protected IServiceCollection Services { get; set; }

        private string ConsumerId { get; set; }

        protected KafkaConsumerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string consumerId, bool commit = false, int commitPeriod = 5)
        {
            Services = services;
            KafkaHandler = kafkaHandler;
            ConsumerId = consumerId;
            Consume(commit, commitPeriod);
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
        private void Consume(bool commit, int commitPeriod) 
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
                        HandleCommand(consumeResult.Message.Value);
                        if (consumeResult.IsPartitionEOF)
                        {
                            Devon4NetLogger.Information($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                            continue;
                        }

                        Devon4NetLogger.Debug($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

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
                            Devon4NetLogger.Error($"Commit error: {e.Error.Reason}");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Devon4NetLogger.Error($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Devon4NetLogger.Error("Closing consumer.");
                consumer.Close();
            }
        }
    }
}
