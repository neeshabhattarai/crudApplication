using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace crudApplication.Controllers
{
    [ApiController]
    [Route("/api/register/{action=Index}")]
    public class RegisterController : Controller
    {
      public SignInManager<ApplicationUser> context;
        public UserManager<ApplicationUser> userManager;
        public readonly IConfiguration configuration;
        private SendEmail _email;


        public RegisterController(
     SignInManager<ApplicationUser> context,
     UserManager<ApplicationUser> userManager,
     IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Otp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Otp(VerifyOtp OtpHandle)
        {
            Console.WriteLine(OtpHandle.ToString());
            var sessionOtpStored = HttpContext.Session.GetString("Otp");
            if (sessionOtpStored == null) {
                return View();
            }
            if (OtpHandle.Otp == null)
            {
                return View(OtpHandle);
            }
            var sessionOtp = JsonSerializer.Deserialize<otpGenerate>(sessionOtpStored) ;
            if (sessionOtp.OTP!=OtpHandle.Otp || sessionOtp.Expiry < DateTime.UtcNow)
            {
                return View();
            }
            
            var appUser = new ApplicationUser
            {
                Email = sessionOtp.user.Email,
                UserName = sessionOtp.user.UserName,
                FirstName = sessionOtp.user.FirstName,
                LastName = sessionOtp.user.LastName
            };

            var result = await userManager.CreateAsync(appUser, sessionOtp.user.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(OtpHandle);
            }

            await userManager.AddToRoleAsync(appUser, "client");
            await context.SignInAsync(appUser, false);

            HttpContext.Session.Remove("OTP_DATA");

            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterUser user)
        {
            _email = new SendEmail(configuration);
            Console.WriteLine(user.ToString());
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var otp = new Random().Next(1000, 9999).ToString();
            var sendOtp = new otpGenerate { OTP = otp ,user=user,Expiry=DateTime.UtcNow.AddMinutes(8)};
            HttpContext.Session.SetString("Otp",JsonSerializer.Serialize(sendOtp));

            await _email.SendEmailAsync(sendOtp.OTP);
            return RedirectToAction("Otp");
           
           /* var userAdded = new ApplicationUser()
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
           
            return View(user);*/
        }
    }
}
