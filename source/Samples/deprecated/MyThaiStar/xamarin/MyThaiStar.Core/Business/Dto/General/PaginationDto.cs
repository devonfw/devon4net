using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.General
{
    public class Pagination
    {
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }
        [JsonProperty(PropertyName = "total")]
        public object Total { get; set; }
    }
}