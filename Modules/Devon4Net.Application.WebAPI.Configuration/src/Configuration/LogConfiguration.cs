using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Log.Attribute;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Graylog.Extended;
using System;
using Devon4Net.Infrastructure.Common.Options.Log;
using Devon4Net.Infrastructure.Common;
using System.IO;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class LogConfiguration
    {
        private const string DefaultLogFile = "log-{0}.txt";
        private const string DefaultSqliteFile = "devonfwLogDatabase.db";
        private static LoggerConfiguration LoggerConfiguration { get; set; }
        public static void SetupLog(this IServiceCollection services, LogOptions logOptions)
        {
            if (logOptions == null) return;

            ConfigureLog(logOptions);

            if (logOptions.UseAOPTrace)
            {
                SetupLogAop(ref services, logOptions);
            }

            if (logOptions.GrayLog != null)
            {
                SetupGraylog(logOptions.GrayLog);
            }
        }

        private static void SetupLogAop(ref IServiceCollection services, LogOptions logOptions)
        {
            services.AddTransient<AopControllerAttribute>();
            services.AddTransient<AopExceptionFilterAttribute>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AopControllerAttribute(logOptions.UseAOPTrace));
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AopExceptionFilterAttribute());
            });
        }

        public static void ConfigureLog(LogOptions logOptions)
        {
            var logFile = logOptions.LogFile != null ? string.Format(logOptions.LogFile, DateTime.Today.ToShortDateString().Replace("/", string.Empty)) : DefaultLogFile;

            LoggerConfiguration = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", GetLogLevel(logOptions.LogLevel))
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(GetValidPath(logFile, DefaultLogFile));

            if (!string.IsNullOrEmpty(logOptions.SeqLogServerHost))
            {
                LoggerConfiguration = LoggerConfiguration.WriteTo.Seq(logOptions.SeqLogServerHost);
            }

            if (!string.IsNullOrEmpty(logOptions.SqliteDatabase))
            {
                LoggerConfiguration = LoggerConfiguration.WriteTo.SQLite(GetValidPath(logOptions.SqliteDatabase, DefaultSqliteFile));
            }

            Serilog.Log.Logger = LoggerConfiguration.CreateLogger();
        }

        private static void SetupGraylog(GraylogOptions graylogOptions)
        {
            if (graylogOptions != null)
            {
                var graylogConfig = new GraylogSinkConfiguration
                {
                    GraylogTransportType = GetGraylogTransportTypeFromString(graylogOptions.GrayLogProtocol),
                    Host = graylogOptions.GrayLogHost,
                    Port = graylogOptions.GrayLogPort,
                    UseSecureConnection = graylogOptions.UseSecureConnection,
                    UseAsyncLogging = graylogOptions.UseAsyncLogging,
                    RetryCount = graylogOptions.RetryCount,
                    RetryIntervalMs = graylogOptions.RetryIntervalMs,
                    MaxUdpMessageSize = graylogOptions.MaxUdpMessageSize
                };

                LoggerConfiguration = LoggerConfiguration.WriteTo.Graylog(graylogConfig);
            }
        }

        private static GraylogTransportType GetGraylogTransportTypeFromString(string transportType)
        {
            switch (transportType.ToLower())
            {
                case "tcp":
                    return GraylogTransportType.Tcp;
                case "udp":
                    return GraylogTransportType.Udp;
                case "http":
                    return GraylogTransportType.Http;
                default:
                    break;
            }
            return GraylogTransportType.Udp;
        }

        private static string GetValidPath(string inputPath, string optionalFileName)
        {
            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(Path.GetFileName(inputPath)))
            {
                return Path.Combine(FileOperations.GetApplicationPath(), optionalFileName);
            }

            return Path.GetFullPath(inputPath);
        }

        private static LogEventLevel GetLogLevel(string logLevelDescription)
        {
            if (string.IsNullOrEmpty(logLevelDescription)) return LogEventLevel.Debug;
            switch (logLevelDescription.ToLower())
            {
                case "warning":
                    return LogEventLevel.Warning;
                case "verbose":
                    return LogEventLevel.Verbose;
                case "fatal":
                    return LogEventLevel.Fatal;
                case "error":
                    return LogEventLevel.Error;
                case "information":
                    return LogEventLevel.Information;
                case "debug":
                default:
                    return LogEventLevel.Debug;

            }
        }
    }
}