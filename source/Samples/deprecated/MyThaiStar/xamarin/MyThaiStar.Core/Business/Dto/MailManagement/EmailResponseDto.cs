namespace MyThaiStar.Core.Business.Dto.MailManagement
{
    public class EmailResponseDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public long bookingId { get; set; }
        public string guestToken { get; set; }
        public string email { get; set; }
        public bool accepted { get; set; }
        public object modificationDate { get; set; }
    }
}