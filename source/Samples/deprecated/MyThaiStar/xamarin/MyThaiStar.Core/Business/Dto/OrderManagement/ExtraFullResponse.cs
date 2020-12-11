namespace MyThaiStar.Core.Business.Dto.OrderManagement
{
    public class ExtraFullResponse
    {
        public int id { get; set; }
        public int modificationCounter { get; set; }
        public object revision { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
    }
}