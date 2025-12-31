using crudApplication.DatabaseRelated;
using crudApplication.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace crudApplication.Api.IntegrationTest.FakeDatabase
{
    public class CustomWebFactory<Program>:WebApplicationFactory<Program> where Program : class
    {
       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var mockedData = new Mock<ICloud>();
                mockedData.Setup(req => req.GetTask("1")).ReturnsAsync(new ProductItem { Id = "1", Name = "apple", Description = "apple" });
                services.AddSingleton<ICloud>(mockedData.Object);
                services.AddScoped<CloudDatabase>();
                //services.AddDbContext<ApplicationDbContext>(options =>
                //{
                //    options.UseSqlServer(services.Conne)
                //});
            });
        }
    }
}
