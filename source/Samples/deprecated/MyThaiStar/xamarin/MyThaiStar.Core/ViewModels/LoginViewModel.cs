using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MyThaiStar.Core.Services.Interfaces;
using XLabs.Ioc;

namespace MyThaiStar.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region private declaration
        private bool _isLoading;
        private string _user;
        private string _password;
        private readonly IMvxNavigationService _navigationService;
        private readonly ILoginService _loginService;

        private bool CanLogin()
        {
            return true;
        }

        private async Task GotoMasterDetailPage()
        {
            await _navigationService.Navigate<MasterDetailViewModel>();
            await _navigationService.Navigate<DishListViewModel>();
        }
        #endregion

        #region public declarations
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion


        public LoginViewModel(IMvxNavigationService navigationService, ILoginService loginService)
        {
            _navigationService = navigationService;
            _loginService = loginService;
            IsLoading = false;
        }


        #region commands
        public IMvxAsyncCommand CancelCommand => new MvxAsyncCommand(GotoMasterDetailPage, CanLogin);


        public IMvxAsyncCommand LoginCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    IsLoading = true;
                    if (await _loginService.LoginAsync(User, Password))
                    {
                        // Todo init sync things
                        await Resolver.Resolve<ISyncService>().FullSyncAsync().ConfigureAwait(false);
                        await NavigationService.Navigate<MainViewModel>();
                    }
                    else
                    {
                        // Todo alert dialog
                        IsLoading = false;
                    }
                }, () => !IsLoading);
            }
        }


        //public IMvxAsyncCommand LoginCommand
        //{
        //    get => new MvxAsyncCommand(GotoMasterDetailPage, CanLogin);
        //    set {  }
        //}


        #endregion


    }
}
