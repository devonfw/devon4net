using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[MvxModalPresentation(WrapInNavigationPage = false)]
    public partial class DishFilterPage : MvxContentPage<DishFilterViewModel>
    {
        public DishFilterPage()
        {            
            InitializeComponent();
        }
    }
}