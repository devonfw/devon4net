using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Business;
using Excalibur.Shared.Presentation;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using XLabs.Ioc;

namespace MyThaiStar.Core.ViewModels
{
    public class DishListViewModel : ListViewModel<int, Observable.Dish, Observable.Dish, IListPresentation<int, Observable.Dish, Observable.Dish>, DishListItemViewModel>
    {
        public MvxAsyncCommand ShowFilterCommand { get; private set; }
        private readonly IMvxNavigationService _navigationService;
        public DishListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            if (!Observables.Any()) return;
            Resolver.Resolve<IListBusiness<int, Domain.Dish>>().PublishFromStorageAsync();
            ShowFilterCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DishFilterViewModel>());
            IsLoading = false;
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    // Todo fix IsLoading presentation ref
                    IsLoading = true;

                    await Task.Delay(5000);
                    await Resolver.Resolve<IListBusiness<int, Domain.Dish>>().UpdateFromServiceAsync();

                    IsLoading = false;
                });
            }
        }

        

        internal void GoToTest()
        {            
            Mvx.Resolve<IMvxNavigationService>().Navigate<DishFilterViewModel>();
        }

        public void ShowDetailCommand(Observable.Dish dishDetail)
        {
            Mvx.Resolve<IMvxNavigationService>().Navigate<DishDetailViewModel, Observable.Dish>(dishDetail);
            /*return new MvxCommand(async () =>
                {
                    // Todo fix IsLoading presentation ref
                    IsLoading = true;

                    await Task.Delay(5000);

                    await Mvx.Resolve<IMvxNavigationService>().Navigate<DishDetailViewModel>();

                    IsLoading = false;
                });*/
            
        }
    }
}
