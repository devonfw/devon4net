namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Designer.ServiceDesigner.CreateComponentsAndRelations
{
    public class CreateComponentsAndRelationsRequestDto
    {
        public CreateComponentsAndRelationsRequestDtoNode[] nodes { get; set; }
        public CreateComponentsAndRelationsRequestDtoRelationship[] relationships { get; set; }
        public object[] groups { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDtoNode
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

    public class CreateComponentsAndRelationsRequestDtoRelationship
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string relationshipTypeId { get; set; }
        public CreateComponentsAndRelationsRequestDtoSource source { get; set; }
        public CreateComponentsAndRelationsRequestDtoTarget target { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDtoSource
    {
        public string name { get; set; }
    }

    public class CreateComponentsAndRelationsRequestDtoTarget
    {
        public string name { get; set; }
    }

}
