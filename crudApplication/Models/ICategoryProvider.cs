namespace crudApplication.Models
{
    public interface ICategoryProvider
    {
        public IReadOnlyCollection<string> AllowedCategories();
    }
}
