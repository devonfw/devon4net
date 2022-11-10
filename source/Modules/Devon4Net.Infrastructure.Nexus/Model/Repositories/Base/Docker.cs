using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Docker
    {
        [JsonPropertyName("v1Enabled")]
        public bool V1Enabled { get; set; }
        [JsonPropertyName("forceBasicAuth")]
        public bool ForceBasicAuth { get; set; }
        [JsonPropertyName("httpPort")]
        public int HttpPort { get; set; }
        [JsonPropertyName("httpsPort")]
        public int HttpsPort { get; set; }
    }

    public class DockerProxy
    {
        [JsonPropertyName("indexType")]
        public string IndexType { get; set; }
        [JsonPropertyName("indexUrl")]
        public string IndexUrl { get; set; }
    }
}