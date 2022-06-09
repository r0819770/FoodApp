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
using Xamarin.Essentials;
using Location = FoodApp.Models.Location;

namespace FoodApp.ViewModels
{
    public class StoreDetailPageViewModel : ViewModelBase
    {
        private Location location;
        private IPageDialogService pageDialogService;
        private IItemRepository<Location> itemRepository;

        public StoreDetailPageViewModel(INavigationService navigationService, IItemRepository<Location> itemRepository, IPageDialogService pageDialogService)
            : base(navigationService)
        {


            Title = "Store detail Page";


            this.pageDialogService = pageDialogService;

            this.itemRepository = itemRepository;

            DeleteLocationCommand = new DelegateCommand(OnDeleteLocation);


            GoToLocationCommand = new DelegateCommand(OnGoToLocation);

        }

        private async void OnGoToLocation()
        {
            var adress = location.Name;
            var locationlist = await Geocoding.GetLocationsAsync(adress);
            var finallocation = locationlist?.FirstOrDefault();
            if (finallocation != null)
            {
                await Map.OpenAsync(finallocation);
            }
            else
            {
                var result = await pageDialogService.DisplayAlertAsync("Alert", "Your search isn't precise enough", "Delete", "Continue");
                if (result)
                {
                    await itemRepository.DeleteLocationAsync(location.Id);
                    await NavigationService.GoBackAsync();

                }
            }
            
        }

        private async void OnDeleteLocation()
        {
            var result = await pageDialogService.DisplayAlertAsync("Alert", "Are you sure you want to delete this store?", "Yes", "No");
            if (result)
            {
                await itemRepository.DeleteLocationAsync(location.Id);
                await NavigationService.GoBackAsync();

            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }

        }
       

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("location"))
            {
                location = parameters.GetValue<Location>("location");
                Name = location.Name;
             
            }
        }

        public DelegateCommand DeleteLocationCommand { get; }
        public DelegateCommand GoToLocationCommand { get; }

    }
}
