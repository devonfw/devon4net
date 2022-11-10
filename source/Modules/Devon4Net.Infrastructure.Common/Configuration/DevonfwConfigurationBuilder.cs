using Devon4Net.Infrastructure.Common.IO;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.Common.Configuration
{
    public class DevonfwConfigurationBuilder
    {
        public  IConfiguration Configuration { get; set; }
        public  ConfigurationBuilder ConfigurationBuilder { get; set; }

        public DevonfwConfigurationBuilder()
        {
            SetupConfigurationBuilder();
            LoadConfiguration();
        }

        private void SetupConfigurationBuilder()
        {
            if (ConfigurationBuilder != null) return;
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            Configuration = ConfigurationBuilder.Build();
        }

        private void LoadConfiguration()
        {
            AddConfigurationSettingsFile("appsettings.json", true, true);
            var environment = string.IsNullOrWhiteSpace(Configuration["Environment"]) ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") : Configuration["Environment"]; 
            AddConfigurationSettingsFile($"appsettings.{environment}.json", true, true);
            CheckExtraSettingsFiles();
        }



        private void AddConfigurationSettingsFile(string filename, bool optional, bool reloadOnChange, string defaultDirectory = null)
        {
            if (string.IsNullOrEmpty(filename) || string.IsNullOrWhiteSpace(filename))
            {
                Devon4NetLogger.Information($"{filename} settings file does NOT exists!!!");
                return;
            }

            SetupConfigurationBuilder();

            var fileName = FileOperations.GetFileFullPath(filename, defaultDirectory);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            ConfigurationBuilder.AddJsonFile(filename, optional, reloadOnChange);
            Configuration = ConfigurationBuilder.Build();
        }

        private void CheckExtraSettingsFiles()
        {
            Devon4NetLogger.Information("CheckExtraSettingsFiles Initialized");
            var appSettingsList = new List<string>();
            Configuration.GetSection("ExtraSettingsFiles").Bind(appSettingsList);

            if (appSettingsList?.Any() != true)
            {
                Devon4NetLogger.Information("CheckExtraSettingsFiles does not contains any settings file to be managed");
                return;
            }

            Devon4NetLogger.Information($"CheckExtraSettingsFiles has detected the global settings : {appSettingsList}");
            ManageSettingsFiles(appSettingsList);
        }

        private void ManageSettingsFiles(IReadOnlyCollection<string> settingsItemList)
        {
            Devon4NetLogger.Information("Managing settings global settings files ...");
            if (settingsItemList?.Any() != true)
            {
                Devon4NetLogger.Information("No global settings files found!");
                return;
            }

            foreach (var settingsItem in settingsItemList)
            {
                if (string.IsNullOrEmpty(settingsItem)) continue;

                if (Directory.Exists(settingsItem))
                {
                    Devon4NetLogger.Information($"SettingsItem {settingsItem} is a directory. Checking the directory...");
                    ProcessDirectorySettings(settingsItem);
                    Devon4NetLogger.Information($"SettingsItem {settingsItem} processed");
                }
                else
                {
                    if (!File.Exists(settingsItem))
                    {
                        Devon4NetLogger.Error($"The provided settings file '{settingsItem}' does not exists as directory or file");
                        continue;
                    }

                    Devon4NetLogger.Information($"SettingsItem {settingsItem} is a file. Checking the file...");
                    AddConfigurationSettingsFile(settingsItem, true, false);
                    Devon4NetLogger.Information($"SettingsItem {settingsItem} processed");
                }
            }
        }

        private void ProcessDirectorySettings(string settingsItemDirectory)
        {
            if (string.IsNullOrEmpty(settingsItemDirectory) || string.IsNullOrWhiteSpace(settingsItemDirectory) || !Directory.Exists(settingsItemDirectory)) return;
            var fileNameList = FileOperations.GetFilesFromPath("*", settingsItemDirectory);

            foreach (var fileSettings in fileNameList)
            {
                Devon4NetLogger.Information($"Processing {fileSettings}");
                AddConfigurationSettingsFile(fileSettings, true, false, settingsItemDirectory);
            }
        }
    }
}
