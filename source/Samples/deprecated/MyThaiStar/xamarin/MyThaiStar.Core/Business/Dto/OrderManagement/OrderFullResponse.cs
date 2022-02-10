namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderFullResponse
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public long bookingId { get; set; }
        public long? invitedGuestId { get; set; }
        public object bookingToken { get; set; }
        public int? hostId { get; set; }
    }
}