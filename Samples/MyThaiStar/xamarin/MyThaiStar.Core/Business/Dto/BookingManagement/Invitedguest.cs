using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{
    public class Invitedguest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}