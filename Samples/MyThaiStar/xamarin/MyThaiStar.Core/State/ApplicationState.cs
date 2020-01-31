using Excalibur.Shared.State;
using MyThaiStar.Core.Configuration;

namespace MyThaiStar.Core.State
{
    public class ApplicationState : BaseState<Config>, IApplicationState
    {
        public string Email
        {
            get { return Config.Email; }
            set { Config.Email = value; }
        }
    }
}
