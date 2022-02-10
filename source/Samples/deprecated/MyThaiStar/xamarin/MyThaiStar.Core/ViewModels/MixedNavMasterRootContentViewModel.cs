using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace MyThaiStar.Core.ViewModels
{
    public class MixedNavMasterRootContentViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MixedNavMasterRootContentViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowModalCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DishListViewModel>());
        }

        public IMvxAsyncCommand ShowModalCommand { get; private set; } 
    }
}
