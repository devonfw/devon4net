namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class YumUploadComponent : UploadComponent
    {
        public string Directory { get; set; }
        public string Filename { get; set; }
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "yum.asset" },
                { new StringContent(Directory), "yum.directory" },
                { new StringContent(Filename), "yum.asset.filename" },
            };
        }
    }
}
