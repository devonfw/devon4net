using System;
using System.IO;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.WebAPI.Configuration
{
    public static class KillSwitchConfiguration
    {
        public static void SetupKillSwitch(this IServiceCollection services, ref IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var killSwitchFile = configuration["devonfw:KillSwitch:killSwitchSettingsFile"];

            var filePath = FileOperations.GetFileFullPath(killSwitchFile);
            
            if (!File.Exists(filePath)) return;
            
            var configurationBuilder = new ConfigurationBuilder().AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filePath, optional: false, reloadOnChange: true);

            configurationBuilder.AddJsonFile(filePath, optional: false, reloadOnChange: true);
            configuration = configurationBuilder.Build();
        }
    }
}