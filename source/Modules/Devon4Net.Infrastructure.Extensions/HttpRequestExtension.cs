using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Devon4Net.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public static string GetContextIsoLanguage(this HttpRequest request)
        {
            return request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.ThreeLetterISOLanguageName;
        }

        public static List<string> GetContextLanguages(this HttpRequest request)
        {
            return request.GetTypedHeaders()
                       .AcceptLanguage
                       ?.OrderByDescending(x => x.Quality ?? 1)
                       .Select(x => x.Value.ToString())
                       .ToList() ?? new List<string>();
        }
        public static CultureInfo GetContextCulture(this HttpRequest request)
        {
            return  request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
        }
    }
}
