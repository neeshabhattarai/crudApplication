using Microsoft.AspNetCore.Mvc.Testing;

namespace crudApplication.Api.IntegrationTest
{
    public class HealthChecks:IClassFixture<WebApplicationFactory<Program>>
    {
        public readonly HttpClient _httpClient;
        public HealthChecks(WebApplicationFactory<Program> factory)
        {
           _httpClient=factory.CreateDefaultClient(); 
        }
        [Fact]
        public async Task HealthChecksOk()
        {
            var request = await _httpClient.GetAsync("http://localhost:7149/Admin/Index");
            request.EnsureSuccessStatusCode();
        }
    }
}
