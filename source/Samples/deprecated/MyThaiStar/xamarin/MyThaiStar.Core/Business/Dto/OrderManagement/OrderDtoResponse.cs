namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderDtoResponse
    {
        public int id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public int bookingId { get; set; }
        public object invitedGuestId { get; set; }
        public object bookingToken { get; set; }
        public object hostId { get; set; }
    }
}