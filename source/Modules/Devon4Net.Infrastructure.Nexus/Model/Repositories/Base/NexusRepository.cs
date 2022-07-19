using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class NexusRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("online")]
        public bool Online { get; set; }
        [JsonPropertyName("storage")]
        public Storage Storage { get; set; }
    }

    public class Storage
    {
        [JsonPropertyName("blobStoreName")]
        public string BlobStoreName { get; set; }
        [JsonPropertyName("strictContentTypeValidation")]
        public bool StrictContentTypeValidation { get; set; }
        [JsonPropertyName("writePolicy")]
        public string WritePolicy { get; set; }
    }
}
