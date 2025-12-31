using crudApplication.Models;

namespace crudApplication.DatabaseRelated
{
    public interface ICloud
    {
        Task<ProductItem> GetTask(string id);
        Task InserAsync(string id,ProductItem item);

        Task<IReadOnlyCollection<ProductItem>> GetAll();

    }
}
