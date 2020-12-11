using Excalibur.Shared.Storage;

namespace MyThaiStar.Core.Domain
{
    public class ShoppingCartItem : StorageDomain<int>
    {
        public int Quantity { get; set; }
        public Observable.Dish Dish { get; set; }
    }

}
