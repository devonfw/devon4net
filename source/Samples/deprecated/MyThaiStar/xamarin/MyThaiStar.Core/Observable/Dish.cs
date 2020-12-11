using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excalibur.Shared.Observable;
using MyThaiStar.Core.Business.Dto.DishManagement;
using Xamarin.Forms;


namespace MyThaiStar.Core.Observable
{
    
    public class Dish : ObservableBase<int>
    {

        private long _id;
        private string _name;
        private string _description;
        private decimal? _price;
        private string _image;
        private string _priceCurrency;

        private ICollection<ExtraDto> _extras;
        private ICollection<CategoryDto> _categories;
        
        public Dish()
        {
            _extras = new List<ExtraDto>();
            _categories = new List<CategoryDto>();
            //ImageSourceData = new FileImageSource();
        }

        public long IdDish
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string ShortDescription
        {
            get { return GetDescLenght(_description); }
            set { SetProperty(ref _description, value); }
        }
        public decimal? Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public ICollection<ExtraDto> Extras
        {
            get { return _extras; }
            set { SetProperty(ref _extras, value); }
        }
        public ICollection<CategoryDto> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        //public ImageSource ImageSourceData
        //{
        //    get { return ParseImageBase(_image); }
        //    //get { return ImageFromBase64(); }

        //    set { }
        //}

        public string PriceCurrency
        {
            get { return $"{_price} €"; }
            set { _price = null; }
        }


        public string SelectedExtras
        {
            get {
                var extras= string.Join(",", Extras.Where(e => e.selected).Select(e => e.name));
                return extras;
            }            
        }

        //private ImageSource ParseImageBase(string imageBase)
        //{
        //    if (string.IsNullOrEmpty(imageBase)) return new FileImageSource();
        //    try
        //    {
        //        return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(imageBase.Replace("data:image/jpeg;base64,", string.Empty))));
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = $"{ex.Message} : {ex.InnerException}";
        //    }
        //    return new FileImageSource();
        //}

        //public ImageSource GetImageSourceData()
        //{
        //    if (string.IsNullOrEmpty(_image)) return new FileImageSource();
        //    try
        //    {
        //        return ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_image.Replace("data:image/jpeg;base64,", string.Empty))));
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = $"{ex.Message} : {ex.InnerException}";
        //    }
        //    return new FileImageSource();
        //}

        public string GetDescLenght(string descriptionText)
        {
            if (descriptionText.Length > 180) return descriptionText.Substring(0, 176) + "...";

            return descriptionText;
        }

        public  Image ImageFromBase64()
        {
            byte[] imageBytes = Convert.FromBase64String(_image.Replace("data:image/jpeg;base64,", string.Empty)); return new Image { Source = ImageSource.FromStream(() => new MemoryStream(imageBytes)) };
        }
    }
}
