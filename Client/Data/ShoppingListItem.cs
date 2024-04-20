using System.ComponentModel.DataAnnotations;

namespace Client.Data
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Give your item a name.")]
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}
