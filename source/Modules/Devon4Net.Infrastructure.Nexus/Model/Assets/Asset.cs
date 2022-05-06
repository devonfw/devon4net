using System;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Assets
{
    public class Asset
    {
        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("repository")]
        public string Repository { get; set; }
        [JsonPropertyName("format")]
        public string Format { get; set; }
        [JsonPropertyName("checksum")]
        public Checksum Checksum { get; set; }
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
        [JsonPropertyName("lastModified")]
        public DateTime? LastModified { get; set; }
    }
}