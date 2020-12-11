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
    public partial class InviteFriendsPage : MvxContentPage<InviteFriendsViewModel>
    {
        public InviteFriendsPage()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            TagEntry.TagTapped += Handle_ItemTapped;
            TagEntry.TagValidatorFactory += Handle_ItemValidator;
            ButtonEmailEntry.Clicked += Handle_AddGuest;
            TagEntry.TagEntry.IsVisible = false;
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

        private void Handle_AddGuest(object sender, EventArgs e)
        {
            var item = EmailEntry.Text;
            var result = ViewModel.ValidateAndReturn(item);
        }

        private Object Handle_ItemValidator(string arg)
        {
            return ViewModel.ValidateAndReturn(arg);            
        }

        private void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var item = e.Item as TagItem;            
            if (item != null) ViewModel.RemoveTagCommand(item);
        }
    }
}