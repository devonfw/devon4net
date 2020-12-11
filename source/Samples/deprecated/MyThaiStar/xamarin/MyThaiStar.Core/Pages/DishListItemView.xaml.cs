using MvvmCross.Forms.Views;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DishListItemView : MvxContentView<DishListItemViewModel>
    {
        public DishListItemView()
        {
            InitializeComponent();
        }
    }
}