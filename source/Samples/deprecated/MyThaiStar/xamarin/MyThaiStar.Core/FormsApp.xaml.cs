using MvvmCross.Forms.Platform;
using MyThaiStar.Core.Business;
using MyThaiStar.Core.Business.Dto.DishManagement;
using MyThaiStar.Core.Business.Interfaces;

namespace MyThaiStar.Core
{
    public partial class FormsApp : MvxFormsApplication
    {
        private static IStorableObject<Observable.ShoppingCartItem> KartShop { get; set; }
        private static FilterDtoSearchObject GlobalDishFilter { get; set; }

        public FormsApp()
        {
            SetUp();
        }

        private void SetUp()
        {
            KartShop = new StorableObject<Observable.ShoppingCartItem>();
            GlobalDishFilter = new FilterDtoSearchObject { };
        }

        #region Shoppingcart
        public IStorableObject<Observable.ShoppingCartItem> GetShoppingKart()
        {
            return KartShop;
        }
        #endregion

        #region DishFilter
        public FilterDtoSearchObject GetDishFilter()
        {
            return GlobalDishFilter;
        }

        public void ResetDishFilter()
        {
            GlobalDishFilter = new FilterDtoSearchObject { Categories = new CategorySearchDto[0], MinLikes = "", MaxPrice = "", SearchBy = "", sort = new Business.Dto.General.SortByDto[0] };
        }

        public void SetDishFilter(FilterDtoSearchObject filter)
        {
            GlobalDishFilter = filter;
        }
        #endregion



    }
}
