using System.Linq;
using Excalibur.Shared.Observable;
using Xamarin.Forms;

namespace MyThaiStar.Core.Observable
{
    public class ShoppingCartItem : ObservableBase<int>
    {
        public int Quantity { get; set; }
        public Observable.Dish Dish { get; set; }

        public string TotalPrice => GetTotalPrice();

        public string TotalExtrasPrice => GetTotalExtrasPrice();



        public ShoppingCartItem()
        {
            Dish = new Dish();
        }

        private string GetTotalPrice()
        {
            decimal total = 0;
            if (Dish.Price != null) total = total + (Dish.Price.Value * Quantity) + Dish.Extras.Where(e => e.selected && e.price != null).Sum(e => e.price.Value);
            return total.ToString();
        }

        private string GetTotalExtrasPrice()
        {
            decimal total = 0;
            total = total +  Dish.Extras.Where(e => e.selected && e.price != null).Sum(e => e.price.Value);
            return total.ToString();
        }


    }
}