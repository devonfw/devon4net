using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.Base
{
    public class NugetProxy
    {
        [JsonPropertyName("queryCacheItemMaxAge")]
        public int QueryCacheItemMaxAge { get; set; }
        [JsonPropertyName("nugetVersion")]
        public string NugetVersion { get; set; }
    }
}
