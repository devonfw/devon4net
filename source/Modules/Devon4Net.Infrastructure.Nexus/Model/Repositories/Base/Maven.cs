using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Maven
    {
        [JsonPropertyName("versionPolicy")]
        public string VersionPolicy { get; set; }
        [JsonPropertyName("layoutPolicy")]
        public string LayoutPolicy { get; set; }
        [JsonPropertyName("contentDisposition")]
        public string ContentDisposition { get; set; }
    }
}



