using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignContainer
{
    public class CreateDesignContainerRequestDto
    {
        public string container_type { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public CreateDesignContainerRequestDtoTag[] tags { get; set; }
    }

    public class CreateDesignContainerRequestDtoTag
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }
    }
}
