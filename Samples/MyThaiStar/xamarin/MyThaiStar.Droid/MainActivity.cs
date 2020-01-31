using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Droid.Views;
using MvvmCross.Platform;
using MyThaiStar.Droid;
using MyThaiStar.Core.ViewModels;
using ImageCircle.Forms.Plugin.Droid;
using MyThaiStar.Droid.Renderers;

namespace MyThaiStar.Droid
{
    [Activity(
        Label = "MyThaiStar.Droid", 
        Icon = "@mipmap/icon",
        Theme = "@style/AppTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            ImageCircleRenderer.Init();
            TagEntryRenderer.Init();
            base.OnCreate(bundle);
        }

        public override void OnBackPressed()
        {
            //if (FragmentManager.BackStackEntryCount > 0)
            //{
            //    FragmentManager.PopBackStack();
            //}
            //else
            //{
            //    base.OnBackPressed();
            //}
        }
    }
}
