using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MyThaiStar.Core.Business.Dto.DishManagement;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyThaiStar.Core.ViewModels
{
    public class DishFilterViewModel: MvxViewModel
    {
        #region private properties
        private int _MinPrice;
        private int _MaxPrice;
        private int _Price;
        private int _MinLikes;
        private int _MaxLikes;
        private int _Likes;

        private bool _MainDish;
        private bool _Starter;
        private bool _Dessert;

        private bool _Noodle;
        private bool _Rice;
        private bool _Curry;

        private bool _Vegan;
        private bool _Vegetarian;

        private bool _Favourite;

        private string _SearchBy;

        #endregion

        #region public properties
        public int MinPrice {
            get => _MinPrice;
            set => SetProperty(ref _MinPrice, value);
        }
        public int MaxPrice
        {
            get => _MaxPrice;
            set => SetProperty(ref _MaxPrice, value);
        }
        public int MinLikes
        {
            get => _MinLikes;
            set => SetProperty(ref _MinLikes, value);
        }
        public int MaxLikes
        {
            get => _MaxLikes;
            set => SetProperty(ref _MaxLikes, value);
        }
        public int Price
        {
            get => _Price;
            set => SetProperty(ref _Price, value);
        }
        public int Likes
        {
            get => _Likes;
            set => SetProperty(ref _Likes, value);
        }

        public bool MainDish
        {
            get => _MainDish;
            set => SetProperty(ref _MainDish, value);
        }
        public bool Starter
        {
            get => _Starter;
            set => SetProperty(ref _Starter, value);
        }
        public bool Dessert
        {
            get => _Dessert;
            set => SetProperty(ref _Dessert, value);
        }

        public bool Noodle
        {
            get => _Noodle;
            set => SetProperty(ref _Noodle, value);
        }
        public bool Rice
        {
            get => _Rice;
            set => SetProperty(ref _Rice, value);
        }
        public bool Curry
        {
            get => _Curry;
            set => SetProperty(ref _Curry, value);
        }

        public bool Vegan
        {
            get => _Vegan;
            set => SetProperty(ref _Vegan, value);
        }
        public bool Vegetarian
        {
            get => _Vegetarian;
            set => SetProperty(ref _Vegetarian, value);
        }

        public bool Favourite
        {
            get => _Favourite;
            set => SetProperty(ref _Favourite, value);
        }

        public string SearchBy
        {
            get => _SearchBy;
            set => SetProperty(ref _SearchBy, value);
        }

        #endregion

        #region consts
        private const int minLikes = 0;
        private const int maxLikes = 0;
        private const int minPrice = 0;
        private const int maxPrice = 0;
        #endregion


        private readonly IMvxNavigationService _navigationService;
        public IMvxAsyncCommand CloseCommand { get; private set; }

        public DishFilterViewModel()
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();// navigationService;
            CloseCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
            GetValues();
        }

        private void GetValues()
        {
            Reset();
            var globalFilter = ((MyThaiStar.Core.FormsApp)Application.Current).GetDishFilter();

            Likes = Convert.ToInt32(globalFilter.MinLikes);
            Price = Convert.ToInt32(globalFilter.MaxPrice);
            if (globalFilter.Categories == null) return;
            var categories = new List<CategorySearchDto>(globalFilter.Categories);

            MainDish = categories.Exists(c => c.Id == 0);
            Starter = categories.Exists(c => c.Id == 1);
            Dessert = categories.Exists(c => c.Id == 2);
            Noodle = categories.Exists(c => c.Id == 3);
            Rice = categories.Exists(c => c.Id == 4);
            Curry = categories.Exists(c => c.Id == 5);
            Vegan = categories.Exists(c => c.Id == 6); 
            Vegetarian = categories.Exists(c => c.Id == 7);            
        }

        public override System.Threading.Tasks.Task Initialize()
        {
            return base.Initialize();
        }

        private void Reset()
        {
            _MinLikes = minLikes;
            _MaxLikes = maxLikes;
            _MinPrice = minPrice;
            _MaxPrice = maxPrice;
            Price = 0;
            Likes = 0;

            Favourite = false;
            Vegan = false;
            Vegetarian = false;
            Curry = false;
            Rice = false;
            Noodle = false;
            Dessert = false;
            Starter = false;
            MainDish = false;
            SearchBy = string.Empty;
        }

        private  FilterDtoSearchObject GetFilter()
        {
            var categories = new List<CategorySearchDto>();

            if (MainDish) categories.Add(new CategorySearchDto { Id = 0 });
            if (Starter) categories.Add(new CategorySearchDto { Id = 1 });
            if (Dessert) categories.Add(new CategorySearchDto { Id = 2 });
            if (Noodle) categories.Add(new CategorySearchDto { Id = 3 });
            if (Rice) categories.Add(new CategorySearchDto { Id = 4 });
            if (Curry) categories.Add(new CategorySearchDto { Id = 5 });
            if (Vegan) categories.Add(new CategorySearchDto { Id = 6 });
            if (Vegetarian) categories.Add(new CategorySearchDto { Id = 7 });

            return new FilterDtoSearchObject { Categories = categories.ToArray(), MinLikes = Likes.ToString(), MaxPrice = Price.ToString(), SearchBy = SearchBy, sort = new Business.Dto.General.SortByDto[0] }; ;
        }

        public ICommand ResetFilter
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    Reset();
                    ((MyThaiStar.Core.FormsApp)Application.Current).ResetDishFilter();
                });
            }
        }

        public ICommand SetFilter
        {
            get
            {
                return new MvxCommand(async () =>
                {                    
                    ((MyThaiStar.Core.FormsApp)Application.Current).SetDishFilter(GetFilter());
                    Mvx.Resolve<IMvxNavigationService>().Navigate<MasterDetailViewModel>();
                    Mvx.Resolve<IMvxNavigationService>().Navigate<DishListViewModel>();
                });
            }
        }


        //
    }
}
