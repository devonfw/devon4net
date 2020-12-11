using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class GetOfferingProvidersResponseDto
    {
        public string completionStatus { get; set; }
        public GetOfferingProvidersResponseDtoResult[] results { get; set; }
        public int totalCount { get; set; }
    }

    public class GetOfferingProvidersResponseDtoResult
    {
        public string version { get; set; }
        public GetOfferingProvidersResponseDtoTag[] tags { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
    }

    public class GetOfferingProvidersResponseDtoTag
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
    }

}
