using Newtonsoft.Json;

namespace MyThaiStar.Core.Business.Dto.UserManagement
{
    public class LoginDto
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
