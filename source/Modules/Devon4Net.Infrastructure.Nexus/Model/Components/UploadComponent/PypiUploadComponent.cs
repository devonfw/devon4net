namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class PypiUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "pypi.asset" },
            };
        }
    }
}
