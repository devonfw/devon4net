using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Devon4Net.Infrastructure.AWS.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if Lambda
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
#endif
namespace Devon4Net.Infrastructure.AWS.Lambda
{
    public abstract class LambdaFunction<TInput,TOutput> where TInput : class
    {
        protected IConfigurationRoot Configuration { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }
        protected IServiceCollection ServiceCollection = new ServiceCollection();
        protected ILogger Logger { get; set; }
        protected abstract void ConfigureServices(IServiceCollection services);

        protected LambdaFunction()
        {
            Setup();
        }

        /// <summary>
        /// Setting up options
        /// </summary>
        public void Setup()
        {
            SetupConfiguration();
            ServiceCollection.AddOptions();
            ServiceCollection.AddSingleton(Configuration);
            ServiceCollection.AddLogging(ConfigureLogging); //NOSONAR false positive
            ConfigureServices(ServiceCollection);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            Logger = ServiceProvider.GetRequiredService<ILogger<TInput>>();
        }

        public Task<TOutput> FunctionHandler(TInput input, ILambdaContext context)
        {
            using var scope = ServiceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetService<ILambdaEventHandler<TInput, TOutput>>();

            if (handler == null)
            {
                var message = $"EventHandler<{typeof(TInput).Name}> Not found. Please check the dependency injection declaration";
                Logger.LogError(message);
                throw new InvalidOperationException(message);
            }

            Logger.LogInformation("Invoking handler");
            return handler.FunctionHandler(input, context);
        }

        protected void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.AddConfiguration(Configuration.GetSection("Logging"));
            logging.AddLambdaLogger(new LambdaLoggerOptions
            {
                IncludeCategory = true,
                IncludeLogLevel = true,
                IncludeNewline = true
            });
        }
        protected void SetupConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddSecretsHandler()
                .Build();
        }
    }
}
