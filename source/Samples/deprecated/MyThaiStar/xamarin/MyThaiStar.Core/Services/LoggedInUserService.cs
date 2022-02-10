using System;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using MyThaiStar.Core.Domain;

namespace MyThaiStar.Core.Services
{
    public class LoggedInUserService : ServiceBase<LoggedInUser>
    {
        public override Task<LoggedInUser> SyncDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
