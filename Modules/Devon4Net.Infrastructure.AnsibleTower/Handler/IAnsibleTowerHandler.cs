using System.Threading.Tasks;

namespace Devon4Net.Infrastructure.AnsibleTower.Handler
{
    public interface IAnsibleTowerHandler
    {
        Task<string> Login(string userName, string password);
    }
}