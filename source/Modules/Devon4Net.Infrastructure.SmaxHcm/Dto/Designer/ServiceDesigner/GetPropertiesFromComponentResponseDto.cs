using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner
{
    public class GetPropertiesFromComponentResponseDto
    {
        public GetPropertiesFromComponentResponseDtoMember[] members { get; set; }
    }

    public class GetPropertiesFromComponentResponseDtoMember
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public bool confidentialData { get; set; }
        public int minOccurs { get; set; }
        public int maxOccurs { get; set; }
        public bool modifiableDuringPackageDeploy { get; set; }
        public bool modifiableDuringPackageRedeploy { get; set; }
        public bool supportsModifyLifecycleAction { get; set; }
        public string measurableUnits { get; set; }
        public string measurableType { get; set; }
        public bool visibleWhenDeployDesign { get; set; }
        public bool requiredWhenDeployDesign { get; set; }
        public int optionReferenceCount { get; set; }
        public int lifecycleActionReferenceCount { get; set; }
        public int componentReferenceCount { get; set; }
        public int userOperationReferenceCount { get; set; }
        public int resourceOfferingPropertyMappingReferenceCount { get; set; }
        public int resourceOfferingMeasurablePropertyReferenceCount { get; set; }
        public int resourceOfferingProviderSelectionActionReferenceCount { get; set; }
        public int serviceDesignReferenceCount { get; set; }
        public int componentTemplateReferenceCount { get; set; }
        public object value { get; set; }
        public GetPropertiesFromComponentResponseDtoConstraint[] constraints { get; set; }
    }

    public class GetPropertiesFromComponentResponseDtoConstraint
    {
        [JsonPropertyName("@type")]
        public string type { get; set; }
        public int max_length { get; set; }
        public bool validate { get; set; }
    }

}
