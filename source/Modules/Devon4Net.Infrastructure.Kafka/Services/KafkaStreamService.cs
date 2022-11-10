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
        private bool _disposed;
        private StreamOptions StreamOptions { get; set; }
        private KafkaStream Stream { get; set; }
        protected StreamBuilder StreamBuilder { get; set; }
        protected IServiceCollection Services { get; }
        public abstract void CreateStreamBuilder();

        protected KafkaStreamService(IServiceCollection services, KafkaOptions kafkaOptions, string applicationId, ISerDes<TKey> keySerDes = null, ISerDes<TValue> valueSerDes = null)
        {
            Services = services;
            StreamOptions = kafkaOptions.Streams.Find(s => s.ApplicationId == applicationId);
            GenerateStreamBuilder(keySerDes, valueSerDes);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Stream.StartAsync();
        }

        public override void Dispose()
        {
            DisposeKafkaStreamService(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void DisposeKafkaStreamService(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Stream.Dispose();
                base.Dispose();
            }

            _disposed = true;
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
            return new StreamConfig
            {
                DefaultKeySerDes = keySerDes ?? GetSerDesForType<TKey>(),
                DefaultValueSerDes = valueSerDes ?? GetSerDesForType<TValue>(),
                ApplicationId = StreamOptions.ApplicationId,
                BootstrapServers = StreamOptions.Servers,
                AutoOffsetReset = KafkaConverters.GetAutoOffsetReset(StreamOptions.AutoOffsetReset),
                StateDir = StreamOptions.StateDir,
                CommitIntervalMs = StreamOptions.CommitIntervalMs ?? KafkaDefaultValues.StreamsCommitIntervalMs,
                Guarantee = KafkaConverters.GetProcessingGuarantee(StreamOptions.Guarantee),
                MetricsRecording = KafkaConverters.GetMetricsRecordingLevel(StreamOptions.MetricsRecording)
            };
        }

        protected TS GetInstance<TS>()
        {
            var sp = Services.BuildServiceProvider();
            return sp.GetService<TS>();
        }

        private static ISerDes<T> GetSerDesForType<T>()
        {
            var type = typeof(T);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return (ISerDes<T>)new StringSerDes();
                case TypeCode.Double:
                    return (ISerDes<T>)new DoubleSerDes();
                case TypeCode.Single:
                    return (ISerDes<T>)new FloatSerDes();
                case TypeCode.Char:
                    return (ISerDes<T>)new CharSerDes();
                case TypeCode.Int32:
                    return (ISerDes<T>)new Int32SerDes();
                case TypeCode.Int64:
                    return (ISerDes<T>)new Int64SerDes();
                case TypeCode.Object:
                    return type == typeof(byte[])
                        ? (ISerDes<T>)new ByteArraySerDes()
                        : new DefaultKafkaSerDes<T>();
                default:
                    return new DefaultKafkaSerDes<T>();
            }
        }
        #endregion
    }
}