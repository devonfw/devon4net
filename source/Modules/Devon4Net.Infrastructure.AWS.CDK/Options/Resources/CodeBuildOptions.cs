namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class CodeBuildOptions
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string Role { get; set; }
        public string BuildSpecPath { get; set; }
        public bool? EnableIndependentTrigger { get; set; }
    }
}
