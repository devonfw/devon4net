using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace Devon4Net.Infrastructure.AWS.Lambda.Handlers
{
    public class LambdaClientHandler : ILambdaClientHandler
    {
        private const string BuiltInTypeObjectNames = "String, DateTime, DateTimeKind, DateTimeOffset, AsyncCallback, AttributeTargets, AttributeUsageAttribute, Boolean, Byte, Char, CharEnumerator, Base64FormattingOptions, DayOfWeek, DBNull, Decimal, Double, EnvironmentVariableTarget, EventHandler, GCCollectionMode, Guid, Int16, Int32, Int64, IntPtr, SByte, Single, TimeSpan, TimeZoneInfo, TypeCode, UInt16, UInt32, UInt64, UIntPtr";
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
            var lambdaConfig = new AmazonLambdaConfig() { RegionEndpoint = RegionEndpoint.GetBySystemName(AwsRegion) };
            var awsClient = new AmazonLambdaClient(AwsSecretAccessKeyId, AwsSecretAccessKey, lambdaConfig);

            var response = await awsClient.InvokeAsync(new InvokeRequest
            {
                FunctionName = functionName,
                InvocationType = invocationType == null ? InvocationType.RequestResponse : invocationType,
                Payload = JsonSerializer.Serialize(inputParam)
            });
            
            return  Deserialize<TOutput>(Encoding.Default.GetString(response.Payload.ToArray()));
        }

        private T Deserialize<T>(string input)
        {   
            return string.IsNullOrEmpty(input)
                ? default
                : BuiltInTypeObjectNames.Contains(typeof(T).Name) ? (T)Convert.ChangeType(input, typeof(T)) : JsonSerializer.Deserialize<T>(input);
        }
    }
}
