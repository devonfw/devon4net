using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner
{
    public class GetServiceDesignerMetamodelResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }
        public GetServiceDesignerMetamodelResponseDtoPalette[] palettes { get; set; }
        public GetServiceDesignerMetamodelResponseDtoComponenttype[] componentTypes { get; set; }
        public GetServiceDesignerMetamodelResponseDtoRelationshiptype[] relationshipTypes { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoPalette
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string icon { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoComponenttype
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string icon { get; set; }
        public string[] tags { get; set; }
        public GetServiceDesignerMetamodelResponseDtoType[] types { get; set; }
        public string[] paletteIds { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoType
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoRelationshiptype
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int minOccursOnSource { get; set; }
        public int maxOccursOnSource { get; set; }
        public int minOccursOnTarget { get; set; }
        public int maxOccursOnTarget { get; set; }
        public GetServiceDesignerMetamodelResponseDtoSourcetype sourceType { get; set; }
        public GetServiceDesignerMetamodelResponseDtoSourcetype[] sourceTypes { get; set; }
        public GetServiceDesignerMetamodelResponseDtoTargettype targetType { get; set; }
        public GetServiceDesignerMetamodelResponseDtoTargettype[] targetTypes { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoSourcetype
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDtoTargettype
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
