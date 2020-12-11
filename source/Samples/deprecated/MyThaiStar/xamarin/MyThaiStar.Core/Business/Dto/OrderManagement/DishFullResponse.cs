namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class DishFullResponse
    {
        public long id { get; set; }
        public int? modificationCounter { get; set; }
        public int? revision { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int? imageId { get; set; }
    }
}