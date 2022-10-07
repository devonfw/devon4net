using Confluent.Kafka;
using Devon4Net.Infrastructure.Kafka.Common.Const;
using Devon4Net.Infrastructure.Kafka.Common.Converters;
using Devon4Net.Infrastructure.Kafka.Options;
using Devon4Net.Infrastructure.Kafka.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;


namespace Devon4Net.Infrastructure.Kafka.Streams.Services
{
    public abstract class KafkaStreamService<TKey, TValue> : BackgroundService where TValue : class where TKey : class
    {
       
        private StreamOptions StreamOptions { get; set; }
        private KafkaStream Stream { get; set; }
        protected StreamBuilder StreamBuilder { get; set; }
        public IServiceCollection Services { get; }
        public abstract void CreateStreamBuilder();

        public KafkaStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId)
        {
            Services = services;
            StreamOptions = kafkaOptions.Streams.Find(s => s.ApplicationId == applicationId);
            GenerateStreamBuilder();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Stream.StartAsync();
        }

        public override void Dispose()
        {
            Stream.Dispose();
            base.Dispose();
        }

        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }

        #region StreamConfiguration

        private void GenerateStreamBuilder()
        {
            StreamBuilder = new StreamBuilder();
            CreateStreamBuilder();
            Stream = new KafkaStream(StreamBuilder.Build(), GetConfigFromOptions());
        }

        // 
        private IStreamConfig GetConfigFromOptions()
        {
            var config = new StreamConfig();
            
            config.DefaultValueSerDes = new DefaultKafkaSerDes<TValue>();
            config.DefaultKeySerDes = new StringSerDes();

            config.ApplicationId = StreamOptions.ApplicationId;
            config.BootstrapServers = StreamOptions.Servers;
            config.AutoOffsetReset = KafkaConverters.GetAutoOffsetReset(StreamOptions.AutoOffsetReset);
            config.StateDir = StreamOptions.StateDir;
            config.CommitIntervalMs = StreamOptions.CommitIntervalMs ?? KafkaDefaultValues.StreamsCommitIntervalMs;
            config.Guarantee = KafkaConverters.GetProcessingGuarantee(StreamOptions.Guarantee);
            config.MetricsRecording = KafkaConverters.GetMetricsRecordingLevel(StreamOptions.MetricsRecording);

            return config;
        }

        protected TS GetInstance<TS>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<TS>();
        }
        #endregion
    }
}