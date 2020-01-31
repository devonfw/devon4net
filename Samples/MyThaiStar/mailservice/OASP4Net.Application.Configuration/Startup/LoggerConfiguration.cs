using Serilog;
using Serilog.Events;
using System;

namespace OASP4Net.Application.Configuration.Startup
{
    public static class LoggerApplicationConfiguration
    {
        public static void ConfigureLog(this ConfigurationManager configurationManager)
        {
            var logFile = string.Format(configurationManager.LogFile, DateTime.Today.ToShortDateString().Replace("/", string.Empty));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File($"{ConfigurationManager.ApplicationPath}/{configurationManager.LogFolder}/{logFile}")
                .WriteTo.Seq(configurationManager.SeqLogServerUrl)
                .WriteTo.SQLite($"{ConfigurationManager.ApplicationPath}/{configurationManager.LogFolder}/{configurationManager.LogDatabase}")
                .CreateLogger();
        }
    }
}
