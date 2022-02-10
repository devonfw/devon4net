using System;
using System.Collections.Generic;
using System.Text;

namespace MyThaiStar.Common.Business.DishManagement.Dto
{
    /// <summary>
    /// Result of the dish search
    /// </summary>
    public class DishSearchResultDto
    {
        public List<Content> Content { get; set; }
        public Pageable Pageable { get; set; }
        public int TotalElements { get; set; }
    }
}
