using System;
using System.Globalization;
using System.IO;
using MvvmCross.Forms.Views;
using MyThaiStar.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Excalibur.Cross.ViewModels;

namespace MyThaiStar.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCartListItemView : MvxContentView<ShoppingCartListItemViewModel>
    {
        public ShoppingCartListItemView()
        {
            InitializeComponent();   
            
            //DishImage.Source = GetImage();
        }

        

        //private ImageSource GetImage()
        //{
            
        //    var img = ViewModel.SelectedObservable as Observable.ShoppingCartItem;
        //    return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img.Dish.Image.Replace("data:image/jpeg;base64,", string.Empty))));
        //}
    }

    public class ConverterBase64ImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            var base64Image = ((Binding)parameter).Source as Label;
            if (base64Image != null && string.IsNullOrEmpty(base64Image.Text)) return new MemoryStream();
            var imageBytes = System.Convert.FromBase64String(base64Image.Text.Replace("data:image/jpeg;base64,", string.Empty));
            return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not implemented as we do not convert back
            throw new NotImplementedException();
        }
    }

}