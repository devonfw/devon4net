namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class RUploadComponent : UploadComponent
    {
        public string PathId { get; set; }

        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "r.asset" },
                { new StringContent(PathId), "r.asset.pathId" },
            };
        }
    }
}
