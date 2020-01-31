using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.UserManagement
{
    public class CurrentUserDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        public CurrentUserDto()
        {
            Id = -1;
            Name = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Role = string.Empty;
        }
    }


}
