using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Yum
    {
        [JsonPropertyName("repodataDepth")]
        public int RepodataDepth { get; set; }
        [JsonPropertyName("deployPolicy")]
        public string DeployPolicy { get; set; }
    }

    public class YumSigning
    {
        [JsonPropertyName("keypair")]
        public string Keypair { get; set; }
        [JsonPropertyName("passphrase")]
        public string Passphrase { get; set; }
    }
}
