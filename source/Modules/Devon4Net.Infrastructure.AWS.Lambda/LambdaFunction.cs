using Amazon;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Devon4Net.Infrastructure.AWS.Common.Consts;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Devon4Net.Infrastructure.AWS.ParameterStore;
using Devon4Net.Infrastructure.AWS.Secrets;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#if Lambda
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
#endif
namespace Devon4Net.Infrastructure.AWS.Lambda
{
    public abstract class LambdaFunction<TInput, TOutput> where TInput : class
    {
        protected IConfigurationRoot Configuration { get; set; }
        protected IConfigurationBuilder ConfigurationBuilder { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }
        protected IServiceCollection ServiceCollection = new ServiceCollection();
        protected AWSCredentials AWSCredentials { get; set; }
        protected ILogger Logger { get; set; }
        protected AwsOptions AwsOptions { get; set; }
        protected abstract void ConfigureServices(IServiceCollection services);

        protected LambdaFunction()
        {
            Setup();
        }

        public void Setup()
        {
            SetupConfiguration();
            LoadAwsCredentials(ServiceCollection);
            LoadAwsConfigurationSources();
            LoadAwsRegionEndpoint(ServiceCollection);
            SetupServiceActions();
            ConfigureServices(ServiceCollection);
            FinalizeSetupServiceProviderActions();
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
                var lambdaMessage = $"Message '{name}': \"{ex?.Message}\" | InnerException: \"{ex?.InnerException}\"";
                LambdaLogger.Log(lambdaMessage);

                var message = ex?.Message;
                var innerException = ex?.InnerException;
                Logger.LogError("Message '{name}': \"{message}\" | InnerException: \"{innerException}\"", name, message, innerException);

                throw;
            }
            finally
            {
                LambdaLogger.Log($"Disposing {name} function scope");
                Logger.LogInformation("Disposing {name} function scope", name);
                scope?.Dispose();
            }
        }

        #region provate methods
        private void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.AddConfiguration(Configuration.GetSection("Logging"));
            logging.AddLambdaLogger(new LambdaLoggerOptions
            {
                IncludeCategory = true,
                IncludeLogLevel = true,
                IncludeNewline = true,
                IncludeEventId = true,
                IncludeException = true
            });
        }

        private void SetupServiceActions()
        {
            ServiceCollection.AddOptions();
            ServiceCollection.AddSingleton(Configuration);
            ServiceCollection.AddLogging(ConfigureLogging); //NOSONAR false positive
        }

        private void FinalizeSetupServiceProviderActions()
        {
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            Logger = ServiceProvider.GetRequiredService<ILogger<TInput>>();
        }

        protected void SetupConfiguration()
        {
            ConfigurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = ConfigurationBuilder.Build();
            AwsOptions = ServiceCollection.GetTypedOptions<AwsOptions>(Configuration, ConfigurationConsts.AwsOptionsNodeName);
        }

        private void LoadAwsConfigurationSources()
        {
            if (AwsOptions.UseSecrets)
            {
                ConfigurationBuilder.AddSecretsHandler(AWSCredentials);
            }

            if (AwsOptions.UseParameterStore)
            {
                ConfigurationBuilder.AddParameterStoreHandler(AWSCredentials);
            }

            if (AwsOptions.UseParameterStore || AwsOptions.UseSecrets)
            {
                Configuration = ConfigurationBuilder.Build();
            }
        }

        private void LoadAwsRegionEndpoint(IServiceCollection services)
        {
            if (!string.IsNullOrEmpty(AwsOptions.Credentials.Region))
            {
                services.AddSingleton(RegionEndpoint.GetBySystemName(AwsOptions.Credentials.Region));
            }
        }

        private void LoadAwsCredentials(IServiceCollection services)
        {
            AWSCredentials = null;

            if (!string.IsNullOrEmpty(AwsOptions.Credentials.AccessKeyId) && !string.IsNullOrEmpty(AwsOptions.Credentials.SecretAccessKey))
            {
                AWSCredentials = new BasicAWSCredentials(AwsOptions.Credentials.AccessKeyId, AwsOptions.Credentials.SecretAccessKey);
            }
            else
            {
                if (!string.IsNullOrEmpty(AwsOptions.Credentials.Profile))
                {
                    var sharedFile = new SharedCredentialsFile();
                    sharedFile.TryGetProfile(AwsOptions.Credentials.Profile, out var profile);
                    AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedFile, out AWSCredentials credentials);
                    AWSCredentials = credentials;
                }
            }

            if (AWSCredentials != null) services.AddSingleton(AWSCredentials);
        }
        #endregion
    }
}
