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
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Location = FoodApp.Models.Location;

namespace FoodApp.ViewModels
{
    public class StorePageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private IItemRepository<Location> itemRepository;
        public StorePageViewModel(INavigationService navigationService, IItemRepository<Location> itemRepository)
            : base(navigationService)
        {
            Title = "stores";
            AddLocationCommand = new DelegateCommand(OnAddLocation);
            LoadLocationsCommand = new DelegateCommand(OnLoadLocations);
            Locations = new ObservableCollection<Location>();
            this.itemRepository = itemRepository;


        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        private Location selectedLocation;
        public Location SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                if (SetProperty(ref selectedLocation, value))
                {
                    OnLocationSelected(selectedLocation);
                }

            }
        }

        private async void OnLocationSelected(Location location)
        {
            if (location == null)
            {
                return;
            }
            var p = new NavigationParameters();
            p.Add("location", location);
            await NavigationService.NavigateAsync(nameof(StoreDetailPage), p);

        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }

        }

 
        public ObservableCollection<Location> Locations { get; private set; }
        public ICommand LoadLocationsCommand { get; private set; }
        public ICommand AddLocationCommand { get; private set; }


        private async void OnLoadLocations()
        {
            try
            {
                Locations.Clear();
                var locations = await itemRepository.GetItemsAsync(true);
                foreach (var location in locations)
                {
                    Locations.Add(location);
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

        private async void OnAddLocation()
        {
            await NavigationService.NavigateAsync(nameof(NewStorePage), null, true, true);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            selectedLocation = null;
        }

        public void OnDisappearing()
        {

        }


    }
}
