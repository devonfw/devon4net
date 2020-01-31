using System;
using MvvmCross.Forms.Views;
using MvvmCross.Forms.Views.Attributes;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class DishDetailPage : MvxContentPage<DishDetailViewModel>
    {
        public DishDetailPage()
        {
            InitializeComponent();            
            //ImgBackground.Source = ImageSource.FromResource("MyThaiStar.Core.Resources.wood.jpg");
            SetUp();
            StepperDishNumber.ValueChanged += OnStepperValueChanged;            
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumberDishes.Text = $"Total {e.NewValue}";
        }

        private void SetUp()
        {
            //StepperDishNumber.Value = 1;
            NumberDishes.Text = $"Total {1}";
        }

        private void OnSelectedExtraItem(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }

        private void BtnAddToOrder(object sender, EventArgs e)
        {
            var item = new Observable.ShoppingCartItem { Quantity = Convert.ToInt32(StepperDishNumber.Value), Dish = ViewModel.DishDetail };
            ((MyThaiStar.Core.FormsApp)Application.Current).GetShoppingKart().AddItem(item);
        }

        //protected override bool OnBackButtonPressed()
        //{

        //    // If you want to stop the back button
        //    return true;

        //}
    }
}