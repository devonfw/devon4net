using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MyThaiStar.Core.Business;
using Excalibur.Cross.ViewModels;
using Excalibur.Shared.Presentation;
using Xamarin.Forms;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;

namespace MyThaiStar.Core.ViewModels
{
    public class ShoppingCartListViewModel : ListViewModel<int, Observable.ShoppingCartItem, Observable.ShoppingCartItem, IListPresentation<int, Observable.ShoppingCartItem, Observable.ShoppingCartItem>, ShoppingCartListItemViewModel>
    {
        public List<Observable.ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCartListViewModel()
        {
            try
            {                
                
                ShoppingCartItems = ((MyThaiStar.Core.FormsApp)Application.Current).GetShoppingKart().GetItems();

            }
            catch (Exception ex)
            {
                var msg = $"{ex.Message} : {ex.InnerException}";
            } 
        }

        internal bool CheckCart()
        {
            return ShoppingCartItems.Any();
        }

        public IMvxAsyncCommand GoShopCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if (ShoppingCartItems != null && ShoppingCartItems.Any())
                    {
                        Mvx.Resolve<IMvxNavigationService>().Navigate<MasterDetailViewModel>();
                        Mvx.Resolve<IMvxNavigationService>().Navigate<OrderMenuViewModel>();
                    }
                });
            }
        }
    }
}
