using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.HostedRepositories
{
    public class YumHostedRepository : RepositoryHosted
    {
        [JsonPropertyName("yum")]
        public Yum Yum { get; set; }
    }
}
