using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations
{
    public class CreateComponentsAndRelationsResponseDto
    {
        [JsonPropertyName("@self")]
        public string self { get; set; }

        public Node[] nodes { get; set; }
        public Relationship[] relationships { get; set; }
        public object[] statusMessages { get; set; }
    }

    public class Node
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
    }

    public class Relationship
    {
        public string id { get; set; }
        public string name { get; set; }
        public string relationshipTypeId { get; set; }
        public string type { get; set; }
        public Source source { get; set; }
        public Target target { get; set; }
        public object[] statusMessages { get; set; }
    }

    public class Source
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Target
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
