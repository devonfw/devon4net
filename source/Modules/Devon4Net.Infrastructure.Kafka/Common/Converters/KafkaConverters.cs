using Confluent.Kafka;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.Kafka.Common.Converters
{
    public static class KafkaConverters
    {
        public static Acks? GetAck(string producerAck)
        {
            return producerAck.ToLower() switch
            {
                "gzip" => Acks.None,
                "all" => Acks.All,
                "leader" => Acks.Leader,
                "none" => Acks.None,
                _ => Acks.None
            };
        }

        public static CompressionType? GetCompressionType(string producerCompressionType)
        {
            return producerCompressionType.ToLower() switch
            {
                "gzip" => CompressionType.Gzip,
                "snappy" => CompressionType.Snappy,
                "lz4" => CompressionType.Lz4,
                "zstd" => CompressionType.Zstd,
                "none" => CompressionType.None,
                _ => CompressionType.None
            };
        }

        public static AutoOffsetReset? GetAutoOffsetReset(string consumerAutoOffsetReset)
        {
            return consumerAutoOffsetReset.ToLower() switch
            {
                "latest" => AutoOffsetReset.Latest,
                "earliest" => AutoOffsetReset.Earliest,
                "error" => AutoOffsetReset.Error,
                _ => AutoOffsetReset.Latest
            };
        }

        public static IsolationLevel GetIsolationLevel(string isolationLevel)
        {
            return isolationLevel.ToLower() switch
            {
                "readuncommitted" => IsolationLevel.ReadUncommitted,
                "readcommitted" => IsolationLevel.ReadCommitted,
                _ => IsolationLevel.ReadCommitted
            };
        }

        public static ProcessingGuarantee GetProcessingGuarantee(string processingGuarantee)
        {
            return processingGuarantee.ToLower() switch
            {
                "at_least_one" => ProcessingGuarantee.AT_LEAST_ONCE,
                "exactly_one" => ProcessingGuarantee.EXACTLY_ONCE,
                _ => ProcessingGuarantee.AT_LEAST_ONCE
            };
        }
        public static MetricsRecordingLevel GetMetricsRecordingLevel(string metricsRecordingLevel)
        {
            return metricsRecordingLevel.ToLower() switch
            {
                "info" => MetricsRecordingLevel.INFO,
                "debug" => MetricsRecordingLevel.DEBUG,
                _ => MetricsRecordingLevel.DEBUG
            };
        }
    }
}
