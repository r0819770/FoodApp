using FoodApp.Models;
using FoodApp.Repositories;
using FoodApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodApp.ViewModels
{
    public class ListItemDetailPageViewModel : ViewModelBase
    {
        private Item item;
        private IPageDialogService pageDialogService;
        private IItemRepository<Item> itemRepository;

        public ListItemDetailPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository, IPageDialogService pageDialogService)
            : base(navigationService)
        {


            Title = "Detail Page";



            this.pageDialogService = pageDialogService;

            this.itemRepository = itemRepository;

            EditItemCommand = new DelegateCommand(OnEditItem);
            DeleteItemCommand = new DelegateCommand(OnDeleteItem);

        }


        private async void OnDeleteItem()
        {
            var result = await pageDialogService.DisplayAlertAsync("Alert", "Are you sure you want to delete this item?", "Yes", "No");
            if (result)
            {
                await itemRepository.DeleteItemAsyncList(item.Id);
                await NavigationService.GoBackAsync();

            }
        }

        private async void OnEditItem()
        {
            var p = new NavigationParameters();
            p.Add("item", item);
            await NavigationService.NavigateAsync(nameof(EditListItemPage), p);
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }

        }
      
        private string amount;
        public string Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                item = parameters.GetValue<Item>("item");
                Name = item.Name;
                Amount = item.Amount;



            }
        }

        public DelegateCommand EditItemCommand { get; }
        public DelegateCommand DeleteItemCommand { get; }

    }
}
