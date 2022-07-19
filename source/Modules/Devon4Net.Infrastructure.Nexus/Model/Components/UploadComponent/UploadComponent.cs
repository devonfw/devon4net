namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public abstract class UploadComponent
    {
        public string AssetPath { get; set; }
        public string RepositoryName{ get; set; }

        public abstract MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent);
    }
}
