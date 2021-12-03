using System.Text;
using System.Text.Json;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Devon4Net.Infrastructure.AWS.Common.Helper;

namespace Devon4Net.Infrastructure.AWS.Lambda.Handlers
{
    public class LambdaClientHandler : ILambdaClientHandler
    {
        private string AwsRegion { get; }
        private string AwsSecretAccessKeyId { get; }
        private string AwsSecretAccessKey { get; }

        public LambdaClientHandler(string awsRegion, string awsSecretAccessKeyId, string awsSecretAccessKey)
        {
            AwsRegion = awsRegion;
            AwsSecretAccessKey = awsSecretAccessKey;
            AwsSecretAccessKeyId = awsSecretAccessKeyId;
        }

        public async Task<TOutput> Invoke<TInput,TOutput>(string functionName, TInput inputParam, InvocationType invocationType = null)
        {
            var jsonHelper = new JsonHelper();
            var lambdaConfig = new AmazonLambdaConfig() { RegionEndpoint = RegionEndpoint.GetBySystemName(AwsRegion) };
            var awsClient = new AmazonLambdaClient(AwsSecretAccessKeyId, AwsSecretAccessKey, lambdaConfig);

            var response = await awsClient.InvokeAsync(new InvokeRequest
            {
                FunctionName = functionName,
                InvocationType = invocationType ?? InvocationType.RequestResponse,
                Payload = JsonSerializer.Serialize(inputParam)
            }).ConfigureAwait(false);

            return jsonHelper.Deserialize<TOutput>(Encoding.Default.GetString(response.Payload.ToArray()));
        }
    }
}
