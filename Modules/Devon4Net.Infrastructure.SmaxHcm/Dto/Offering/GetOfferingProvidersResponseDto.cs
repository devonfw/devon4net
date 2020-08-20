using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Offering
{
    public class GetOfferingProvidersResponseDto
    {
        public string completionStatus { get; set; }
        public GetOfferingProvidersResponseDto_Result[] results { get; set; }
        public int totalCount { get; set; }
    }

    public class GetOfferingProvidersResponseDto_Result
    {
        public string version { get; set; }
        public GetOfferingProvidersResponseDto_Tag[] tags { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
    }

    public class GetOfferingProvidersResponseDto_Tag
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
