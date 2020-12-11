using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.Secrets
{
    public class AwsSecretsConfigurationSource : IConfigurationSource
    {
        private AWSCredentials AwsCredentials { get; set; }
        private RegionEndpoint RegionEndpoint { get; set; }

        public AwsSecretsConfigurationSource(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            AwsCredentials = awsCredentials;
            RegionEndpoint = regionEndpoint;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsSecretsConfigurationProvider(AwsCredentials, RegionEndpoint);
        }
    }
}
