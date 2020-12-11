using System;
using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{
    public class BookingDtoValues
    {
        [JsonProperty(PropertyName = "bookingDate")]
        public DateTime BookingDate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "bookingType")]
        public int BookingType { get; set; }

        [JsonProperty(PropertyName = "assistants")]
        public int Assistants { get; set; }
    }
}