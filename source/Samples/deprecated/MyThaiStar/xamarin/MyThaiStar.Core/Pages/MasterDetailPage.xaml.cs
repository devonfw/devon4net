using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false)]
    public partial class MasterDetailPage : MvxContentPage<MasterDetailViewModel>
    {
        public MasterDetailPage()
        {
            InitializeComponent();

            ImageTop.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.thairestaurantlogin.jpg");
            ImageBottom.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.starS.png");
        }
    }
}