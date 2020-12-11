using System;
using System.IO;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Presentation;

namespace MyThaiStar.Core.ViewModels
{
    public class ShoppingCartListItemViewModel : DetailViewModel<int, Observable.ShoppingCartItem, IListPresentation<int, Observable.ShoppingCartItem, Observable.ShoppingCartItem>>
    {
        public Xamarin.Forms.ImageSource ItemImageSource {
            get
            {
                
                var img = SelectedObservable as Observable.ShoppingCartItem;
                return Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img.Dish.Image.Replace("data:image/jpeg;base64,", string.Empty))));
            }
           
        }

        public ShoppingCartListItemViewModel()
        {
            var a = "";
            //GetImage();
        }

        //public void GetImage()
        //{
        //    var img = SelectedObservable as Observable.ShoppingCartItem;
        //    ItemImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(img.Dish.Image.Replace("data:image/jpeg;base64,", string.Empty))));
        //}
    }
}