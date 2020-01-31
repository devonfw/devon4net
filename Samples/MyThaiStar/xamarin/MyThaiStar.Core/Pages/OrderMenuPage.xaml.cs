using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class OrderMenuPage : MvxContentPage<OrderMenuViewModel>
    {        
        public OrderMenuPage()
        {
            InitializeComponent();     
        }
        private async void PopUpPageOk(object sender, EventArgs e)
        {
            if (ViewModel.CheckAcceptedTerms())
                await Navigation.PushPopupAsync(new PopUpPage("You have just send your order!", "ok.png"));
            else AcceptTerms(sender, e);
        }

        private async void PopUpPageCancel(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopUpPage("You canceled your order", "info.png"));
        }

        private async void AcceptTerms(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopUpPage("Please accept the terms first!", "info.png"));
        }

    }
}