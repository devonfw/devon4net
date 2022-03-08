using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.AWS.SQS.Dto
{
    public class RedrivePolicy
    {
        [JsonPropertyName("maxReceiveCount")]
        public int MaxReceiveCount { get; set; }

        [JsonPropertyName("deadLetterTargetArn")]
        public string DeadLetterQueueUrl { get; set; }
    }
}
