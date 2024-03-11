using Shoppzter.Data;
using Shoppzter.Services.Contracts;
using Blazored.LocalStorage;

namespace Shoppzter.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ILocalStorageService _localStorageService;

        public ShoppingListService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<ShoppingList> GetShoppingList(int id)
        {
            var shoppingLists = await _localStorageService.GetItemAsync<List<ShoppingList>>("shoppingLists");
            
            if (shoppingLists != null)
            {
                var shoppingList = shoppingLists.FirstOrDefault(sl => sl.Id == id); // Assuming your ShoppingList class has an Id property.
                return shoppingList;
            }
            
            return new ShoppingList();
        }

        public async Task<List<ShoppingList>> GetShoppingLists()
        {
            var shoppingLists = await _localStorageService.GetItemAsync<List<ShoppingList>>("shoppingLists");

            if (shoppingLists != null)
            {
                return shoppingLists;
            }
            return new List<ShoppingList>();
        }

        public async Task AddShoppingList(ShoppingList shoppingList)
        {
            var shoppingLists = await GetShoppingLists(); // Get the current list
            int maxId = shoppingLists.Any() ? shoppingLists.Max(sl => sl.Id) : 0;
            shoppingList.Id = maxId + 1;
            shoppingLists.Add(shoppingList);
            await _localStorageService.SetItemAsync("shoppingLists", shoppingLists); // Update local storage
        }

        public async Task UpdateShoppingList(ShoppingList shoppingList)
        {
            var shoppingLists = await GetShoppingLists(); // Get the current list
            var listIndex = shoppingLists.FindIndex(sl => sl.Id == shoppingList.Id); // Find the index of the list to update

            if (listIndex != -1) // Check if the list was found
            {
                shoppingLists[listIndex] = shoppingList; // Update the list at the found index
                await _localStorageService.SetItemAsync("shoppingLists", shoppingLists); // Update local storage
            }
        }

        public async Task DeleteShoppingList(ShoppingList shoppingList)
        {
            var shoppingLists = await GetShoppingLists(); // Get the current list
            var listIndex = shoppingLists.FindIndex(sl => sl.Id == shoppingList.Id);

            if (listIndex != -1)
            {
                shoppingLists.RemoveAt(listIndex);
                await _localStorageService.SetItemAsync("shoppingLists", shoppingLists); // Update local storage
            }            
        }
    }
}
