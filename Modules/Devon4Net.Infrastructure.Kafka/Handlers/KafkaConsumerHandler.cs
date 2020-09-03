using System;
using System.Threading;
using Confluent.Kafka;
using Devon4Net.Infrastructure.Log;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.Kafka.Handlers
{
    public abstract class KafkaConsumerHandler<T, TV> where T : class where TV : class
    {
        public abstract void HandleCommand(T key, TV value);
        private IKakfkaHandler KafkaHandler { get; set; }
        private bool EnableConsumerFlag { get; set; }
        private bool Commit { get; set; }
        private int CommitPeriod { get; set; }

        protected IServiceCollection Services { get; set; }

        private string ConsumerId { get; set; }

        public KafkaConsumerHandler(IServiceCollection services, IKakfkaHandler kafkaHandler, string consumerId, bool commit = false, int commitPeriod = 5)
        {
            Services = services;
            KafkaHandler = kafkaHandler;
            ConsumerId = consumerId;
            EnableConsumerFlag = true;
            Commit = commit;
            CommitPeriod = commitPeriod;
            Consume(Commit, CommitPeriod);
        }

        public T GetInstance<T>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<T>();
        }

        public void EnableConsumer(bool startConsumer = true)
        {
            EnableConsumerFlag = true;
            Devon4NetLogger.Debug("The EnableConsumerFlag is set to true");
            if (startConsumer) Consume(Commit, CommitPeriod);
        }

        public void DisableConsumer()
        {
            EnableConsumerFlag = false;
            Devon4NetLogger.Debug("The EnableConsumerFlag is set to false");
        }

        /// <summary>
        /// If commit is set to true, the Commit method sends a "commit offsets" request to the Kafka cluster and synchronously waits for the response.
        /// This is very slow compared to the rate at which the consumer is capable of consuming messages.
        /// A high performance application will typically commit offsets relatively infrequently and be designed handle duplicate messages in the event of failure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="commit"></param>
        /// <param name="commitPeriod"></param>
        private void Consume(bool commit, int commitPeriod) 
        {
            var cancellationToken = new CancellationTokenSource();

            using var consumer = KafkaHandler.GetConsumerBuilder<T, TV>(ConsumerId);
            try
            {
                while (EnableConsumerFlag)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken.Token);
                        if (consumeResult?.Message == null) continue;

                        HandleCommand(consumeResult.Message.Key, consumeResult.Message.Value);
                        if (consumeResult.IsPartitionEOF)
                        {
                            Devon4NetLogger.Information($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                            continue;
                        }

                        Devon4NetLogger.Debug($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");

                        if (!commit) continue;

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

                Devon4NetLogger.Debug("The EnableConsumerFlag is set to false. Going to close consumer");
                Devon4NetLogger.Information("Closing consumer");
                consumer.Close();
                Devon4NetLogger.Information("Consumer closed");
            }
            catch (OperationCanceledException e)
            {
                Devon4NetLogger.Error("Closing consumer.");
                Devon4NetLogger.Error(e);
                consumer?.Close();
            }
        }
    }
}
