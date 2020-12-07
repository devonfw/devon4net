namespace Devon4Net.Infrastructure.AWS.Common.Validators
{
    public interface IApiKeyValidator
    {
        bool ValidateApiToken(string apiToken);
    }
}