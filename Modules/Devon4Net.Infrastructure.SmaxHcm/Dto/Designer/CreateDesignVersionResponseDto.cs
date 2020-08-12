using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer
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
        public CreateDesignVersionResponseDto_Ext ext { get; set; }
        public string url { get; set; }
        public string version { get; set; }
        public bool published { get; set; }
        public string containerId { get; set; }
        public CreateDesignVersionResponseDto_Optionmodel optionModel { get; set; }
        [JsonPropertyName("@type")]
        public string type_2 { get; set; } // There are 2 "duplicated" fields in the response, type and @type, this one corresponds to: @type
        public object[] upgrades_from { get; set; }
        public object[] upgrades_to { get; set; }
    }

    public class CreateDesignVersionResponseDto_Ext
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
    }

    public class CreateDesignVersionResponseDto_Optionmodel
    {
        public string self { get; set; }
    }

}
