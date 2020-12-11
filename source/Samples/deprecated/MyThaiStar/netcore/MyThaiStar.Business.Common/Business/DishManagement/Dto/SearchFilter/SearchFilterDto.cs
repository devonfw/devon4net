using System;
using System.Collections.Generic;
using System.Text;

namespace MyThaiStar.Common.Business.DishManagement.Dto.SearchFilter
{
    /// <summary>
    /// The dish search filter
    /// </summary>
    public class SearchFilterDto
    {
        public List<CategorySearch> Categories { get; set; }
        public string SearchBy { get; set; }
        public Pageable Pageable { get; set; }
        public int MaxPrice { get; set; }
        public int MinLikes { get; set; }
    }
}
