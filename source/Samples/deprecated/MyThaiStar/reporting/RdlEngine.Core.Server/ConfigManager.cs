using System.IO;
using Microsoft.Extensions.Configuration;

namespace OASP4Net.Application.WebApi
{
    public class ConfigManager
    {
        private  IConfiguration Configuration { get; set; }
        private static bool UseVisualStudioIde = false;
        public string CorsPolicy { get; set; }
        public string LocalListenPort { get; set; }
        public string LocalKestrelUrl { get; set; }
        public string LogFile { get; set; }
        public string LogFolder { get; set; }
        public string SeqLogServerUrl { get; set; }
        public string LogDatabase { get; set; }
        public string LogCategory { get; set; }
        public string ValidAudienceJwt { get; set; }

        public string TemplatesFolder { get; set; }



        public ConfigManager(IConfiguration configuration)
        {
            Configuration = configuration;
            Configure();
        }
        public ConfigManager()
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(ConfigManager.ApplicationPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Configure();
        }

        public static string ApplicationPath
        {
            get
            {
                
                return Directory.GetCurrentDirectory() + @"/";
            }
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
            LocalKestrelUrl = string.Format( GetConfigurationValue("LocalKestrelUrl"),LocalListenPort);
            LogFile = GetConfigurationValue("LogFile");
            LogFolder = GetConfigurationValue("LogFolder");
            SeqLogServerUrl = GetConfigurationValue("SeqLogServerUrl");
            LogDatabase = GetConfigurationValue("LogDatabase");
            LogCategory = GetConfigurationValue("LogCategory");
            ValidAudienceJwt =  string.Format( GetConfigurationValue("ValidAudienceJwt"),LocalListenPort);
            TemplatesFolder = GetConfigurationValue("TemplatesFolder");
        }
    }
}
