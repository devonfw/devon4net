namespace Devon4Net.Infrastructure.Nexus.Model.Components.UploadComponent
{
    public class MavenUploadComponent : UploadComponent
    {
        public string GroupId { get; set; }
        public string ArtifactId { get; set; }
        public string Version { get; set; }
        public bool GeneratePom { get; set; }
        public string Packaging { get; set; }
        public string Classifier { get; set; }
        public string Extension { get; set; }

        public override MultipartFormDataContent GetMultiPartFormData(ByteArrayContent fileContent)
        {
            return new MultipartFormDataContent
            {
                { fileContent, "maven2.asset1" },
                { new StringContent(GroupId), "maven2.groupId" },
                { new StringContent(ArtifactId), "maven2.artifactId" },
                { new StringContent(Version), "maven2.version" },
                { new StringContent(GeneratePom.ToString()), "maven2.generate-pom" },
                { new StringContent(Packaging), "maven2.packaging" },
                { new StringContent(Classifier), "maven2.asset1.classifier" },
                { new StringContent(Extension), "maven2.asset1.extension" },
            };
        }
    }
}
