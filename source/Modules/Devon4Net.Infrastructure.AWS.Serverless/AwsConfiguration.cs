using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amazon;
using Devon4Net.Infrastructure.Common;
using Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager;
using Devon4Net.Infrastructure.AWS.Common.Managers.ParameterStoreManager;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.AWS.SQS.Interfaces;
using Devon4Net.Infrastructure.AWS.SQS.Handlers;
using Devon4Net.Infrastructure.Logger;
using Devon4Net.Infrastructure.AWS.Common.Helpers;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.API;

namespace Devon4Net.Infrastructure.AWS.Serverless
{
    public static class AwsConfiguration
    {
        private static AwsOptions AwsOptions { get; set; }
        private static AwsCredentialsHelper AwsCredentialsHelper { get; set; }

        public static void SetupDevonfwAws(this IServiceCollection services, IConfiguration configuration, bool setupDevon = false)
        {
            if (setupDevon)
            {
                services.SetupDevonfw(configuration);
            }

            AwsOptions = services.GetTypedOptions<AwsOptions>(configuration, OptionsDefinition.AwsOptions);
            AwsCredentialsHelper = new AwsCredentialsHelper(AwsOptions);

            if (!AwsOptions.EnableAws) return;

            GetAwsConfiguration(out var credentials, out var awsRegion);

            if (credentials == null) RaiseCredentialsException();

            services.SetupAwsLog(configuration, credentials);

            if (AwsOptions.UseSecrets)
            {
                (configuration as IConfigurationBuilder)?.AddSecretsHandler(credentials, awsRegion);
            }

            if (AwsOptions.UseParameterStore)
            {
                (configuration as IConfigurationBuilder)?.AddParameterStoreHandler(credentials, awsRegion);
            }

            if (AwsOptions.UseSqs)
            {
                services.AddSingleton<ISqsClientHandler, SqsClientHandler>();
            }

            AddConfigurationToDependencyInjection(services, credentials, awsRegion);
        }

        private static void AddConfigurationToDependencyInjection(IServiceCollection services, AWSCredentials credentials, RegionEndpoint awsRegion)
        {
            services.AddSingleton(credentials);
            if (awsRegion != null) services.AddSingleton(awsRegion);
        }

        private static void GetAwsConfiguration(out AWSCredentials credentials, out RegionEndpoint awsRegion)
        {
            credentials = AwsCredentialsHelper.LoadAwsCredentials();
            awsRegion = AwsCredentialsHelper.LoadAwsRegionEndpoint();
        }

        private static void RaiseCredentialsException()
        {
            Devon4NetLogger.Error("The AWS Credentials are null");
            throw new AWSCommonRuntimeException("The AWS Credentials can not be null. Please check the AWS options in the configuration file");
        }
    }
}