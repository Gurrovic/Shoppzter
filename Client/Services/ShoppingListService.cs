using Client.Data;
using Client.Services.Contracts;
using Blazored.LocalStorage;

namespace Client.Services
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
                var shoppingList = shoppingLists.FirstOrDefault(sl => sl.Id == id);
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
            var shoppingLists = await GetShoppingLists();
            int maxId = shoppingLists.Any() ? shoppingLists.Max(sl => sl.Id) : 0;
            shoppingList.Id = maxId + 1;
            shoppingLists.Add(shoppingList);
            await _localStorageService.SetItemAsync("shoppingLists", shoppingLists);
        }

        public async Task UpdateShoppingList(ShoppingList shoppingList)
        {
            var shoppingLists = await GetShoppingLists();
            var listIndex = shoppingLists.FindIndex(sl => sl.Id == shoppingList.Id);

            if (listIndex != -1)
            {
                shoppingLists[listIndex] = shoppingList;
                await _localStorageService.SetItemAsync("shoppingLists", shoppingLists);
            }
        }

        public async Task<List<ShoppingList>> DeleteShoppingList(ShoppingList shoppingList)
        {
            var shoppingLists = await GetShoppingLists();
            var listIndex = shoppingLists.FindIndex(sl => sl.Id == shoppingList.Id);

            if (listIndex != -1)
            {
                shoppingLists.RemoveAt(listIndex);
                await _localStorageService.SetItemAsync("shoppingLists", shoppingLists);
                return shoppingLists;
            }

            return null!;
        }

        public async Task AddItemToShoppingList(ShoppingList list, ShoppingListItem item)
        {
            if (item.Name != null)
            {
                int maxId = list.Items.Any() ? list.Items.Max(item => item.Id) + 1 : 1;
                item.Id = maxId;
                list.Items.Add(item);

                await UpdateShoppingList(list);
            }
        }

        public async Task<ShoppingList> DeleteItemFromShoppingList(ShoppingList Model, ShoppingListItem item)
        {
            Model.Items.Remove(item);
            await UpdateShoppingList(Model);

            return Model;
        }
    }
}
