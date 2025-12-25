namespace crudApplication.Models
{
    public class Category : ICategoryProvider
    {
        public IReadOnlyCollection<string> AllowedCategories()
        {
            var categoriesList = new string[]{ "Apple", "Ball", "Cat" };
            return categoriesList;
        }
    }
}
