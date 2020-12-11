using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{
    public class BookingResponseDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "modificationCounter")]
        public int ModificationCounter { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public int? Revision { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "bookingToken")]
        public string BookingToken { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "bookingDate")]
        public string BookingDate { get; set; }

        [JsonProperty(PropertyName = "expirationDate")]
        public string ExpirationDate { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "canceled")]
        public bool Canceled { get; set; }

        [JsonProperty(PropertyName = "bookingType")]
        public string BookingType { get; set; }

        [JsonProperty(PropertyName = "tableId")]
        public long? TableId { get; set; }

        [JsonProperty(PropertyName = "orderId")]
        public long? OrderId { get; set; }

        [JsonProperty(PropertyName = "assistants")]
        public int? Assistants { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public long? UserId { get; set; }
    }
}