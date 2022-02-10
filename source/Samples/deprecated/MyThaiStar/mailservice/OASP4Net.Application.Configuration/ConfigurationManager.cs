using Microsoft.Extensions.Configuration;
using OASP4Net.Infrastructure.Extensions;
using System.IO;
using System.Linq;

namespace OASP4Net.Application.Configuration
{
    public class ConfigurationManager
    {
        private IConfiguration Configuration { get; set; }
        public string CorsPolicy { get; set; }
        public string LocalListenPort { get; set; }
        public string LocalKestrelUrl { get; set; }
        public string LogFile { get; set; }
        public string LogFolder { get; set; }
        public string SeqLogServerUrl { get; set; }
        public string LogDatabase { get; set; }
        public string LogCategory { get; set; }
        public string ValidAudienceJwt { get; set; }
        public string ValidAudienceJwtPort { get; set; }
        public string EmailServiceUrl { get; set; }

        public ConfigurationManager(IConfiguration configuration)
        {
            DiscoverApplicationPath();
            Configuration = configuration;
            Configure();
        }
        public ConfigurationManager()
        {
            DiscoverApplicationPath();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(ApplicationPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables().Build();

            var stagingEnvironment = Configuration["StagingEnvironment"];

            Configuration = new ConfigurationBuilder()
                .SetBasePath(ApplicationPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{stagingEnvironment}.json", optional: true)
                .AddEnvironmentVariables().Build();

            Configure();
        }

        public static string ApplicationPath { get; set; }

        public void DiscoverApplicationPath()
        {
            ApplicationPath = Path.GetDirectoryName(Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.json", SearchOption.AllDirectories).FirstOrDefault());
        }

        public IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        private string GetConfigurationValue(string key)
        {
            return Configuration[key];
        }

        private void Configure()
        {
            CorsPolicy = GetConfigurationValue("CorsPolicy");
            LocalListenPort = GetConfigurationValue("LocalListenPort");
            LocalKestrelUrl = string.Format(GetConfigurationValue("LocalKestrelUrl"), LocalListenPort);
            LogFile = GetConfigurationValue("LogFile");
            LogFolder = GetConfigurationValue("LogFolder");
            SeqLogServerUrl = GetConfigurationValue("SeqLogServerUrl");
            LogDatabase = GetConfigurationValue("LogDatabase");
            LogCategory = GetConfigurationValue("LogCategory");
        }
    }
}
