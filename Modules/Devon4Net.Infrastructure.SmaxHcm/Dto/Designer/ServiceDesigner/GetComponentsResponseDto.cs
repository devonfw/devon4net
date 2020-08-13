using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner
{
    public class GetComponentsResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }
        public GetComponentsResponseDto_Node[] nodes { get; set; }
        public GetComponentsResponseDto_Relationship[] relationships { get; set; }
        public object[] statusMessages { get; set; }
    }

    public class GetComponentsResponseDto_Node
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public int orderIndex { get; set; }
        public string typeId { get; set; }
        public string[] tags { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public GetComponentsResponseDto_Statusmessage[] statusMessages { get; set; }
    }

    public class GetComponentsResponseDto_Statusmessage
    {
        public string type { get; set; }
        public string message { get; set; }
        public object[] applicableItems { get; set; }
        public object[] _params { get; set; }
        public string level { get; set; }
        public int priority { get; set; }
    }

    public class GetComponentsResponseDto_Relationship
    {
        public string id { get; set; }
        public string name { get; set; }
        public string relationshipTypeId { get; set; }
        public string type { get; set; }
        public GetComponentsResponseDto_Source source { get; set; }
        public GetComponentsResponseDto_Target target { get; set; }
        public object[] statusMessages { get; set; }
    }

    public class GetComponentsResponseDto_Source
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class GetComponentsResponseDto_Target
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}
