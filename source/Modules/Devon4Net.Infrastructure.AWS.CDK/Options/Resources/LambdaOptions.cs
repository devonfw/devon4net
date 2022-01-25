
using System.Collections.Generic;

namespace Devon4Net.Infrastructure.AWS.CDK.Options.Resources
{
    public class LambdaOptions
    {
        public string Id { get; set; }
        public string FunctionName { get; set; }
        public string Role { get; set; }
        public string SecurityGroupId { get; set; }
        public string FunctionHandler { get; set; }
        public string Runtime { get; set; }
        public string VpcId { get; set; }
        public string[] SubnetIds { get; set; }
        public LambdaEnvironmentVariableOptions[] LambdaEnvironmentVariables { get; set; }
        public LambdaSourceCodeOptions SourceCode { get; set; }
    }

    public class LambdaSourceCodeOptions
    {
        public string CodeZipFilePath { get; set; }
        public LambdaCodeSourceBucketOptions CodeBucket { get; set; }
    }

    public class LambdaCodeSourceBucketOptions
    {
        public string BucketName { get; set; }
        public string FilePath { get; set; }
    }

    public class LambdaEnvironmentVariableOptions
    {
        public string EnvironmentVariableName { get; set; }
        public string EnvironmentVariableValue { get; set; }
    }
}
