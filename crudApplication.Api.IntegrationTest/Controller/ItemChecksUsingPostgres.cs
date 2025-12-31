using crudApplication.Api.IntegrationTest.FakeDatabase;
using crudApplication.Api.IntegrationTest.Model;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Controller
{
    public class ItemChecksUsingPostgres : IClassFixture<CustomWebFactoryPostgress<Program>>
    {
        private readonly HttpClient client;
        public ItemChecksUsingPostgres(CustomWebFactoryPostgress<Program> factory)
        {
            
            client = factory.CreateClient();
        }

        [Fact]
        public async Task PostItemChecks()
        {
            var checks = await client.PostAsJsonAsync("Admin/Item/Create", FeedData(), JsonSerializerOptions.Default);
            checks.EnsureSuccessStatusCode();
        }
        public static RegisterItemModel FeedData()
        {
            return new RegisterItemModel
            {
                Name = "Test",
                Description = "This is for testing"
            };
        }

    }
}
