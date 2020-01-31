using System;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class ShoppingCartListPage : MvxContentPage<ShoppingCartListViewModel>
    {
        public ShoppingCartListPage()
        {
            InitializeComponent();
            if (ViewModel == null) ViewModel = new ShoppingCartListViewModel();
        }
        private async void GoShoppingCommand(object sender, EventArgs e)
        {
            
            if (!ViewModel.CheckCart())
                await Navigation.PushPopupAsync(new PopUpPage("Please add items to your order!", "info.png"));
            else ViewModel.GoShopCommand.Execute(null);
        }
    }
}