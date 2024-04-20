using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Choose a name. Can't create an empty list.")]
        public string? Name { get; set; }

        public List<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();

        public ShoppingList(string name) 
        { 
            Name = name;           
        }

        public ShoppingList()
        {

        }
    }
}
