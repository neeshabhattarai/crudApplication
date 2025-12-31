using crudApplication.Api.IntegrationTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Text.Json;

namespace crudApplication.Api.IntegrationTest.Controller
{
    [ApiController]
    public class ItemChecks : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public ItemChecks(WebApplicationFactory<Program> factory)
        {
            this._httpClient = factory.CreateDefaultClient();
        }
        [Fact]
        public async Task FirstItemTest()
        {
            var request = await _httpClient.GetAsync("/Admin/Item/Test");
            request.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task SecondItemTestByContent()
        {
            var request = await _httpClient.GetAsync("/Admin/Item/Test");
            Assert.Equal("application/json", request.Content.Headers.ContentType.MediaType);
        }
        [Fact]
        public async Task ThirdByLength()
        {
            var request = await _httpClient.GetAsync("/Admin/Item/Test");
            Assert.NotNull(request.Content);
            Assert.True(request.Content.Headers.ContentLength > 0);
        }
        [Fact]
        public async Task ChecksByContent()
        {
            var request = await _httpClient.GetStringAsync("/Admin/Item/Test");
            Assert.Equal("{\"allowedCategories\":[\"Apple\",\"Ball\",\"Cat\"]}", request);
        }
        [Fact]
        public async Task checkByStream()
        {
            var fruitList = new List<string> { "Apple", "Cat", "Ball" };
            var request = await _httpClient.GetStreamAsync("/Admin/Item/Test");
            var model = await JsonSerializer.DeserializeAsync<ItemModel>(request, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(model?.AllowedCategories);
            Assert.Equal(model.AllowedCategories.OrderBy(v => v), fruitList.OrderBy(v => v));
        }
        [Fact]
        public async Task checkTestSystemNetHttp()
        {
            var fruitList = new List<string> {  "Cat", "Ball", "Apple" };

            var request = await _httpClient.GetFromJsonAsync<ItemModel>("/Admin/Item/Test");
            Assert.NotNull(request?.AllowedCategories);
            Assert.Equal(fruitList.OrderBy(v=>v),request.AllowedCategories.OrderBy(v => v));
        }
        [Fact]
        public async Task CacheCheck()
        {
            var response = await _httpClient.GetAsync("/Admin/Item/Test");
            var headers = response.Headers.CacheControl;
            Assert.NotNull(headers?.MaxAge.HasValue);
            Assert.Equal(TimeSpan.FromMinutes(5), headers?.MaxAge.Value);
        }
    }
}
