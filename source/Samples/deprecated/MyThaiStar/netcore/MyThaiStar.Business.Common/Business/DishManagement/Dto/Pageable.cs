using System.Collections.Generic;

namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    /// <summary>
    /// Pagination result
    /// </summary>
    public class Pageable
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<string> Sort { get; set; }
    }
}
