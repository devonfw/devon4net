using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyThaiStar.Core.Services.Interfaces;
using MyThaiStar.Core.ViewModels;
using XLabs.Ioc;

namespace MyThaiStar.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        private readonly ILoginService _loginService;
        public AppStart(ILoginService loginService)
        {
            _loginService = loginService;
            
        }

        public void Start(object hint = null)
        {
            Resolver.Resolve<ISyncService>().PartialSyncAsync().ConfigureAwait(false);
            
            //Mvx.Resolve<IMvxNavigationService>().Navigate<LoginViewModel>();
            var logged = _loginService.ValidateSync();

            //// login things
            if (logged)
            {
                // todo init sync and loading of data

                //rol user:
                GotoMasterDetailPage();

                //rol waiter...


            }
            else
            {
                GotoMasterDetailPage();
            }

            
        }

        private void GotoMasterDetailPage()
        {
            Mvx.Resolve<IMvxNavigationService>().Navigate<MasterDetailViewModel>();
            Mvx.Resolve<IMvxNavigationService>().Navigate<DishListViewModel>();            
        }
    }
}