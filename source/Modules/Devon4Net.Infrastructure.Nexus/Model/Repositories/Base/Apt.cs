using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Apt
    {
        [JsonPropertyName("distribution")]
        public string Distribution { get; set; }
        [JsonPropertyName("flat")]
        public bool Flat { get; set; }
    }

    public class Aptsigning
    {
        [JsonPropertyName("keypair")]
        public string Keypair { get; set; }
        [JsonPropertyName("passphrase")]
        public string Passphrase { get; set; }
    }
}
