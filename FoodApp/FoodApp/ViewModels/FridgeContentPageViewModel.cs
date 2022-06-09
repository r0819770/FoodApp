using FoodApp.Models;
using FoodApp.Repositories;
using FoodApp.Views;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace FoodApp.ViewModels
{
    public class FridgeContentPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private IItemRepository<Item> itemRepository;
        private DateTime currentDate = DateTime.Now.Date;
        private IPageDialogService pageDialogService;
        public bool expired;

        public FridgeContentPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "fridge";
            AddItemCommand = new DelegateCommand(OnAddItem);
            LoadItemsCommand = new DelegateCommand(OnLoadItems);
            Items = new ObservableCollection<Item>();
            this.itemRepository = itemRepository;
            this.pageDialogService = pageDialogService;


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
            await NavigationService.NavigateAsync(nameof(ItemDetailPage2), p);
            
        }

        public ObservableCollection<Item> Items { get; private set; }
        public ICommand LoadItemsCommand { get; private set; }

        public ICommand AddItemCommand { get; private set; }


        private async void OnLoadItems()
        {
            try
            {

                Items.Clear();
                var items = await itemRepository.GetItemsAsync(true);
                items = items.OrderBy(i => i.ExpiryDate);
                foreach(var item in items)
                {
                    if (currentDate > item.ExpiryDate.Date)
                    {
                       expired = true;
                    }
                    Items.Add(item);
                }
               


            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                if (expired == true)
                {
                    await pageDialogService.DisplayAlertAsync("Careful", "Your fridge has expired items in it", "Continue");
                };
                expired = false;
                IsBusy = false; 
            }
        }

        private async void OnAddItem()
        {
            await NavigationService.NavigateAsync(nameof(NewItemPage), null,true,true);
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
