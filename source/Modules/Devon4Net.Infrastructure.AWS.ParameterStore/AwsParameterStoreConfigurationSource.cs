using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.ParameterStore
{
    public class AwsParameterStoreConfigurationSource : IConfigurationSource
    {
        private readonly AWSCredentials _awsCredentials;
        private readonly RegionEndpoint _regionEndpoint;

        public AwsParameterStoreConfigurationSource(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            _awsCredentials = awsCredentials;
            _regionEndpoint = regionEndpoint;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AwsParameterStoreConfigurationProvider(_awsCredentials, _regionEndpoint);
        }
    }
}
