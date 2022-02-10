using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyThaiStar.Core.Business.Dto.UserManagement;
using MyThaiStar.Core.Business.Interfaces;
using MyThaiStar.Core.Configuration;
using MyThaiStar.Core.Domain;
using MyThaiStar.Core.Services.Interfaces;
using MyThaiStar.Core.State;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace MyThaiStar.Core.Services
{
    public class LoginService : ILoginService
    {
        private HttpClient DefaultHttpClient { get; set; }

        public LoginService()
        {
            DefaultHttpClient = GetDefaultClient();
        }

        private HttpClient GetDefaultClient(string accessToken = null)
        {
            var handler = new HttpClientHandler { UseCookies = false };
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(600) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.98 Safari/537.36";
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            
            if (accessToken!=null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return client;
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            var state = Resolver.Resolve<IApplicationState>();
            state.Email = email;

            await state.SaveAsync();

            //Login
            var url = ApplicationConfig.LoginUrl;
            var loginDto = new LoginDto { UserName = email, Password = password };
            var result = await DefaultHttpClient.PostAsync(new Uri(url), GetStringContentFromObject(loginDto)).Result.Content.ReadAsStringAsync().ConfigureAwait(false);
            await Task.Delay(1000);

            if (string.IsNullOrEmpty(result)) return false;

            var userData = await GetCurrentUserData(result);

            var loggedInUserBusiness = Resolver.Resolve<ILoggedInUser>();
            await loggedInUserBusiness.Store(new LoggedInUser { Email = email, Username = email, AccessToken = result,Role = userData.Role, Name = userData.Name, LastName = userData.LastName, IdUser = userData.Id});

            return true;
        }

        public async Task<bool> ValidateAsync()
        {
            var state = Resolver.Resolve<IApplicationState>();
            if (string.IsNullOrWhiteSpace(state.Email)) return false;
            // we load the current user from storage, usually we reauth and sync current user

            var loggedInUserBusiness = Resolver.Resolve<ILoggedInUser>();
            await loggedInUserBusiness.PublishFromStorageAsync();
            
            return true;
        }

        public bool ValidateSync()
        {
            var state = Resolver.Resolve<IApplicationState>();
            if (string.IsNullOrWhiteSpace(state.Email)) return false;
            // we load the current user from storage, usually we reauth and sync current user

            var loggedInUserBusiness = Resolver.Resolve<ILoggedInUser>();
            loggedInUserBusiness.PublishFromStorageAsync();

            return true;
        }

        internal StringContent GetStringContentFromObject<T>(T objectToSend)
        {
            var content = JsonConvert.SerializeObject(objectToSend);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public async Task<CurrentUserDto> GetCurrentUserData(string authToken)
        {
            DefaultHttpClient = GetDefaultClient(authToken);
            var url = ApplicationConfig.CurrentUserUrl;
            var result = await DefaultHttpClient.GetAsync(new Uri(url));
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            return data!= null ? JsonConvert.DeserializeObject<CurrentUserDto>(data) : new CurrentUserDto();
        }

    }
}
