using System.Collections.Generic;
using MyThaiStar.Core.Business.Dto.BookingManagement;

namespace MyThaiStar.Core.Business.Dto.OrderManagement
{

    public class OrderSearchFullResponseDto
    {
        public OrderFullResponse order { get; set; }
        public BookingSearchResponseDto booking { get; set; }
        public InvitedguestFullResponse invitedGuest { get; set; }
        public List<OrderlineFullResponse> orderLines { get; set; }
        public HostFullResponse host { get; set; }
    }
}
