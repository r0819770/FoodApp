using FoodApp.Models;
using FoodApp.Repositories;
using FoodApp.Views;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace FoodApp.ViewModels
{
    public class ShoppingListContentPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private IItemRepository<Item> itemRepository;
        
        public ShoppingListContentPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository)
           : base(navigationService)
        {
            Title = "Shopping list";
            AddItemCommand = new DelegateCommand(OnAddItem);
            LoadItemsCommand = new DelegateCommand(OnLoadItems);
            Items = new ObservableCollection<Item>();
            this.itemRepository = itemRepository;

        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (SetProperty(ref selectedItem, value))
                {
                    OnItemSelected(selectedItem);
                }

            }
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
            {
                return;
            }
            var p = new NavigationParameters();
            p.Add("item", item);
            await NavigationService.NavigateAsync(nameof(ListItemDetailPage), p);

        }

        public ObservableCollection<Item> Items { get; private set; }
        public ICommand LoadItemsCommand { get; private set; }

        public ICommand AddItemCommand { get; private set; }


        private async void OnLoadItems()
        {
            try
            {
                Items.Clear();
                var items = await itemRepository.GetItemsAsyncList(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItem()
        {
            await NavigationService.NavigateAsync(nameof(NewShoppingItemPage), null, true, true);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            selectedItem = null;
        }

        public void OnDisappearing()
        {

        }
    }
}
