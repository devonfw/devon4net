using Devon4Net.Infrastructure.Nexus.Model.Repositories.Base;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Repositories.GroupRepositories
{
    public class RawGroupRepository : NexusRepositoryGroup
    {
        [JsonPropertyName("raw")]
        public Raw Raw { get; set; }
    }
}
