using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class Bower
    {
        [JsonPropertyName("rewritePackageUrls")]
        public bool RewritePackageUrls { get; set; }
    }
}
