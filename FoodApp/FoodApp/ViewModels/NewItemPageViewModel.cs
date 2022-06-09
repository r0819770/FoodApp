using FoodApp.Models;
using FoodApp.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
    
    public class NewItemPageViewModel : ViewModelBase
    {
        private IItemRepository<Item> itemRepository;
        public NewItemPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository)
            : base(navigationService)
        {
            Title = "New Page";

            ExpiryDate =  DateTime.UtcNow.Date;

            SaveCommand = new DelegateCommand(OnSave, ValidateSave).ObservesProperty(() => Name).ObservesProperty(() => Amount);
            CancelCommand = new DelegateCommand(OnCancel);
            this.itemRepository = itemRepository;
            
        }


        private async void OnCancel()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                ExpiryDate = ExpiryDate,
                Amount = Amount
                

            };
            await itemRepository.AddItemAsync(newItem);
            await NavigationService.GoBackAsync();
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
            bool test = !String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(amount) && amount != "0" ;
            return test;
        }
    }
}
