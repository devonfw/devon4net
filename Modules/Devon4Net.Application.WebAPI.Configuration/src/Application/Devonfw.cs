using System;
using System.IO;
using Devon4Net.Infrastructure.Common;
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
        private static IConfiguration Configuration { get; set; }
        private static ConfigurationBuilder ConfigurationBuilder { get; set; }

        public static void Configure<T>(string[] args) where T : class
        {
            LoadConfiguration();
            CreateHostBuilder<T>(args);
            HostBuilder.Build().Run();
        }

        public static IWebHostBuilder InitializeDevonFw(this IWebHostBuilder builder)
        {
            LoadConfiguration();
            builder.UseSerilog();
            builder.UseConfiguration(Configuration);

            var useDetailedErrorsKey = Configuration["devonfw:UseDetailedErrorsKey"];
            builder.UseSetting(WebHostDefaults.DetailedErrorsKey, useDetailedErrorsKey);

            var useIis = Convert.ToBoolean(Configuration["devonfw:UseIIS"],
                System.Globalization.CultureInfo.InvariantCulture);

            if (useIis)
            {
                ConfigureIis(ref builder);
            }
            else
            {
                SetupKestrel.Configure(ref builder, Configuration);
            }

            builder.ConfigureServices(services => services.AddSingleton(Configuration));
            return builder;
        }

        private static void CreateHostBuilder<T>(string[] args)  where T: class
        {
            HostBuilder = Host.CreateDefaultBuilder(args);
            HostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<T>();
                webBuilder.UseSerilog();
                webBuilder.UseConfiguration(Configuration);

                var useDetailedErrorsKey = Configuration["devonfw:UseDetailedErrorsKey"];
                webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, useDetailedErrorsKey);

                var useIis = Convert.ToBoolean(Configuration["devonfw:UseIIS"],
                    System.Globalization.CultureInfo.InvariantCulture);

                if (useIis)
                {
                    ConfigureIis(ref webBuilder);
                }
                else
                {
                    SetupKestrel.Configure(ref webBuilder, Configuration);
                }

            });
        }

        private static void ConfigureIis(ref IWebHostBuilder webBuilder)
        {
            webBuilder.UseIISIntegration();
        }

        private static void LoadConfiguration()
        {
            AddConfigurationSettingsFile("appsettings.json", false, true);
            AddConfigurationSettingsFile($"appsettings.{Configuration["devonfw:Environment"]}.json", true, true);
        }

        private static void AddConfigurationSettingsFile(string filename, bool optional, bool reloadOnChange)
        {
            if (string.IsNullOrEmpty(filename) || string.IsNullOrWhiteSpace(filename)) return;

            if (ConfigurationBuilder == null)
            {
                ConfigurationBuilder = new ConfigurationBuilder();
                ConfigurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            }

            var fileName = FileOperations.GetFileFullPath(filename);

            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName)) return;

            ConfigurationBuilder.AddJsonFile(filename, optional, reloadOnChange);
            Configuration = ConfigurationBuilder.Build();
        }
    }
}