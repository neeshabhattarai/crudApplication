using crudApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace crudApplication.Service
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Item> ListItem { get; set; }
    }
}
