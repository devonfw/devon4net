using Newtonsoft.Json;

namespace fyiReporting.RDL
{
    /// <summary>
    /// 
    /// </summary>
    public class Param
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
        [JsonProperty(PropertyName = "type")]
        public ParamType Type { get; set; }
        [JsonProperty(PropertyName = "dataset")]
        public string Dataset { get; set; }
    }
}