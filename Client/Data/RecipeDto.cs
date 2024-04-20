namespace Client.Data
{
    public class RecipeDto    
    {    
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Thumbnail { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Measures { get; set; }
        public string Instructions { get; set; }
        public bool Favorite { get; set; } = false;
    }
}
