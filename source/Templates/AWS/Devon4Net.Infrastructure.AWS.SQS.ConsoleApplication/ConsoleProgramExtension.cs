using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Devon4Net.Infrastructure.AWS.Common.Consts;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Consumers;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Common.Consts;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication
{
    public class ConsoleProgramExtension
    {
        protected IConfigurationRoot Configuration { get; set; }
        protected IConfigurationBuilder ConfigurationBuilder { get; set; }
        protected IServiceProvider ServiceProvider { get; set; }
        protected IServiceCollection ServiceCollection = new ServiceCollection();
        protected AWSCredentials AWSCredentials { get; set; }
        protected AwsOptions AwsOptions { get; set; }

        public ConsoleProgramExtension()
        {
            SetupConfiguration();
            LoadAwsCredentials(ServiceCollection);
            LoadAwsRegionEndpoint(ServiceCollection);
            SetupServiceActions();
            ConfigureServices(ServiceCollection);
            FinalizeSetupServiceProviderActions();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.SetupLog(Configuration);
            services.SetupSqs(Configuration);
            services.AddSqsConsumer<SqsConsumerSample>(SqsSampleConsts.QueueName);
            services.AddScoped<SqsSample>();
        }

        public void GetConfigurationObjects(out IConfigurationRoot configuration, out IServiceCollection serviceCollection)
        {
            configuration = Configuration;
            serviceCollection = ServiceCollection;
        }

        public void GetAwsConfigurationObjects(out AWSCredentials awsCredentials, out AwsOptions awsOptions)
        {
            awsCredentials = AWSCredentials;
            awsOptions = AwsOptions;
        }

        protected void SetupConfiguration()
        {
            ConfigurationBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = ConfigurationBuilder.Build();

            ConfigurationBuilder= ConfigurationBuilder.AddJsonFile($"appsettings.{Configuration["Environment"]}.json", true, true);

            Configuration = ConfigurationBuilder.Build();
            AwsOptions = ServiceCollection.GetTypedOptions<AwsOptions>(Configuration, ConfigurationConsts.AwsOptionsNodeName);
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

        private void LoadAwsRegionEndpoint(IServiceCollection services)
        {
            if (!string.IsNullOrEmpty(AwsOptions.Credentials.Region))
            {
                services.AddSingleton(RegionEndpoint.GetBySystemName(AwsOptions.Credentials.Region));
            }
        }

        private void SetupServiceActions()
        {
            ServiceCollection.AddOptions();
            ServiceCollection.AddSingleton(Configuration);
            ServiceCollection.AddLogging(ConfigureLogging); //NOSONAR false positive
        }

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

        private void FinalizeSetupServiceProviderActions()
        {
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
    }
}