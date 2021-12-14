using Amazon;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.ParameterStore
{
    public class AwsParameterStoreConfigurationProvider : ConfigurationProvider, IDisposable
    {
        private bool _disposed;
        private IAwsParameterStoreHandler _awsParameterStoreHandler { get; set; }

        public AwsParameterStoreConfigurationProvider(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            _awsParameterStoreHandler = new AwsParameterStoreHandler(awsCredentials,regionEndpoint);
            _disposed = false;
        }
        public override void Load()
        {
            base.Load();
            Data = GetAwsParameterStoreData(default).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _awsParameterStoreHandler.Dispose();
            }
            _disposed = true;
        }

        private async Task<Dictionary<string, string>> GetAwsParameterStoreData(CancellationToken cancellationToken)
        {
            var parameters = await _awsParameterStoreHandler.GetAllParameters(cancellationToken).ConfigureAwait(false);
            var result = new Dictionary<string, string>();

            foreach (var parameter in parameters.Select(p=>p.Name))
            {
                var parameterValue = await _awsParameterStoreHandler.GetParameterValue(parameter, cancellationToken).ConfigureAwait(false);
                result.Add(parameter, parameterValue);
            }

            return result;
        }
    }
}
