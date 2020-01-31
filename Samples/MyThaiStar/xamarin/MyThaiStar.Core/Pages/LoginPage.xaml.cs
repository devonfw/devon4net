using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms;

namespace MyThaiStar.Core.Pages
{
    [MvxContentPagePresentation(WrapInNavigationPage = false)]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
            Img.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.thairestaurantlogin.jpg");
        }
    }
}