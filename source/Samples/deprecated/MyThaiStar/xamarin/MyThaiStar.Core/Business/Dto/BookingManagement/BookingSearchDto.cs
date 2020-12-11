using MyThaiStar.Core.Business.Dto.General;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{
    public class BookingSearchDto
    {
        public string email { get; set; }
        public string bookingToken { get; set; }
        public Pagination pagination { get; set; }
        public object[] sort { get; set; }
    }    

}