namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class HelmUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "helm.asset" },
            };
        }
    }
}
