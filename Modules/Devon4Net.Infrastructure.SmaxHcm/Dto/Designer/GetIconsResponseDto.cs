using System;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
{
    public class GetIconsResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@modified")]
        public string modified { get; set; }

        public GetIconsResponseDto_Member[] members { get; set; }

        [JsonPropertyName("@start_index")]
        public int start_index { get; set; }

        [JsonPropertyName("@items_per_page")]
        public int items_per_page { get; set; }

        [JsonPropertyName("@total_results")]
        public int total_results { get; set; }
    }

    public class GetIconsResponseDto_Member
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@modified")]
        public string modified { get; set; }

        [JsonPropertyName("@content_type")]
        public string content_type { get; set; }

        [JsonPropertyName("@content_length")]
        public int content_length { get; set; }
    }

}
