using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Testcontainers.PostgreSql;

namespace crudApplication.Api.IntegrationTest.FakeDatabase
{
    public class CustomWebFactoryPostgress<Program> : WebApplicationFactory<Program>, IAsyncLifetime
        where Program : class
    {
        private readonly PostgreSqlContainer _postgresContainer =
            new PostgreSqlBuilder()
                .WithImage("postgres:16")                 
                .WithDatabase("mydatabase")
                .WithUsername("postgres")
                .WithPassword("postgres123")
                .Build();

        // Start container before any tests
        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();
        }

        // Dispose container after tests
        public async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                services.RemoveAll(typeof(ApplicationDbContext));
                services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    }).AddEntityFrameworkStores<ApplicationDbContext>();
                services.AddControllersWithViews(opt =>
                {
                    var antiforgeryFilters = opt.Filters
        .Where(f => f is Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute)
        .ToList();

                    foreach (var filter in antiforgeryFilters)
                    {
                        opt.Filters.Remove(filter);
                    }
                });

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(_postgresContainer.GetConnectionString()));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                Console.WriteLine(db.Database.ProviderName); // must be Npgsql
                db.Database.EnsureCreated();
            });
        }
    }
}
