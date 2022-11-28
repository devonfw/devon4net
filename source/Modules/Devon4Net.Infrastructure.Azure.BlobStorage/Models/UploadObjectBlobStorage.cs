namespace Devon4Net.Infrastructure.Azure.BlobStorage.Models
{
    public class UploadObjectBlobStorage
    {
        public Stream Stream { get; set; }
        public string ContainerName { get; set; }
        public string KeyName { get; set; }
        public string ContentType { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IDictionary<string, string> Tags { get; set; }
    }
}
