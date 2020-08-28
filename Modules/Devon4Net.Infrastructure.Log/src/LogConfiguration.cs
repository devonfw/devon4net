using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Log.Attribute;
using Serilog;
using Serilog.Sinks.Graylog.Extended;
using System;
using Devon4Net.Infrastructure.Common.Options.Log;
using Devon4Net.Infrastructure.Common;
using System.IO;
using Devon4Net.Infrastructure.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class LogConfiguration
    {
        private const string DefaultLogFile = "log-{0}.txt";
        private const string DefaultSqliteFile = "devonfwLogDatabase.db";

        private static LoggerConfiguration LoggerConfiguration { get; set; }
        public static void SetupLog(this IServiceCollection services, ref IConfiguration configuration)
        {
            var logOptions = services.GetTypedOptions<LogOptions>(configuration, "Log");

            if (logOptions == null) return;

            using var serviceProvider = services.BuildServiceProvider();

            ConfigureLog(logOptions);

            if (logOptions.UseAOPTrace)
            {
                SetupLogAop(ref services, logOptions);
            }

            if (logOptions.GrayLog != null)
            {
                SetupGraylog(logOptions.GrayLog);
            }

            serviceProvider.GetService<ILoggerFactory>().AddSerilog();
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
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(GetValidPath(logFile, DefaultLogFile));

            SetLogLevel(logOptions.LogLevel, LoggerConfiguration);

            if (!string.IsNullOrEmpty(logOptions.SeqLogServerHost))
            {
                LoggerConfiguration = LoggerConfiguration.WriteTo.Seq(logOptions.SeqLogServerHost);
            }

            if (!string.IsNullOrEmpty(logOptions.SqliteDatabase))
            {
                LoggerConfiguration = LoggerConfiguration.WriteTo.SQLite(GetValidPath(logOptions.SqliteDatabase, DefaultSqliteFile));
            }

            Log.Logger = LoggerConfiguration.CreateLogger();
        }

        private static void SetupGraylog(GraylogOptions graylogOptions)
        {
            if (graylogOptions == null) return;
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

        private static GraylogTransportType GetGraylogTransportTypeFromString(string transportType)
        {
            return transportType.ToLower() switch
            {
                "tcp" => GraylogTransportType.Tcp,
                "udp" => GraylogTransportType.Udp,
                "http" => GraylogTransportType.Http,
                _ => GraylogTransportType.Udp
            };
        }

        private static string GetValidPath(string inputPath, string optionalFileName)
        {
            if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(Path.GetFileName(inputPath)))
            {
                return Path.Combine(FileOperations.GetApplicationPath(), optionalFileName);
            }

            return Path.GetFullPath(inputPath);
        }

        private static void SetLogLevel(string logEventLevel, LoggerConfiguration loggerConfiguration)
        {
            if (string.IsNullOrEmpty(logEventLevel)) return;
            switch (logEventLevel.ToLower())
            {
                case "warning":
                    loggerConfiguration.MinimumLevel.Warning();
                    return;
                case "verbose":
                    loggerConfiguration.MinimumLevel.Verbose();
                    return;
                case "fatal":
                    loggerConfiguration.MinimumLevel.Fatal();
                    return;
                case "error":
                    loggerConfiguration.MinimumLevel.Error();
                    return;
                case "information":
                    loggerConfiguration.MinimumLevel.Information();
                    return;
                default:
                    loggerConfiguration.MinimumLevel.Debug();
                    return;
            }
        }
    }
}