using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.Common.Managers.ParameterStoreManager
{
    public static class AwsParameterStoreConfigurationBuilder
    {
        public static IConfigurationBuilder AddParameterStoreHandler(this IConfigurationBuilder configurationBuilder, AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            configurationBuilder.Add(new AwsParameterStoreConfigurationSource(awsCredentials, regionEndpoint));
            return configurationBuilder;
        }
    }
}
