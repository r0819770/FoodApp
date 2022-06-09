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
    public class NewShoppingItemPageViewModel : ViewModelBase
    {
        private IItemRepository<Item> itemRepository;
        public NewShoppingItemPageViewModel(INavigationService navigationService, IItemRepository<Item> itemRepository)
            : base(navigationService)
        {
            Title = "New Page";

            SaveCommand = new DelegateCommand(OneSave, ValidateSave).ObservesProperty(() => Name);
            CancelCommand = new DelegateCommand(OnCancel);
            this.itemRepository = itemRepository;

        }


        private async void OnCancel()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OneSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Amount = Amount

            };
            await itemRepository.AddItemAsyncList(newItem);
            await NavigationService.GoBackAsync();
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



        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        private bool ValidateSave()
        {
            bool test = !String.IsNullOrWhiteSpace(name);
            return test;
        }
    }
}
