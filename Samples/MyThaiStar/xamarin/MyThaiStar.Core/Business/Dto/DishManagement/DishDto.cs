namespace MyThaiStar.Core.Business.Dto.DishManagement
{
    public class DishDto
    {
        public long id { get; set; }
        public int modificationCounter { get; set; }
        public int? revision { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal? price { get; set; }
        public long? imageId { get; set; }
    }
}