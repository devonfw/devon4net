using Amazon.Runtime;
using AWS.Logger;
using AWS.Logger.SeriLog;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Devon4Net.Infrastructure.Logger;

public static class AwsLogConfiguration
{
    private static LoggerConfiguration LoggerConfiguration { get; set; }
    private static AWSCredentials AwsCredentials { get; set; }

    public static void SetupAwsLog(this IServiceCollection services, IConfiguration configuration)
    {
        Setup(services, configuration);
    }

    public static void SetupAwsLog(this IServiceCollection services, IConfiguration configuration, AWSCredentials awsCredentials)
    {
        AwsCredentials = awsCredentials;
        Setup(services, configuration);
    }

    private static void Setup(IServiceCollection services, IConfiguration configuration)
    {
        var logOptions = services.GetTypedOptions<AwsOptions>(configuration, OptionsDefinition.AwsOptions);
        if (logOptions?.AwsLogOptions == null) return;
        services.AddSingleton(ConfigureLog(logOptions?.AwsLogOptions));
        services.AddSingleton(LoggerConfiguration);
    }

    private static ILoggerFactory ConfigureLog(AwsLogOptions awsLogOptions)
    {
        LoggerConfiguration = CreateLoggerConfiguration(awsLogOptions);
        SetLogLevel(string.IsNullOrWhiteSpace(awsLogOptions.LogLevel)? "Debug" : awsLogOptions.LogLevel);        
        return CreateLoggerFactory();
    }

    private static LoggerConfiguration CreateLoggerConfiguration(AwsLogOptions awsLogOptions)
    {
        var loggerConfiguration = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console(); //NOSONAR false positive
        var options =  new AWSLoggerConfig { Region = awsLogOptions.LogRegion, LogGroup = awsLogOptions.LogGroup};
        if (AwsCredentials!=null) options.Credentials = AwsCredentials;
        return loggerConfiguration.Enrich.FromLogContext().WriteTo.AWSSeriLog(options); //NOSONAR false positive
    }
    
    private static ILoggerFactory CreateLoggerFactory()
    {
        Log.Logger = LoggerConfiguration.CreateLogger();
        return LoggerFactory.Create(logging => { logging.AddSerilog(Log.Logger); }).AddSerilog();
    }

    private static void SetLogLevel(string logEventLevel = "verbose")
    {
        switch (logEventLevel.ToLower())
        {
            case "warning":
               LoggerConfiguration.MinimumLevel.Warning();
                return;
            case "verbose":
                LoggerConfiguration.MinimumLevel.Verbose();
                return;
            case "fatal":
                LoggerConfiguration.MinimumLevel.Fatal();
                return;
            case "error":
                LoggerConfiguration.MinimumLevel.Error();
                return;
            case "information":
                LoggerConfiguration.MinimumLevel.Information();
                return;
            default:
                LoggerConfiguration.MinimumLevel.Debug();
                return;
        }
    }
}