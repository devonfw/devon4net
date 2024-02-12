using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.IO;
using Devon4Net.Infrastructure.Logger.Common.Attributes;
using Devon4Net.Infrastructure.Logger.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace Devon4Net.Infrastructure.Logger;

public static class LogConfiguration
{
    private const string DefaultLogFile = "log-{0}.txt";
    private const string DefaultSqliteFile = "devonfwLogDatabase.db";
    private static LoggerConfiguration LoggerConfiguration { get; set; }

    public static void SetupLog(this IServiceCollection services, IConfiguration configuration)
    {
        var logOptions = services.GetTypedOptions<LogOptions>(configuration, OptionsDefinition.LoggingOptions);

        if (logOptions == null) return;

        LoggerConfiguration = CreateLoggerConfiguration(configuration);

        services.AddSingleton(ConfigureLog(logOptions));
        services.AddSingleton(LoggerConfiguration);

        if (logOptions.UseAopTrace) SetupLogAop(ref services, logOptions);
    }

    private static ILoggerFactory ConfigureLog(LogOptions logOptions)
    {
        ConfigureLogFile(logOptions);
        ConfigureLogSeq(logOptions);
        ConfigureLogSqLiteDb(logOptions);
        SetupGraylog(logOptions);

        return CreateLoggerFactory();
    }

    private static void ConfigureLogFile(LogOptions logOptions)
    {
        if (!logOptions.UseLogFile) return;

        var logFile = logOptions.LogFile != null
            ? string.Format(logOptions.LogFile, DateTime.Today.ToShortDateString().Replace("/", string.Empty))
            : DefaultLogFile;
        LoggerConfiguration = LoggerConfiguration.WriteTo.File(GetValidPath(logFile, DefaultLogFile));
    }

    private static void ConfigureLogSeq(LogOptions logOptions)
    {
        if (!string.IsNullOrEmpty(logOptions.SeqLogServerHost))
            LoggerConfiguration = LoggerConfiguration.WriteTo.Seq(logOptions.SeqLogServerHost);
    }

    private static void ConfigureLogSqLiteDb(LogOptions logOptions)
    {
        if (logOptions.UseSqLiteDb && !string.IsNullOrEmpty(logOptions.SqliteDatabase))
            LoggerConfiguration =
                LoggerConfiguration.WriteTo.SQLite(GetValidPath(logOptions.SqliteDatabase, DefaultSqliteFile));
    }

    private static LoggerConfiguration CreateLoggerConfiguration(IConfiguration configuration)
    {
        return new LoggerConfiguration() //NOSONAR false positive
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext().WriteTo.Console();
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        Log.Logger = LoggerConfiguration.CreateLogger();

        return LoggerFactory.Create(logging => logging.AddSerilog(Log.Logger));
    }

    private static void SetupLogAop(ref IServiceCollection services, LogOptions logOptions)
    {
        services.AddTransient<AopControllerAttribute>();
        services.AddTransient<AopExceptionFilterAttribute>();
        services.AddMvc(options => options.Filters.Add(new AopControllerAttribute(logOptions.UseAopTrace)));
        services.AddMvc(options => options.Filters.Add(new AopExceptionFilterAttribute()));
    }

    private static void SetupGraylog(LogOptions logOptions)
    {
        if (logOptions?.UseGraylog == true && logOptions.GrayLog != null)
        {
            var graylogOptions = logOptions?.GrayLog;
            var graylogConfig = new GraylogSinkOptions
            {
                HostnameOrAddress = graylogOptions.GrayLogHost,
                TransportType = GetGraylogTransportTypeFromString(graylogOptions.GrayLogProtocol),
                HostnameOverride = graylogOptions.GrayLogHost,
                Port = graylogOptions.GrayLogPort,
                MaxMessageSizeInUdp = graylogOptions.MaxUdpMessageSize
            };

            LoggerConfiguration = LoggerConfiguration.WriteTo.Graylog(graylogConfig);
        }
    }

    private static TransportType GetGraylogTransportTypeFromString(string transportType)
    {
        if (transportType == null) return TransportType.Udp;
        return transportType.ToLower() switch
        {
            "tcp" => TransportType.Tcp,
            "udp" => TransportType.Udp,
            "http" => TransportType.Http,
            _ => TransportType.Udp
        };
    }

    private static string GetValidPath(string inputPath, string optionalFileName)
    {
        if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(Path.GetFileName(inputPath)))
            return Path.Combine(FileOperations.GetApplicationPath() ?? string.Empty, optionalFileName);

        return Path.GetFullPath(inputPath);
    }
}