using Devon4Net.Application.WebAPI.Configuration.Application;
using Devon4Net.Infrastructure.AWS.Secrets;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common.Options.AWS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Devon4Net.Infrastructure.AWS.Serverless
{
    public static class AwsConfiguration
    {
        private static AwsOptions AwsOptions { get; set; }
        public static IWebHostBuilder InitializeDevonFwAws(this IWebHostBuilder builder, AwsOptions awsOptions)
        {
            builder.InitializeDevonFw();

            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                if (awsOptions.UseSecrets) configurationBuilder.AddSecretsHandler();
            });

            return builder;
        }

        public static void ConfigureDevonfwAWS(this IServiceCollection services, IConfiguration configuration, ConfigureHostBuilder hostBuilder)
        {
            AwsOptions = services.GetTypedOptions<AwsOptions>(configuration, "AWS");

            if (AwsOptions.EnableAws)
            {
                hostBuilder.ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.InitializeDevonFwAws(AwsOptions));
            }
        }
    }
}
