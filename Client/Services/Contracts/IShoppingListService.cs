using Shoppzter.Data;

namespace Shoppzter.Services.Contracts
{
    public interface IShoppingListService
    {
        Task<List<ShoppingList>> GetShoppingLists();
        Task<ShoppingList> GetShoppingList(int id);
        Task AddShoppingList(ShoppingList shoppingList);
        Task DeleteShoppingList(ShoppingList shoppingList);
        Task UpdateShoppingList(ShoppingList shoppingList);
    }
}
