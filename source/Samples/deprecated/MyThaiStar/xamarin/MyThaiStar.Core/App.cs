using System.Collections.Generic;
using Excalibur.Cross;
using Excalibur.Providers.File;
using Excalibur.Shared.Business;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MyThaiStar.Core.Business;
using MyThaiStar.Core.Business.Interfaces;
using MyThaiStar.Core.Observable;
using MyThaiStar.Core.Services;
using MyThaiStar.Core.Services.Interfaces;
using MyThaiStar.Core.State;
using MyThaiStar.Core.ViewModels;
using XLabs.Ioc;

namespace MyThaiStar.Core
{
    public partial class App : ExApp
    {
        

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            base.Initialize();

            var state = Resolver.Resolve<IApplicationState>();
            state.InitAndLoadAsync().GetAwaiter().GetResult();

            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            var appStart = Mvx.Resolve<IMvxAppStart>();
            RegisterAppStart(appStart);
        }

        public override void RegisterDependencies()
        {
            // Application Things
            Container.RegisterSingle<IApplicationState, ApplicationState>();
            Container.RegisterSingle<ISyncService, SyncService>();

            // User
            Container.Register<IObjectStorageProvider<int, Domain.LoggedInUser>, ObjectAsFileStorageProvider<int, Domain.LoggedInUser>>();
            Container.Register<IObjectMapper<Domain.LoggedInUser, Observable.LoggedInUser>, BaseObjectMapper<Domain.LoggedInUser, Observable.LoggedInUser>>();
            Container.Register<IObjectMapper<Observable.LoggedInUser, Observable.LoggedInUser>, BaseObjectMapper<Observable.LoggedInUser, Observable.LoggedInUser>>();
            Container.Register<Business.Interfaces.ILoggedInUser, Business.LoggedInUser>();
            Container.Register<IServiceBase<Domain.LoggedInUser>, LoggedInUserService>();
            Container.RegisterSingle<ISinglePresentation<int, Observable.LoggedInUser>, BaseSinglePresentation<int, Domain.LoggedInUser, Observable.LoggedInUser>>();

            // User
            Container.Register<IObjectStorageProvider<int, Domain.User>, ObjectAsFileStorageProvider<int, Domain.User>>();
            Container.Register<IObjectMapper<Domain.User, Observable.User>, BaseObjectMapper<Domain.User, Observable.User>>();
            Container.Register<IObjectMapper<Observable.User, Observable.User>, BaseObjectMapper<Observable.User, Observable.User>>();
            Container.Register<IListBusiness<int, Domain.User>, BaseListBusiness<int, Domain.User>>();
            Container.Register<IServiceBase<IList<Domain.User>>, UserService>();
            Container.RegisterSingle<IListPresentation<int, Observable.User, Observable.User>, BaseListPresentation<int, Domain.User, Observable.User, Observable.User>>();

            //Dish
            Container.Register<IObjectStorageProvider<int, Domain.Dish>, ObjectAsFileStorageProvider<int, Domain.Dish>>();
            Container.Register<IObjectMapper<Domain.Dish, Observable.Dish>, BaseObjectMapper<Domain.Dish, Observable.Dish>>();
            Container.Register<IObjectMapper<Observable.Dish, Observable.Dish>, BaseObjectMapper<Observable.Dish, Observable.Dish>>();
            Container.Register<IListBusiness<int, Domain.Dish>, BaseListBusiness<int, Domain.Dish>>();
            Container.Register<IServiceBase<IList<Domain.Dish>>, DishService>();
            Container.RegisterSingle<IListPresentation<int, Observable.Dish, Observable.Dish>, BaseListPresentation<int, Domain.Dish, Observable.Dish, Observable.Dish>>();

            //CartItem
            Container.Register<IObjectMapper<Observable.ShoppingCartItem, Observable.ShoppingCartItem>, BaseObjectMapper<Observable.ShoppingCartItem, Observable.ShoppingCartItem>>();
            Container.RegisterSingle<IListPresentation<int, Observable.ShoppingCartItem, Observable.ShoppingCartItem>, BaseListPresentation<int, Domain.ShoppingCartItem, Observable.ShoppingCartItem, Observable.ShoppingCartItem>>();

        }
    }
}
