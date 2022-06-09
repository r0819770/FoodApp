using FoodApp.Models;
using FoodApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FoodApp.ViewModels
{
    public class EditItemContentPageViewModel : ViewModelBase
    {
        private Item item;

        private IItemRepository<Item> itemRepository;
        public EditItemContentPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository)
            : base(navigationService)
        {
            Title = "Edit item";


            SaveCommand = new DelegateCommand(OnSave, ValidateSave).ObservesProperty(() => Name).ObservesProperty(() => Amount);
            CancelCommand = new DelegateCommand(OnCancel);

            this.itemRepository = itemRepository;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("item"))
            {
                item = parameters.GetValue<Item>("item");
                Name = item.Name;
                ExpiryDate = item.ExpiryDate;
                Amount = item.Amount;
                

            };
        }


        private async void OnCancel()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnSave()
        {
            item.Name = Name;
            item.Amount = Amount;
            item.ExpiryDate = ExpiryDate;
            await itemRepository.UpdateItemAsync(item);
            await NavigationService.GoBackToRootAsync();
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }

        }
        private DateTime expiryDate;
        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { SetProperty(ref expiryDate, value); }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }
        

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private bool ValidateSave()
        {
            bool test = !String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(amount) && amount != "0";
            return test;
        }

    }
}
