using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class HomeController : Controller
    {
            public IActionResult Index() => View();
    }
}
