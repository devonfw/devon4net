namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    public class Dish
    {
        public int Id { get; set; }
        public int ModificationCounter { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int ImageId { get; set; }
    }
}
