namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class AptUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "apt.asset" },
            };
        }
    }
}
