using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using FluentValidation.AspNetCore;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Configuration;
using Devon4Net.Infrastructure.Common.Application.Options;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API.Servers;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API.Configuration;
using Devon4Net.Infrastructure.Common.Application.Attributes;

namespace Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API
{
    public static class DevonfwApi
    {
        private static readonly DevonfwConfigurationBuilder DevonfwConfigurationBuilder = new();
        private static DevonfwOptions DevonfwOptions { get; set; }
        public static void InitializeDevonfwApi(this IWebHostBuilder builder, IHostBuilder hostBuilder)
        {
            var useDetailedErrorsKey = DevonfwConfigurationBuilder.Configuration[$"{OptionsDefinition.DefaultSettingsNodeName}:UseDetailedErrorsKey"];
            builder.UseSetting(WebHostDefaults.DetailedErrorsKey, useDetailedErrorsKey);

            var useIis = Convert.ToBoolean(DevonfwConfigurationBuilder.Configuration[$"{OptionsDefinition.DefaultSettingsNodeName}:UseIIS"], System.Globalization.CultureInfo.InvariantCulture);

            if (useIis)
            {
                builder.UseIISIntegration();
            }
            else
            {
                SetupKestrel.Configure(builder, DevonfwConfigurationBuilder.Configuration);
            }

            builder.UseConfiguration(DevonfwConfigurationBuilder.Configuration);
        }

        public static DevonfwOptions SetupDevonfw(this IServiceCollection services, IConfiguration configuration)
        {
            DevonfwOptions = services?.GetTypedOptions<DevonfwOptions>(configuration, OptionsDefinition.DefaultSettingsNodeName);

            if (DevonfwOptions == null)
            {
                throw new ArgumentException("Please check the devonfw options node in your configuration file");
            }

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            if (DevonfwOptions.UseModelStateValidation)
            {
                services?.AddMvc(options => options.Filters.Add(typeof(ModelStateCheckerAttribute)));
            }

            if (DevonfwOptions.UseIIS) services?.ConfigureIIS(DevonfwOptions.IIS);

            if (DevonfwOptions.UseXsrf) services?.ConfigureXsrf();

            return DevonfwOptions;
        }
    }
}
