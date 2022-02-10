namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class SsmParameterOptions
    {
        public string Id { get; set; }      
        public string Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdvancedTier { get; set; }
        public bool IsStringList { get; set; }
        public bool LocateInsteadOfCreate { get; set; }
    }
}
