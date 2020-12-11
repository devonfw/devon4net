namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class BookingFullResponse
    {
        public int id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public string name { get; set; }
        public string bookingToken { get; set; }
        public string comment { get; set; }
        public long bookingDate { get; set; }
        public long expirationDate { get; set; }
        public long creationDate { get; set; }
        public string email { get; set; }
        public bool canceled { get; set; }
        public string bookingType { get; set; }
        public int tableId { get; set; }
        public int? orderId { get; set; }
        public int? assistants { get; set; }
        public int userId { get; set; }
    }
}