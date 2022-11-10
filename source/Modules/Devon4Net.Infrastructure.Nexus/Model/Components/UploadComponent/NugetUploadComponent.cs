namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class NugetUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "nuget.asset" },
            };
        }
    }
}
