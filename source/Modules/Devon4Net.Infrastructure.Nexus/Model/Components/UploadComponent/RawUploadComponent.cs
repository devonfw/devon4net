using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;

namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class RawUploadComponent : UploadComponent
    {
        public string Directory { get; set; }
        public string Filename { get; set; }

        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "raw.asset1" },
                { new StringContent(Directory), "raw.directory" },
                { new StringContent(Filename), "raw.asset1.filename" }
            };
        }
    }
}
