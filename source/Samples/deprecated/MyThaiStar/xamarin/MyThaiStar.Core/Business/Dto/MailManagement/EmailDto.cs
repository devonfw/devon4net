using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.MailManagement
{
    public class EmailDto
    {
        [JsonProperty(PropertyName = "emailFrom")]
        public string EmailFrom { get; set; }

        /// <summary>
        /// Dictionary MD5 Token, email host
        /// </summary>
        [JsonProperty(PropertyName = "emailAndTokenTo")]
        public Dictionary<string, string> EmailAndTokenTo { get; set; }


        /// <summary>
        /// 0 - CreateBooking,
        /// 1 - InvitedGuest,
        /// 2 - InvitedHost,
        /// 3 - Order
        /// </summary>
        [JsonProperty(PropertyName = "emailType")]
        public EmailTypeEnum EmailType { get; set; }


        /// <summary>
        /// List of different elements of the chosen menu
        /// </summary>
        [JsonProperty(PropertyName = "detailMenu")]
        public List<string> DetailMenu { get; set; }

        [JsonProperty(PropertyName = "bookingDate")]
        public DateTime BookingDate { get; set; }

        [JsonProperty(PropertyName = "assistants")]
        public int Assistants { get; set; }

        [JsonProperty(PropertyName = "bookingToken")]
        public string BookingToken { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }


        /// <summary>
        /// Dictionary URL, text Button
        /// </summary>
        [JsonProperty(PropertyName = "buttonActionList")]
        public Dictionary<string, string> ButtonActionList { get; set; }


        /// <summary>
        /// Dictionary email, name
        /// </summary>
        [JsonProperty(PropertyName = "host")]
        public Dictionary<string, string> Host { get; set; }


    }
}
