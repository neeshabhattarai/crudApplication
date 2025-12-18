using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    public class AnotherLayout1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
