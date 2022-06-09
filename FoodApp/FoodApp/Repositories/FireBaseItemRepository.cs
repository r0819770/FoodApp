using Firebase.Database;
using Firebase.Database.Query;
using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repositories
{
    public class FireBaseItemRepository: IItemRepository<Item>, IItemRepository<Location>
    {
        private const string BaseUrl = "https://foodapp-614e7-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly ChildQuery _query;  //query for Fridge items
        private readonly ChildQuery _query2; //query for Shopping list items
        private readonly ChildQuery _query3; //query for Location items

        public FireBaseItemRepository()
        {
            string path = "items";
            string path2 = "list";
            string path3 = "locations";

            _query = new FirebaseClient(BaseUrl).Child(path);
            _query2 = new FirebaseClient(BaseUrl).Child(path2);
            _query3 = new FirebaseClient(BaseUrl).Child(path3);

        }


        //all for fridge
        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                var addedItem = await _query
                    .PostAsync(item);
                item.Id = addedItem.Key;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
       
        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                Item copy = new Item() { Id = item.Id, Name = item.Name, Amount = item.Amount, ExpiryDate = item.ExpiryDate};
                await _query
                    .Child(item.Id)
                    .PutAsync(copy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
        

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                await _query
                    .Child(id)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
       
        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                var item = await _query
                    .Child(id)
                    .OnceSingleAsync<Item>();
                item.Id = id;
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

     

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var firebaseObjects = await _query
                    .OnceAsync<Item>();

                return firebaseObjects
                    .Select(x => new Item { Id = x.Key, Name = x.Object.Name, Amount = x.Object.Amount, ExpiryDate = x.Object.ExpiryDate });//.OrderBy(i=>i.ExpiryDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


        //all for shopping list
        public async Task<bool> AddItemAsyncList(Item item)
        {
            try
            {
                var addedItem = await _query2
                    .PostAsync(item);
                item.Id = addedItem.Key;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateItemAsyncList(Item item)
        {
            try
            {
                Item copy = new Item() { Id = item.Id, Name = item.Name, Amount = item.Amount };
                await _query2
                    .Child(item.Id)
                    .PutAsync(copy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteItemAsyncList(string id)
        {
            try
            {
                await _query2
                    .Child(id)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<Item> GetItemAsyncList(string id)
        {
            try
            {
                var item = await _query2
                    .Child(id)
                    .OnceSingleAsync<Item>();
                item.Id = id;
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public async Task<IEnumerable<Item>> GetItemsAsyncList(bool forceRefresh = false)
        {
            try
            {
                var firebaseObjects = await _query2
                    .OnceAsync<Item>();

                return firebaseObjects
                    .Select(x => new Item { Id = x.Key, Name = x.Object.Name, Amount = x.Object.Amount});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


        //all needed for location
        public async Task<bool> AddItemAsync(Location location)
        {
            try
            {
                var addedItem = await _query3
                    .PostAsync(location);
                location.Id = addedItem.Key;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateItemAsync(Location location)
        {
            try
            {
                Item copy = new Item() { Id = location.Id, Name = location.Name};
                await _query3
                    .Child(location.Id)
                    .PutAsync(copy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteLocationAsync(string id)
        {
            try
            {
                await _query3
                    .Child(id)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }


        async Task<Location> IItemRepository<Location>.GetItemAsync(string id)
        {
            try
            {
                var location = await _query2
                    .Child(id)
                    .OnceSingleAsync<Location>();
                location.Id = id;
                return location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }




        async Task<IEnumerable<Location>> IItemRepository<Location>.GetItemsAsync(bool froceRefresh)
        {
            try
            {
                var firebaseObjects = await _query3
                    .OnceAsync<Location>();

                return firebaseObjects
                    .Select(x => new Location { Id = x.Key, Name = x.Object.Name});//.OrderBy(i=>i.ExpiryDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }





        //not needed for location repository
        Task<Location> IItemRepository<Location>.GetItemAsyncList(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddItemAsyncList(Location item)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateItemAsyncList(Location item)
        {
            throw new NotImplementedException();
        }


        Task<IEnumerable<Location>> IItemRepository<Location>.GetItemsAsyncList(bool froceRefresh)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLocationAsyncList(string id)
        {
            throw new NotImplementedException();
        }

        
    }
}

