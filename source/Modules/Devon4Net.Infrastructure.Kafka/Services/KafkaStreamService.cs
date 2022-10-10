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
        protected IServiceCollection Services { get; }
        public abstract void CreateStreamBuilder();

        public KafkaStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId, ISerDes<TKey> keySerDes = null, ISerDes<TValue> valueSerDes = null)
        {
            Services = services;
            StreamOptions = kafkaOptions.Streams.Find(s => s.ApplicationId == applicationId);
            GenerateStreamBuilder(keySerDes, valueSerDes);
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

        private void GenerateStreamBuilder(ISerDes<TKey> keySerDes, ISerDes<TValue> valueSerDes)
        {
            StreamBuilder = new StreamBuilder();
            CreateStreamBuilder();
            Stream = new KafkaStream(StreamBuilder.Build(), GetConfigFromOptions(keySerDes, valueSerDes));
        }

        private IStreamConfig GetConfigFromOptions(ISerDes<TKey> keySerDes, ISerDes<TValue> valueSerDes)
        {
            var config = new StreamConfig();
            
            config.DefaultKeySerDes = keySerDes ?? GetSerDesForType<TKey>();
            config.DefaultValueSerDes = valueSerDes ?? GetSerDesForType<TValue>();

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

        private ISerDes<T> GetSerDesForType<T>()
        {
            if (typeof(T) == typeof(string)) return (ISerDes<T>) new StringSerDes();
            else if (typeof(T) == typeof(byte[])) return (ISerDes<T>) new ByteArraySerDes();
            else if (typeof(T) == typeof(char)) return (ISerDes<T>) new CharSerDes();
            else if (typeof(T) == typeof(double)) return (ISerDes<T>) new DoubleSerDes();
            else if (typeof(T) == typeof(float)) return (ISerDes<T>) new FloatSerDes();
            else if (typeof(T) == typeof(int)) return (ISerDes<T>) new Int32SerDes();
            else if (typeof(T) == typeof(long)) return (ISerDes<T>) new Int64SerDes();
            return new DefaultKafkaSerDes<T>();
        }
        #endregion
    }
}