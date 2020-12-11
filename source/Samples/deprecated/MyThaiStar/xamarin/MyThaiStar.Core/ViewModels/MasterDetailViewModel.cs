using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace MyThaiStar.Core.ViewModels
{
    public class MasterDetailViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private MenuItem _menuItem;
        private IMvxAsyncCommand<MenuItem> _onSelectedChangedCommand;

        public class MenuItem
        {
            public string Title { get; set; }

            public string Description { get; set; }
            public Type ViewModelType { get; set; }

        }

        public MasterDetailViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Menu = new[] {
                new MenuItem { Title = "Menu", Description = "My Thai Star menu", ViewModelType = typeof(DishListViewModel) },
                new MenuItem { Title = "Login", Description = "Login", ViewModelType = typeof(LoginViewModel)},
                new MenuItem { Title = "Cart", Description = "Your seleceted dishes", ViewModelType = typeof(ShoppingCartListViewModel)},
                new MenuItem { Title = "Book table", Description = "You can book a table and an order menu", ViewModelType = typeof(BookTableViewModel)},
                new MenuItem { Title = "Invite friends", Description = "Add your information and friends", ViewModelType = typeof(InviteFriendsViewModel)},
                new MenuItem { Title = "About", Description = "About My Thai Star", ViewModelType = typeof(AboutViewModel)},                
            };

#if __IOS__
            if(Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsGestureEnabled = false;
            }
            else if(Xamarin.Forms.Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsGestureEnabled = false;
            }
#endif
        }

        public IEnumerable<MenuItem> Menu { get; set; }

        public MenuItem SelectedMenu {
            get => _menuItem;
            set {
                if (SetProperty(ref _menuItem, value))
                    OnSelectedChangedCommand.Execute(value);
            }
        }

        private IMvxAsyncCommand<MenuItem> OnSelectedChangedCommand {
            get {
                return _onSelectedChangedCommand ?? (_onSelectedChangedCommand = new MvxAsyncCommand<MenuItem>(async (item) => 
                {
                    if (item == null)
                        return;

                    var vmType = item.ViewModelType;
                    if(Xamarin.Forms.Application.Current.MainPage is MasterDetailPage masterDetailPage)
                    {
                        masterDetailPage.IsPresented = false;
                    }
                    else if(Xamarin.Forms.Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
                    {
                        nestedMasterDetail.IsPresented = false;
                    }
                    await _navigationService.Navigate(vmType);
                }));
            }
        }
    }
}
