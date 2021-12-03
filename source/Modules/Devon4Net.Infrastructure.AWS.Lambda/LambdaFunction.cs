using Amazon.Lambda.Core;
using Devon4Net.Infrastructure.AWS.Common.Consts;
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

        public async Task<TOutput> FunctionHandler(TInput input, ILambdaContext context)
        {
            var scope = ServiceProvider.CreateScope();
            var name = typeof(TInput).Name;

            try
            {
                var handler = scope.ServiceProvider.GetService<ILambdaEventHandler<TInput, TOutput>>();

                if (handler == null)
                {
                    var message = $"EventHandler<{name}> Not found. Please check the dependency injection declaration";
                    Logger.LogError("EventHandler<{name}> Not found. Please check the dependency injection declaration", name);

                    throw new InvalidOperationException(message);
                }

                Logger.LogInformation("Invoking handler {name}", name);

                return await handler.FunctionHandler(input, context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var message = ex?.Message;
                var innerException = ex?.InnerException;
                Logger.LogError("Message '{name}': \"{message}\" | InnerException: \"{innerException}\"", name, message, innerException);
                throw;
            }
            finally
            {
                Logger.LogInformation("Disposing {name} function scope", name);
                scope?.Dispose();
            }
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
            var builder =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            _ = bool.TryParse(Configuration[ConfigurationConsts.AwsSecretsNodeName], out bool useSecrets);

            if (useSecrets)
            {
                builder.AddSecretsHandler();
                Configuration = builder.Build();
            }
        }
    }
}
