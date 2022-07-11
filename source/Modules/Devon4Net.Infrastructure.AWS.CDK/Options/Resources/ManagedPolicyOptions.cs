namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class ManagedPolicyOptions
    {
        public string Id { get; set; }
        public bool? LocateInsteadOfCreate { get; set; }
        public string PolicyDocumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }
}
