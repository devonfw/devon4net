using System.Threading.Tasks;

namespace MyThaiStar.Core.Services.Interfaces
{
    public interface ISyncService
    {
        Task FullSyncAsync();
        Task PartialSyncAsync();
    }
}
