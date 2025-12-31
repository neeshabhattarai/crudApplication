using crudApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace crudApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]/{action=Index}")]
    public class LoginController : Controller
    {
       
        private readonly  UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
            
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            Console.WriteLine(login.ToString());
            if (!ModelState.IsValid)
            {
                return View(login);
            }
           var Result=await signInManager.PasswordSignInAsync(login.UserName, login.Password, false,false);
            if (Result.Succeeded)
            {
                return RedirectToAction("Index", "Item");
            }
            else
            {
                return View(login);
            }
                
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> EditProfile(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var EditUser = new EditProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            ViewData["Id"] = id;
            return View(EditUser);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfile profile,string id)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Email = profile.Email;
            var Result= await userManager.UpdateAsync(user);
            if (Result.Succeeded)
            {

                return RedirectToAction("Profile", "Login");
            }
            else
            {
                foreach (var errors in Result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
                return View(profile);
            }


        }
        [Authorize]
        public async Task<IActionResult> EditPassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Id"]=id;
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPassword password,string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

          var Result=await userManager.ChangePasswordAsync(user,password.currentPassword,password.newPassword);
            if (Result.Succeeded)
            {
                ViewData["SuccessMessage"] = "Updated Successfully";
                return View();
            }
            else
            {
                ViewData["ErrorMessage"] = "Unable to update password";

                return View();
            }
        }

    }
}
