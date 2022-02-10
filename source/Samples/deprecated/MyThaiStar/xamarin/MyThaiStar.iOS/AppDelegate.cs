using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.iOS;
using MvvmCross.Platform;
using UIKit;

namespace MyThaiStar.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });

            //UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(65, 105, 225); //blue            
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(33, 33, 33); //darkgray
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(33, 33, 33); //darkgray

            var setup = new Setup(this, Window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            LoadApplication(setup.FormsApplication);

            Window.MakeKeyAndVisible();

            return base.FinishedLaunching(app, options);
        }
    }
}
