using Amazon.Lambda.Core;
using Amazon.Runtime;
using Devon4Net.Infrastructure.AWS.Common.Managers.ParameterStoreManager;
using Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.Lambda.Interfaces;
using Devon4Net.Infrastructure.AWS.SQS.Handlers;
using Devon4Net.Infrastructure.AWS.SQS.Interfaces;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.AWS.Common.Helpers;
using Devon4Net.Infrastructure.Common.Configuration;

#if Lambda
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
#endif
namespace Devon4Net.Infrastructure.AWS.Lambda
{
    public abstract class LambdaFunction<TInput, TOutput> where TInput : class
    {
        protected IServiceProvider ServiceProvider { get; set; }
        protected IServiceCollection ServiceCollection = new ServiceCollection();
        protected AWSCredentials AWSCredentials { get; set; }
        protected AwsOptions AwsOptions { get; set; }
        protected abstract void ConfigureServices(IServiceCollection services);
        private AwsCredentialsHelper AwsCredentialsHelper { get; set; }
        private readonly DevonfwConfigurationBuilder DevonfwConfigurationBuilder = new();
        protected LambdaFunction()
        {
            Setup();
        }

        public void Setup()
        {
            SetupConfiguration();
            LoadAwsCredentials();
            LoadAwsRegionEndpoint();
            LoadAwsConfigurationSources();
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
                    Devon4NetLogger.Error($"EventHandler<{name}> Not found. Please check the dependency injection declaration");

                    throw new InvalidOperationException(message);
                }

                Devon4NetLogger.Information($"Invoking handler {name}");

                return await handler.FunctionHandler(input, context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var lambdaMessage = $"Message '{name}': \"{ex?.Message}\" | InnerException: \"{ex?.InnerException}\"";
                LambdaLogger.Log(lambdaMessage);
                Devon4NetLogger.Error(ex);
                throw;
            }
            finally
            {
                LambdaLogger.Log($"Disposing {name} function scope");
                Devon4NetLogger.Information($"Disposing {name} function scope");
                scope?.Dispose();
            }
        }

        #region private methods
        private void SetupServiceActions()
        {
            if (AWSCredentials != null)
            {
                ServiceCollection.SetupAwsLog(DevonfwConfigurationBuilder.Configuration, AWSCredentials);
            }
            else
            {
                ServiceCollection.SetupAwsLog(DevonfwConfigurationBuilder.Configuration);
            }

            ServiceCollection.AddOptions();
            ServiceCollection.AddSingleton(DevonfwConfigurationBuilder.Configuration);
        }

        private void FinalizeSetupServiceProviderActions()
        {
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        protected void SetupConfiguration()
        {
            AwsOptions = ServiceCollection.GetTypedOptions<AwsOptions>(DevonfwConfigurationBuilder.Configuration, OptionsDefinition.AwsOptions);
            AwsCredentialsHelper = new AwsCredentialsHelper(AwsOptions);
        }

        private void LoadAwsConfigurationSources()
        {
            if (AwsOptions.UseSecrets)
            {
                DevonfwConfigurationBuilder.ConfigurationBuilder.AddSecretsHandler(AWSCredentials);
            }

            if (AwsOptions.UseParameterStore)
            {
                DevonfwConfigurationBuilder.ConfigurationBuilder.AddParameterStoreHandler(AWSCredentials);
            }

            if (AwsOptions.UseSqs)
            {
                ServiceCollection.AddSingleton<ISqsClientHandler, SqsClientHandler>();
            }

            if (AwsOptions.UseParameterStore || AwsOptions.UseSecrets)
            {
                DevonfwConfigurationBuilder.Configuration = DevonfwConfigurationBuilder.ConfigurationBuilder.Build();
            }
        }

        private void LoadAwsRegionEndpoint()
        {
            if (!string.IsNullOrEmpty(AwsOptions?.Credentials?.Region))
            {
                ServiceCollection.AddSingleton(AwsCredentialsHelper.LoadAwsRegionEndpoint());
            }
        }

        private void LoadAwsCredentials()
        {
            AWSCredentials = AwsCredentialsHelper?.LoadAwsCredentials();

            if (AWSCredentials != null)
            {
                ServiceCollection.AddSingleton(AWSCredentials);
            }
        }
        #endregion
    }
}