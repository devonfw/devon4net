using Devon4Net.Infrastructure.Nexus.Model.Assets;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Components
{
    public class NexusComponent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("repository")]
        public string Repository { get; set; }
        [JsonPropertyName("format")]
        public string Format { get; set; }
        [JsonPropertyName("group")]
        public string Group { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("assets")]
        public List<Asset> Assets { get; set; }
    }
}