using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using MyThaiStar.Core.Domain;
using Xciles.Uncommon.Net;

namespace MyThaiStar.Core.Services
{
    public class UserService : ServiceBase<IList<User>>
    {
        public override async Task<IList<User>> SyncDataAsync()
        {
            var result = await UncommonRequestHelper.ProcessGetRequestAsync<IList<User>>("https://jsonplaceholder.typicode.com/users");

            return result.Result;
        }
    }
}
