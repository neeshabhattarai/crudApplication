using crudApplication.DatabaseRelated;
using Moq;
namespace crudApplication.Api.IntegrationTest.FakeDatabase
{
    public class MockDatabase
    {
        public void GetProductByName()
        {
            var mock = new Mock<ICloud>();
            mock.Setup(req => req.GetTask("1")).ReturnsAsync(new ProductItem { Id = "1", Name = "Apple", Description = "apple" });
        }
    }
}
