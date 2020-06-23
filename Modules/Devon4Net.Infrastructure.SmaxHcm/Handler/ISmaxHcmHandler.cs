using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.SMAXHCM.Handler
{
    public interface ISmaxHcmHandler
    {
        Task<string> Login(string userName, string password);
    }
}