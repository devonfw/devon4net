using System.Text.Json.Serialization;

namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class RubygemsUploadComponent : UploadComponent
    {
        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "rubygems.asset" },
            };
        }
    }
}
