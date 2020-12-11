using Microsoft.Extensions.Configuration;

namespace Devon4Net.Infrastructure.AWS.Common.Validators
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly IConfiguration _configuration;

        public ApiKeyValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateApiToken(string apiToken)
        {
            var apiKeysList = _configuration.GetSection("ApiKeys").Value;
            return apiKeysList.Contains($"\"{apiToken}\"");
        }
    }
}
