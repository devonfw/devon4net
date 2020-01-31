namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class OrderlineDetailFullResponse
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public long orderId { get; set; }
        public long dishId { get; set; }
        public int amount { get; set; }
        public string comment { get; set; }
    }
}