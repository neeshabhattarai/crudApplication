using crudApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace crudApplication.Service
{
    public class DatabaseIntializer
    {
        public static async Task DataSeed(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            if (user == null || role == null)
            {
                Console.WriteLine("no role and no user");
            }
            var exists = await role.RoleExistsAsync("admin");
            if (!exists) {
                Console.WriteLine("admin role is not assigned");
                await role.CreateAsync(new IdentityRole("admin"));
            }
            exists=await role.RoleExistsAsync("client");
            if (!exists)
            {
                Console.WriteLine("client isnot assigned");
                await role.CreateAsync(new IdentityRole("client"));
            }
            var userRole =await user.GetUsersInRoleAsync("admin");
            if (userRole.Any())
            {
                Console.WriteLine("Admin is already created");
                return;
            }
            var userAdded = new ApplicationUser()
            {
                LastName = "admin",
                FirstName = "admin",
                Email = "admin@12",
            };
            string Password = "admin123";
            var result = await user.CreateAsync(userAdded, Password);
            if (result.Succeeded)
            {
               await user.AddToRoleAsync(userAdded, "admin");
                Console.WriteLine("Admin Created");
            };


            
        }
    }
}
