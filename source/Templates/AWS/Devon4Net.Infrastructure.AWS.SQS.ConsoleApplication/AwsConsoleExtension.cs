using Devon4Net.Infrastructure.AWS.Common.Helpers;
using Devon4Net.Infrastructure.AWS.Common.Options;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Business.SQSManagement.Consumers;
using Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication.Common.Consts;
using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.Console;
using Devon4Net.Infrastructure.Common.Constants;
using Devon4Net.Infrastructure.Common.Handlers;
using Devon4Net.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;


namespace Devon4Net.Infrastructure.AWS.SQS.ConsoleApplication
{
    public class AwsConsoleExtension : DevonfwConsole
    {
        private AwsCredentialsHelper AwsCredentialsHelper { get; set; }
        private AwsOptions AwsOptions { get; set; }

        protected override void ConfigureServices(IServiceCollection services)
        {
            AwsOptions = services.GetTypedOptions<AwsOptions>(DevonfwConfigurationBuilder.Configuration, OptionsDefinition.AwsOptions);
            AwsCredentialsHelper = new AwsCredentialsHelper(AwsOptions);

            var awsCredentials = AwsCredentialsHelper.LoadAwsCredentials();
            if (awsCredentials != null) services.AddSingleton(awsCredentials);

            var awsRegion = AwsCredentialsHelper.LoadAwsRegionEndpoint();
            if (awsRegion != null) services.AddSingleton(awsRegion);

            services.SetupLog(DevonfwConfigurationBuilder.Configuration);
            services.SetupSqs(DevonfwConfigurationBuilder.Configuration);
            services.AddSqsConsumer<SqsConsumerSample>(SqsSampleConsts.QueueName);
            services.AddScoped<SqsSample>();
        }
    }
}
