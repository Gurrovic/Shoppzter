using System.ComponentModel.DataAnnotations;

namespace Shoppzter.Data
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [MinLength(1, ErrorMessage = "The name must be at least 1 character long.")]
        public string? Name { get; set; }

        public List<ShoppingListItem>? Items { get; set; }

        public ShoppingList(string name) 
        { 
            Name = name;        
        }

        public ShoppingList()
        {

        }
    }
}
