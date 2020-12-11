using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{
    public class BookingDto
    {
        [JsonProperty(PropertyName = "booking")]
        public BookingDtoValues Booking { get; set; }

        [JsonProperty(PropertyName = "invitedGuests")]
        public Invitedguest[] InvitedGuests { get; set; }
    }
}
