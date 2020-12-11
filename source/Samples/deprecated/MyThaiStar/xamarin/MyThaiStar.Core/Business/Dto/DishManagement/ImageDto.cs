namespace MyThaiStar.Core.Business.Dto.DishManagement
{
    public class ImageDto
    {
        public long id { get; set; }
        public int? modificationCounter { get; set; }
        public int? revision { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string contentType { get; set; }
        public string mimeType { get; set; }
    }
}