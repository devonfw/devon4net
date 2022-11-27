namespace Devon4Net.Infrastructure.AWS.CDK.Enums
{
    public enum PipelineActionType
    {
        s3Source,
        codebuild,
        cloudformation,
        ecrsource,
        ecsdeploy,
        lambdainvoke
    }
}
