using System.Threading.Tasks;

namespace MyThaiStar.Core.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string email, string password);
        Task<bool> ValidateAsync();
        bool ValidateSync();
    }
}
