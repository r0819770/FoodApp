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
    public class NewStorePageViewModel : ViewModelBase
    {
        private IItemRepository<Location> itemRepository;
        public NewStorePageViewModel(INavigationService navigationService, IItemRepository<Location> itemRepository)
            : base(navigationService)
        {
            Title = "New store";

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
            Location newLocation = new Location()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
            };
            await itemRepository.AddItemAsync(newLocation);
            await NavigationService.GoBackAsync();
        }



        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
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
