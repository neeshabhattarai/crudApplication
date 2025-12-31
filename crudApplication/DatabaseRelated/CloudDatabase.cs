
namespace crudApplication.DatabaseRelated
{
    public class CloudDatabase 
    {
        private readonly ICloud database;
        public CloudDatabase(ICloud database)
        {
            this.database = database;
        }
        public async Task<ProductItem> GetProductName(string id)
        {
            var products =await database.GetTask(id);
            return products;
        }

        public Task<ProductItem> GetTask(string id)
        {
            throw new NotImplementedException();
        }

        public Task InserAsync(string id, ProductItem item)
        {
            throw new NotImplementedException();
        }
    }
}
