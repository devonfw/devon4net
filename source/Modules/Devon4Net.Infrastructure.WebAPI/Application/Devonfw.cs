using Devon4Net.Application.WebAPI.Configuration.Common;
using Devon4Net.Infrastructure.Common.Common.IO;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.Devon;
using Devon4Net.Infrastructure.Logger.Logging;
using Devon4Net.Infrastructure.WebAPI.Common.Attributes;
using Devon4Net.Infrastructure.WebAPI.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Devon4Net.Application.WebAPI.Configuration.Application
{
    public static class Devonfw
    {
        private static IHostBuilder HostBuilder { get; set; }
        private static IWebHostBuilder WebHostBuilder { get; set; }
        private static IConfiguration Configuration { get; set; }
        private static ConfigurationBuilder ConfigurationBuilder { get; set; }
        private static DevonfwOptions DevonfwOptions { get; set; }

        public static IWebHostBuilder InitializeDevonFw(this IWebHostBuilder builder, IHostBuilder hostBuilder)
        {
            WebHostBuilder = builder;
            HostBuilder = hostBuilder;

            LoadConfiguration();

            var useDetailedErrorsKey = Configuration[$"{DevonFwConst.OptionsNodeName}:UseDetailedErrorsKey"];
            WebHostBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, useDetailedErrorsKey);

            var useIis = Convert.ToBoolean(Configuration[$"{DevonFwConst.OptionsNodeName}:UseIIS"], System.Globalization.CultureInfo.InvariantCulture);

            if (useIis)
            {
                WebHostBuilder.UseIISIntegration();
            }
            else
            {
                SetupKestrel.Configure(WebHostBuilder, Configuration);
            }

            WebHostBuilder.UseConfiguration(Configuration);
            HostBuilder.UseSerilog();

            return builder;
        }

        public static DevonfwOptions SetupDevonfw(this IServiceCollection services, IConfiguration configuration)
        {
            DevonfwOptions = services.GetTypedOptions<DevonfwOptions>(configuration, DevonFwConst.OptionsNodeName);

            if (DevonfwOptions == null || string.IsNullOrEmpty(DevonfwOptions.Environment) || DevonfwOptions.Kestrel == null)
            {
                throw new ArgumentException("Please check the devonfw options node in your configuration file");
            }

            if (DevonfwOptions.UseModelStateValidation)
            {
                services.AddMvc(options => options.Filters.Add(typeof(ModelStateCheckerAttribute)));
            }

            if (DevonfwOptions.UseIIS) services.ConfigureIIS(DevonfwOptions.IIS);

            if (DevonfwOptions.UseXsrf) services.ConfigureXsrf();

            return DevonfwOptions;
        }

        private static void LoadConfiguration()
        {
            SetupConfigurationBuilder();
            AddConfigurationSettingsFile("appsettings.json", true, true);
            AddConfigurationSettingsFile($"appsettings.{Configuration[$"{DevonFwConst.OptionsNodeName}:Environment"]}.json", true, true);
            CheckExtraSettingsFiles();
        }

        private static void CheckExtraSettingsFiles()
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

        private static void ManageSettingsFiles(IReadOnlyCollection<string> settingsItemList)
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

        private static void ProcessDirectorySettings(string settingsItemDirectory)
        {
            Devon4NetLogger.Information($"ProcessDirectorySettings {settingsItemDirectory}");
            if (string.IsNullOrEmpty(settingsItemDirectory) || string.IsNullOrWhiteSpace(settingsItemDirectory) || !Directory.Exists(settingsItemDirectory)) return;
            var fileNameList = FileOperations.GetFilesFromPath("*", settingsItemDirectory);

            foreach (var fileSettings in fileNameList)
            {
                Devon4NetLogger.Information($"Processing {fileSettings}");
                AddConfigurationSettingsFile(fileSettings, true, false, settingsItemDirectory);
            }
        }

        private static void AddConfigurationSettingsFile(string filename, bool optional, bool reloadOnChange, string defaultDirectory = null)
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

        private static void SetupConfigurationBuilder()
        {
            if (ConfigurationBuilder != null) return;
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            Configuration = ConfigurationBuilder.Build();
        }
    }
}