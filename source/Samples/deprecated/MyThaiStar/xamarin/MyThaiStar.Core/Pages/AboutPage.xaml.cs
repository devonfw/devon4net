using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class AboutPage : MvxContentPage<AboutViewModel>
    {
		public AboutPage ()
		{
			InitializeComponent ();
		    ImgAbout.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.thairestaurant.jpg");
		    ImageBottom.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.starS.png");
        }
	}
}