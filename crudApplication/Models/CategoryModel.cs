namespace crudApplication.Models
{
    public class CategoryModel
    {
        public CategoryModel(IReadOnlyCollection<string> categories)
        {
            AllowedCategories = categories;
        }
        public IReadOnlyCollection<string> AllowedCategories { get; }
    }
}
