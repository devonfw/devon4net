using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner
{
    public class GetComponentTemplatesFromComponentTypeResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }

        [JsonPropertyName("@total_results")]
        public int total_results { get; set; }

        public GetComponentTemplatesFromComponentTypeResponseDtoMember[] members { get; set; }
    }

    public class GetComponentTemplatesFromComponentTypeResponseDtoMember
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }
        public string created { get; set; } // There are two fields in this response: created and @created, right now, they have the same value, this one corresponds to: created
        
        [JsonPropertyName("@created")]
        public string created_2 { get; set; } // There are two fields in this response: created and @created, right now, they have the same value, this one corresponds to: @created
        
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public GetComponentTemplatesFromComponentTypeResponseDtoExt ext { get; set; }
        public GetComponentTemplatesFromComponentTypeResponseDtoAncestor ancestor { get; set; }
    }

    public class GetComponentTemplatesFromComponentTypeResponseDtoAncestor
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }

        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public GetComponentTemplatesFromComponentTypeResponseDtoExt ext { get; set; }
    }

    public class GetComponentTemplatesFromComponentTypeResponseDtoExt
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
        public object csa_parent { get; set; }
        public bool csa_pattern { get; set; }
        public bool csa_consumer_visible { get; set; }
        public bool csa_designer_visible { get; set; }
    }

}
