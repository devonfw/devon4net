using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Assets
{
    public class Checksum
    {
        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; }
        [JsonPropertyName("sha256")]
        public string Sha256 { get; set; }
        [JsonPropertyName("sha512")]
        public string Sha512 { get; set; }
        [JsonPropertyName("md5")]
        public string Md5 { get; set; }
    }
}