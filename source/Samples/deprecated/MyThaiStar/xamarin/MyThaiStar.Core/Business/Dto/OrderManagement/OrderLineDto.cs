using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderlineDto
    {
        public OrderlineDtoDetail orderLine { get; set; }
        public Extra[] extras { get; set; }
    }

    public class OrderLineDto
    {
        [JsonProperty(PropertyName = "idDish")]
        public long IdDish { get; set; }

        /// <summary>
        /// Contains a list with the id of the extra ingredients that have been selected by the client
        /// </summary>
        [JsonProperty(PropertyName = "extras")]
        public List<long> Extras { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}