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
    public partial class BookTablePage : MvxContentPage<BookTableViewModel>
    {
        public BookTablePage()
        {
            InitializeComponent();
            SetUp();            
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            NumberDishes.Text = $"Total assistants {e.NewValue}";
        }

        private void SetUp()
        {
            StepperDishNumber.ValueChanged += OnStepperValueChanged;
            NumberDishes.Text = $"Total assistants {1}";
            DateImage.GestureRecognizers.Add(new TapGestureRecognizer(OnTapImageDate));
            TimeImage.GestureRecognizers.Add(new TapGestureRecognizer(OnTapImageTime));
        }

        private void OnTapImageDate(View arg1, object arg2)
        {
            DatePickerObj.Focus();
        }
        private void OnTapImageTime(View arg1, object arg2)
        {
            TimePickerObj.Focus();
        }
    }
}