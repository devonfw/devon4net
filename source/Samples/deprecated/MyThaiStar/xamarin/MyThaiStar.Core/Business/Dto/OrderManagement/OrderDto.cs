using System.Collections.Generic;

namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderDto
    {
        public OrderDtoBooking booking { get; set; }
        public List<OrderlineDto> orderLines { get; set; }
    }
}
