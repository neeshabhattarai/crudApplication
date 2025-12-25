using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    public class RegisterController : Controller
    {
      public SignInManager<ApplicationUser> context;
        public UserManager<ApplicationUser> userManager;
      

        public RegisterController(SignInManager<ApplicationUser> context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterUser user)
        {
            Console.WriteLine(user.ToString());
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var userAdded = new ApplicationUser()
            {
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                UserName = user.UserName,

            };
            var userRegister= await userManager.CreateAsync(userAdded,user.Password);
            if (userRegister.Succeeded)
            {
                Console.WriteLine("User Created");
                await userManager.AddToRoleAsync(userAdded, "client");
                await context.SignInAsync(userAdded, false);
            return RedirectToAction("Index", "Home");
            }
            foreach(var errors in userRegister.Errors)
            {
                ModelState.AddModelError("", errors.Description);
            }
            return View(user);
        }
    }
}
