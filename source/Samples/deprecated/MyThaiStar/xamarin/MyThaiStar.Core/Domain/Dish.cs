using System.Collections.Generic;
using Excalibur.Shared.Storage;
using MyThaiStar.Core.Business.Dto.DishManagement;

namespace MyThaiStar.Core.Domain
{
    public class Dish : StorageDomain<int>
    {
        public long IdDish { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }

        public ICollection<ExtraDto> Extras { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }

    }

}
