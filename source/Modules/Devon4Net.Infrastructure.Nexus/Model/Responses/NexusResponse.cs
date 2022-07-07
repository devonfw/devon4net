using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Responses
{
    public class NexusResponse<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
        [JsonPropertyName("continuationToken")]
        public string ContinuationToken { get; set; }

    }
}