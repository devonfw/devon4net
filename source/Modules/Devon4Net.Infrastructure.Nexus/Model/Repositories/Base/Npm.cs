using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Npm
    {
        [JsonPropertyName("removeNonCataloged")]
        public bool RemoveNonCataloged { get; set; }
        [JsonPropertyName("removeQuarantined")]
        public bool RemoveQuarantined { get; set; }
    }
}
