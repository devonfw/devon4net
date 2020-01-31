using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyThaiStar.Common.Business.DishManagement.Dto;
using MyThaiStar.Common.Business.DishManagement.Dto.SearchFilter;

namespace MyThaiStar.Common.Business.DishManagement.Controller
{
    /// <summary>
    /// Dish controller
    /// </summary>
    [ApiController]
    public class DishController : ControllerBase
    {
        /// <summary>
        /// Initialization
        /// </summary>
        public DishController()
        {
        }

        /// <summary>
        /// Gets the  list of available dishes regarding the filter options.
        /// </summary>
        /// <param name="searchFilter">Contains the criteria values to perform the search. Case of null or empty values will return the full set of dishes.</param>
        /// <returns>A list of dishes with all related information</returns>
        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("/mythaistar/services/rest/Dishmanagement/v1/Dish/Search")]
        [EnableCors("CorsPolicy")]
        public async Task<DishSearchResultDto> Get([FromBody] SearchFilterDto searchFilter)
        {
            var result = await Task.FromResult(new DishSearchResultDto());
            return result;
        }
    }
}
