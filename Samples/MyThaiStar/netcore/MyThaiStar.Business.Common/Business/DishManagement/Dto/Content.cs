using System.Collections.Generic;

namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    public class Content
    {
        public Dish Dish { get; set; }
        public Image Image { get; set; }
        public List<Extra> Extras { get; set; }
        public List<Category> Categories { get; set; }
    }
}
