using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.Common.Managers.SecretsManager
{
    public static class AwsSecretsConfigurationBuilder
    {
        public static IConfigurationBuilder AddSecretsHandler(this IConfigurationBuilder configurationBuilder, AWSCredentials awsCredentials= null, RegionEndpoint regionEndpoint = null)
        {
            configurationBuilder.Add(new AwsSecretsConfigurationSource(awsCredentials, regionEndpoint));
            return configurationBuilder;
        }
    }
}
