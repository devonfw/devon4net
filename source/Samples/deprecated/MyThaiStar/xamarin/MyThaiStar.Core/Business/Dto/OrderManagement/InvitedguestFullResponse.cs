namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class InvitedguestFullResponse
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public long bookingId { get; set; }
        public string guestToken { get; set; }
        public string email { get; set; }
        public bool accepted { get; set; }
        public string modificationDate { get; set; }
    }
}