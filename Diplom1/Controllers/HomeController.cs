using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Name == "Admin")
                    return RedirectToAction("Admin", "Roles");
                else
                    return RedirectToAction("User", "Roles");
            }
            return View();
        }
    }
}
