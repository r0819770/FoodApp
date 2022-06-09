using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repositories
{
    public interface IItemRepository<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);

        Task<bool> AddItemAsyncList(T item);

        Task<bool> UpdateItemAsyncList(T item);

        Task<bool> DeleteItemAsyncList(string id);
        Task<T> GetItemAsyncList(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool froceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsyncList(bool froceRefresh = false);

        Task<bool> DeleteLocationAsyncList(string id);
        Task<bool> DeleteLocationAsync(string id);
    }
}
