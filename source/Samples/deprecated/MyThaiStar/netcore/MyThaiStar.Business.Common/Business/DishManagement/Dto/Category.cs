namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    public class Category
    {
        public int Id { get; set; }
        public int ModificationCounter { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ShowOrder { get; set; }
    }
}
