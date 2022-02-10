using Devon4Net.Application.WebAPI.Configuration.Application;
using Devon4Net.Infrastructure.AWS.Secrets;
using Devon4Net.Infrastructure.Common.Options;
using Devon4Net.Infrastructure.Common.Options.AWS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Infrastructure.AWS.Serverless
{
    public static class AwsConfiguration
    {
        private static AwsOptions AwsOptions { get; set; }
        public static IWebHostBuilder InitializeDevonFwAws(this IWebHostBuilder builder)
        {
            builder.InitializeDevonFw();

            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.AddSecretsHandler();
            });

            return builder;
        }

        public static void ConfigureDevonFwAws(this IServiceCollection services, IConfiguration configuration)
        {
            AwsOptions = services.GetTypedOptions<AwsOptions>(configuration, "Aws");
        }
    }
}
