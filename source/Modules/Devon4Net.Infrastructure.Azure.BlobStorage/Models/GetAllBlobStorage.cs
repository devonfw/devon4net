using Azure.Storage.Blobs.Models;

namespace Devon4Net.Infrastructure.Azure.BlobStorage.Models
{
    public class GetAllBlobItems
    {
        public IReadOnlyList<BlobItem> Blobs { get; set; }
        public string ContinuationToken { get; set; }
    }
}