using Amazon;
using Amazon.Runtime;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Devon4Net.Infrastructure.AWS.Common.Managers.ParameterStoreManager.Interfaces;

namespace Devon4Net.Infrastructure.AWS.Common.Managers.ParameterStoreManager.Handlers
{
    public class AwsParameterStoreHandler : IAwsParameterStoreHandler
    {
        private readonly AmazonSimpleSystemsManagementClient _amazonSimpleSystemsManagementClient;
        private bool _disposed;

        public AwsParameterStoreHandler(AWSCredentials awsCredentials = null, RegionEndpoint regionEndpoint = null)
        {
            _amazonSimpleSystemsManagementClient = GetAmazonSystemsManagementClient(awsCredentials, regionEndpoint);
            _disposed = false;
        }

        public async Task<List<ParameterMetadata>> GetAllParameters(CancellationToken cancellationToken = default)
        {
            var parameters = new List<ParameterMetadata>();
            DescribeParametersResponse describeParametersResponse = null;

            do
            {
                try
                {
                    describeParametersResponse = await _amazonSimpleSystemsManagementClient.DescribeParametersAsync(new DescribeParametersRequest { NextToken = describeParametersResponse?.NextToken }, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new ParameterNotFoundException($"The AWS parameters could not be retrieved from the Parameters Store, message: {ex.Message}", ex);
                }

                var statusCode = (int)describeParametersResponse.HttpStatusCode;
                if (statusCode < 200 || statusCode > 299)
                {
                    throw new ParameterNotFoundException($"The AWS parameters could not be retrieved from the Parameters Store, error status code: {statusCode}");
                }

                parameters.AddRange(describeParametersResponse.Parameters);
            } while (!string.IsNullOrWhiteSpace(describeParametersResponse.NextToken));

            return parameters;
        }

        public async Task<string> GetParameterValue(string parameterName, CancellationToken cancellationToken = default)
        {
            GetParameterResponse getParameterResponse;
            try
            {
                getParameterResponse = await _amazonSimpleSystemsManagementClient.GetParameterAsync(new GetParameterRequest
                {
                    Name = parameterName,
                    WithDecryption = true
                }, cancellationToken).ConfigureAwait(false);
            } catch (Exception ex)
            {
                throw new ParameterNotFoundException($"The AWS parameter {parameterName} could not be retrieved from the Parameters Store, message: {ex.Message}", ex);
            }

            var statusCode = (int)getParameterResponse.HttpStatusCode;
            if (statusCode < 200 || statusCode > 299)
            {
                throw new ParameterNotFoundException($"The AWS parameter {parameterName} could not be retrieved from the Parameters Store, error status code: {statusCode}");
            }

            return getParameterResponse.Parameter.Value;
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
                _amazonSimpleSystemsManagementClient.Dispose();
            }
            _disposed = true;
        }

        private static AmazonSimpleSystemsManagementClient GetAmazonSystemsManagementClient(AWSCredentials awsCredentials, RegionEndpoint regionEndpoint)
        {
            if (awsCredentials != null && regionEndpoint != null)
            {
                return new AmazonSimpleSystemsManagementClient(awsCredentials, regionEndpoint);
            }

            return awsCredentials != null ? new AmazonSimpleSystemsManagementClient(awsCredentials) : new AmazonSimpleSystemsManagementClient();
        }
    }
}
