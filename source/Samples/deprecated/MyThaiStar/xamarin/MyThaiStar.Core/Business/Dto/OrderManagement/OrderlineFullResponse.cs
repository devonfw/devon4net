using System.Collections.Generic;

namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderlineFullResponse
    {
        public OrderlineDetailFullResponse orderLine { get; set; }
        public object order { get; set; }
        public DishFullResponse dish { get; set; }
        public List<Extra> extras { get; set; }
    }
}