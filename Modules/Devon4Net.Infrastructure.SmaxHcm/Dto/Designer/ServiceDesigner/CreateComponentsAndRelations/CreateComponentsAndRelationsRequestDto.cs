namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations
{
    public class CreateComponentsAndRelationsRequestDto
    {
        public CreateComponentsAndRelationsRequestDto_Node[] nodes { get; set; }
        public CreateComponentsAndRelationsRequestDto_Relationship[] relationships { get; set; }
        public object[] groups { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDto_Node
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public int orderIndex { get; set; }
        public string typeId { get; set; }
        public string[] tags { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public object[] statusMessages { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDto_Relationship
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string relationshipTypeId { get; set; }
        public CreateComponentsAndRelationsRequestDto_Source source { get; set; }
        public CreateComponentsAndRelationsRequestDto_Target target { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDto_Source
    {
        public string name { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDto_Target
    {
        public string name { get; set; }
    }

}
