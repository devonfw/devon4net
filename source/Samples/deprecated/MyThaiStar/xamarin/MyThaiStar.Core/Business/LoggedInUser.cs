using System.Threading.Tasks;
using Excalibur.Shared.Business;
using MyThaiStar.Core.Business.Interfaces;

namespace MyThaiStar.Core.Business
{
    public class LoggedInUser : BaseSingleBusiness<int, Domain.LoggedInUser>, ILoggedInUser
    {
        public async Task Store(Domain.LoggedInUser user)
        {
            await StoreItemAsync(user).ConfigureAwait(false);

            PublishUpdated(user);
        }
    }
}
