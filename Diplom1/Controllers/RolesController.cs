using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult User()
        {
            return View();
        }
    }
}
