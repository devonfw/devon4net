using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner
{
    public class GetServiceDesignerMetamodelResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Palette[] palettes { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Componenttype[] componentTypes { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Relationshiptype[] relationshipTypes { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Palette
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string icon { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Componenttype
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string icon { get; set; }
        public string[] tags { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Type[] types { get; set; }
        public string[] paletteIds { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Type
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Relationshiptype
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
        public GetServiceDesignerMetamodelResponseDto_Sourcetype sourceType { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Sourcetype[] sourceTypes { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Targettype targetType { get; set; }
        public GetServiceDesignerMetamodelResponseDto_Targettype[] targetTypes { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Sourcetype
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GetServiceDesignerMetamodelResponseDto_Targettype
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
