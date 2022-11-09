using Amazon.CDK.AWS.KMS;

namespace Devon4Net.Infrastructure.AWS.CDK.Resources.Handlers.Kms
{
    public interface IAwsCdkKmsHandler
    {
        IKey Create(string id, IKeyProps keyProps = null);
        IKey Locate(string id, string keyArn);
    }
}