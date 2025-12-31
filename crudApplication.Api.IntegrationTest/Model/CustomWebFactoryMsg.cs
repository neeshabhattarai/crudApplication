using crudApplication.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace crudApplication.Api.IntegrationTest.Model
{
    public class CustomWebFactoryMsg<Program>:WebApplicationFactory<Program>
        where Program : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(service =>
            {
                service.AddSingleton<IMessageService, MessageService>();
                service.AddDistributedMemoryCache();
                service.AddSession();

            });
            builder.Configure(app =>
            {
                app.UseRouting();
                app.UseSession();
                app.UseMiddleware<MessageMiddleware>();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
               
            });
            }
       
    }
}
