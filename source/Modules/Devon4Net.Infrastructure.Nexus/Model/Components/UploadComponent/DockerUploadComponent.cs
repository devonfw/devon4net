namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class DockerUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "docker.asset" },
            };
        }
    }
}
