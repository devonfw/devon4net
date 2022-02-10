using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.DishManagement
{
    public class CategorySearchDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

    }
}
