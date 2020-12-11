using System.Collections.Generic;

namespace MyThaiStar.Core.Business.Dto.BookingManagement
{


    public class BookingSearchResponse
    {
        public BookingSearchResponseDto booking { get; set; }
        public TableSearchDto table { get; set; }
        public List<InvitedguestSearchDto> invitedGuests { get; set; }
        public OrderSearchDto order { get; set; }
        public List<OrderSearchDto> orders { get; set; }
        public UserSearchDto user { get; set; }
    }

    public class BookingSearchResponseDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public string name { get; set; }
        public string bookingToken { get; set; }
        public string comment { get; set; }
        public string bookingDate { get; set; }
        public string expirationDate { get; set; }
        public string creationDate { get; set; }
        public string email { get; set; }
        public bool canceled { get; set; }
        public string bookingType { get; set; }
        public long? tableId { get; set; }
        public long? orderId { get; set; }
        public int? assistants { get; set; }
        public long? userId { get; set; }
    }

    public class TableSearchDto
    {
        public int id { get; set; }
        public int modificationCounter { get; set; }
        public int? revision { get; set; }
        public int seatsNumber { get; set; }
    }

    public class OrderSearchDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public int? revision { get; set; }
        public long? bookingId { get; set; }
        public long? invitedGuestId { get; set; }
        public object bookingToken { get; set; }
        public long? hostId { get; set; }
    }

    public class UserSearchDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public int? revision { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public long userRoleId { get; set; }
    }

    public class InvitedguestSearchDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public int? revision { get; set; }
        public long bookingId { get; set; }
        public string guestToken { get; set; }
        public string email { get; set; }
        public bool accepted { get; set; }
        public string modificationDate { get; set; }
    }

    //public class OrderInvitedSearchDto
    //{
    //    public long id { get; set; }
    //    public int modificationCounter { get; set; }
    //    public object revision { get; set; }
    //    public long bookingId { get; set; }
    //    public long? invitedGuestId { get; set; }
    //    public object bookingToken { get; set; }
    //    public int? hostId { get; set; }
    //}

}