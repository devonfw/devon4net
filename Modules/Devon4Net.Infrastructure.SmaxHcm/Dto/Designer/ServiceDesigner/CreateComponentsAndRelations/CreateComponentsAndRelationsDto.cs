namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations
{
    public class CreateComponentsAndRelationsDto
    {
        public CreateComponentsAndRelationsDto_Node[] nodes { get; set; }
        public CreateComponentsAndRelationsDto_Relationship[] relationships { get; set; }
    }

    public class CreateComponentsAndRelationsDto_Node
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public int orderIndex { get; set; }
        public string typeId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class CreateComponentsAndRelationsDto_Relationship
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string relationshipTypeId { get; set; }
        public string sourceName { get; set; }
        public string targetName { get; set; }
    }
}
