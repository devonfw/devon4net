namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class LambdaPolicyOptions
    {
        public string Id { get; set; }
        public string FunctionId { get; set; }
        public LambdaPolicyStatementOptions[] PolicyStatements { get; set; }
    }
}
