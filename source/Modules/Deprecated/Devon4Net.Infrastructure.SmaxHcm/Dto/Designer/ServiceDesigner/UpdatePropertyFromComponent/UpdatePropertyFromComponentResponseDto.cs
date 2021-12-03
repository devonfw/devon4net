﻿using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.UpdatePropertyFromComponent
{
    public class UpdatePropertyFromComponentResponseDto
    {
        [JsonPropertyName("@created")]
        public string created { get; set; }

        [JsonPropertyName("@modified")]
        public string modified { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public UpdatePropertyFromComponentResponseDtoExt ext { get; set; }

        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }
        public string property_type { get; set; }
        public object property_value { get; set; }
        public bool upgradeLocked { get; set; }
        public bool visibleWhenDeployDesign { get; set; }
        public bool requiredWhenDeployDesign { get; set; }
        public object[] bindings { get; set; }
        public UpdatePropertyFromComponentResponseDtoPropertyMeasure property_measure { get; set; }
        public string ownership { get; set; }
        public UpdatePropertyFromComponentResponseDtoOwner owner { get; set; }
    }

    public class UpdatePropertyFromComponentResponseDtoExt
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
        public bool csa_confidential { get; set; }
        public bool csa_consumer_visible { get; set; }
    }

    public class UpdatePropertyFromComponentResponseDtoPropertyMeasure
    {
        public bool enabled { get; set; }
        public object type { get; set; }
        public object unit { get; set; }
    }

    public class UpdatePropertyFromComponentResponseDtoOwner
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        [JsonPropertyName("@type")]
        public string type { get; set; }

        [JsonPropertyName("@created")]
        public string created { get; set; }

        [JsonPropertyName("@modified")]
        public string modified { get; set; }
        public string global_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public UpdatePropertyFromComponentResponseDtoExt1 ext { get; set; }
    }

    public class UpdatePropertyFromComponentResponseDtoExt1
    {
        public string csa_name_key { get; set; }
        public bool csa_critical_system_object { get; set; }
        public object csa_parent { get; set; }
        public bool csa_pattern { get; set; }
        public bool csa_consumer_visible { get; set; }
        public bool csa_designer_visible { get; set; }
    }

}
