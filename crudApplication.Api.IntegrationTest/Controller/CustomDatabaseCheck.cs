using crudApplication.Api.IntegrationTest.FakeDatabase;
using crudApplication.DatabaseRelated;
using System.Net;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Controller
{
    public class CustomDatabaseCheck:IClassFixture<CustomWebFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebFactory<Program> _customWebFactory;
        public CustomDatabaseCheck(CustomWebFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
            _customWebFactory=factory;
            
        }
        [Fact]
        public async Task getAllData()
        {
            var request = await _httpClient.GetAsync("/api/products/1");
            request.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task getData()
        {
            var request = await _httpClient.GetAsync("/api/products/1");
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);

        }
        [Fact]
        public async Task getContent()
        {
            var request = await _httpClient.GetStreamAsync("/api/products/1");
            var data = await JsonSerializer.DeserializeAsync<ProductItem>(request, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal("apple", data.Name);
        }
        [Fact]
        public async Task getProduct()
        {
            var request =await _httpClient.GetFromJsonAsync<ProductItem>("/api/products/1");
            Assert.Equal("apple", request.Description);
        }
        [Fact]
        public async Task GetCount()
        {
            var request = await _httpClient.GetAsync("/api/products/1");
            Assert.Equal("application/json",request?.Content?.Headers?.ContentType?.MediaType);
        }
    }
}
