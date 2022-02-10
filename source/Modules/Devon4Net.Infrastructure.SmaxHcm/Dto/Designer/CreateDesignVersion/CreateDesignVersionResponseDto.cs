using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.CreateDesignVersion
{
    public class CreateDesignVersionResponseDto
    {
        public string self { get; set; }
        [JsonPropertyName("type")]
        public string type { get; set; } // There are 2 "duplicated" fields in the response, type and @type, this one corresponds to: type
        public string content_version { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public CreateDesignVersionResponseDtoExt ext { get; set; }
        public string url { get; set; }
        public string version { get; set; }
        public bool published { get; set; }
        public string containerId { get; set; }
        public CreateDesignVersionResponseDtoOptionmodel optionModel { get; set; }
        [JsonPropertyName("@type")]
        public string type_2 { get; set; } // There are 2 "duplicated" fields in the response, type and @type, this one corresponds to: @type
        public object[] upgrades_from { get; set; }
        public object[] upgrades_to { get; set; }
    }

    public class CreateDesignVersionResponseDtoExt
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
    }

    public class CreateDesignVersionResponseDtoOptionmodel
    {
        public string self { get; set; }
    }

}
