using FoodApp.Models;
using FoodApp.Repositories;
using FoodApp.ViewModels;
using FoodApp.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace FoodApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainTabbedPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<FridgeContentPage, FridgeContentPageViewModel>();
            containerRegistry.RegisterForNavigation<ShoppingListContentPage, ShoppingListContentPageViewModel>();

            containerRegistry.RegisterForNavigation<NewItemPage, NewItemPageViewModel>();
            containerRegistry.RegisterForNavigation<ItemDetailPage2, ItemDetailPage2ViewModel>();
            containerRegistry.RegisterForNavigation<EditItemContentPage, EditItemContentPageViewModel>();

            containerRegistry.RegisterSingleton<IItemRepository<Item>, FireBaseItemRepository>();
            containerRegistry.RegisterSingleton<IItemRepository<Location>, FireBaseItemRepository>();

            containerRegistry.RegisterForNavigation<NewShoppingItemPage, NewShoppingItemPageViewModel>();
            containerRegistry.RegisterForNavigation<ListItemDetailPage, ListItemDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<EditListItemPage, EditListItemPageViewModel>();
            containerRegistry.RegisterForNavigation<StorePage, StorePageViewModel>();
            containerRegistry.RegisterForNavigation<NewStorePage, NewStorePageViewModel>();
            containerRegistry.RegisterForNavigation<StoreDetailPage, StoreDetailPageViewModel>();
        }
    }
}
