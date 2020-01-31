namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    public class Image
    {
        public int Id { get; set; }
        public int ModificationCounter { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string MimeType { get; set; }
    }
}
