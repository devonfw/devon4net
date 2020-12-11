using System.Threading.Tasks;
using Excalibur.Shared.Business;

namespace MyThaiStar.Core.Business.Interfaces
{
    public interface ILoggedInUser : ISingleBusiness<Domain.LoggedInUser>
    {
        Task Store(Domain.LoggedInUser user);
    }
}