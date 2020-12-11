using System.Threading.Tasks;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MyThaiStar.Core.Business;

namespace MyThaiStar.Core.ViewModels
{
    public class DishListItemViewModel : DetailViewModel<int, Observable.Dish, IListPresentation<int, Observable.Dish, Observable.Dish>>
    {
        //public IMvxAsyncCommand ShowDetailCommand
        //{
        //    get => new MvxAsyncCommand(GotoMasterDetailPage);
        //    set { }
        //}

        //private readonly IMvxNavigationService _navigationService;

        //public DishListItemViewModel(IMvxNavigationService navigationService)
        //{
        //    _navigationService = navigationService;

        //    ShowDetailCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DishListViewModel>());
        //}

        //private async Task GotoMasterDetailPage()
        //{
        //    await _navigationService.Navigate<DishDetailViewModel>();
            
        //}
    }
}
