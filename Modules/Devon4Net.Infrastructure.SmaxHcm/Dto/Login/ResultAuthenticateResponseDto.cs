using System.Collections.Generic;

namespace Devon4Net.Infrastructure.SmaxHcm.Dto.Login
{
    public class ResultAuthenticateResponseDto
    {
        public AuthenticateResponseDto AuthenticateResponseDto { get; set; }
        public List<string> CookieResult { get; set; }
    }
}