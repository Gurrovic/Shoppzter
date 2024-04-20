using Client.Data;

namespace Client.Services.Contracts
{
    public interface IShoppingListService
    {
        Task<List<ShoppingList>> GetShoppingLists();
        Task<ShoppingList> GetShoppingList(int id);
        Task AddShoppingList(ShoppingList shoppingList);
        Task<List<ShoppingList>> DeleteShoppingList(ShoppingList shoppingList);
        Task UpdateShoppingList(ShoppingList shoppingList);

        Task AddItemToShoppingList(ShoppingList list, ShoppingListItem item);
        Task<ShoppingList> DeleteItemFromShoppingList(ShoppingList list, ShoppingListItem item);
    }
}
