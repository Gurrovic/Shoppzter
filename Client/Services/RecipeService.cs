using Client.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Client.Pages;

namespace Client.Services
{
    public class RecipeService
    {       
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public RecipeService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = new HttpClient();            
            _httpClient.BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<RecipeDto> GetRandomRecipeAsync()
        {            
            string endpoint = "random.php";
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var rootObject = JsonConvert.DeserializeObject<Recipe.Rootobject>(content);
                if (rootObject != null && rootObject.meals != null && rootObject.meals.Length > 0)
                {
                    var meal = rootObject.meals[0];
                                        
                    RecipeDto recipeDto = new RecipeDto
                    {
                        MealName = meal.strMeal,
                        Thumbnail = meal.strMealThumb,
                        Ingredients = new List<string>(),
                        Measures = new List<string>(),
                        Instructions = meal.strInstructions
                    };
                    
                    for (int i = 1; i <= 20; i++)
                    {
                        string ingredient = meal.GetType().GetProperty($"strIngredient{i}")?.GetValue(meal, null) as string;
                        string measure = meal.GetType().GetProperty($"strMeasure{i}")?.GetValue(meal, null) as string;
                                                
                        if (!string.IsNullOrWhiteSpace(ingredient) && !string.IsNullOrWhiteSpace(measure))
                        {
                            recipeDto.Ingredients.Add(ingredient);
                            recipeDto.Measures.Add(measure);
                        }
                    }

                    return recipeDto;
                }
            }
            return default!;
        }

        public async Task SaveRecipe(RecipeDto recipe)
        {
            var recipes = await GetRecipes();
            if (recipes.Any(r => r.MealName == recipe.MealName)) return;

            int maxId = recipes.Any() ? recipes.Max(sl => sl.Id) : 0;
            recipe.Id = maxId + 1;
            recipes.Add(recipe);

            await _localStorageService.SetItemAsync("Recipes", recipes);
                       
        }

        public async Task<List<RecipeDto>> GetRecipes()
        {
            var recipes = await _localStorageService.GetItemAsync<List<RecipeDto>>("Recipes");

            if (recipes != null)
            {
                return recipes;
            }
            return new List<RecipeDto>();
        }

        public async Task<RecipeDto> GetRecipe(int id)
        {
            var RecipeDtos = await _localStorageService.GetItemAsync<List<RecipeDto>>("Recipes");

            if (RecipeDtos != null)
            {
                var RecipeDto = RecipeDtos.FirstOrDefault(sl => sl.Id == id);
                return RecipeDto;
            }

            return new RecipeDto();
        }

        public async Task DeleteRecipe(RecipeDto recipe)
        {
            var RecipeDtos = await GetRecipes();
            
            var listIndex = RecipeDtos.FindIndex(sl => sl.Id == recipe.Id);

            if (listIndex != -1)
            {
                RecipeDtos.RemoveAt(listIndex);
                await _localStorageService.SetItemAsync("Recipes", RecipeDtos);                
            }
        }        
    }
}
