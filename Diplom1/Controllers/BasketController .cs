using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Diplom.Service.Interfaces;

namespace Diplom.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;
            if (userName == null)
                return RedirectToAction("Login", "Account");

            var response = await _basketService.GetItems(userName);
            return View(response.Data);
        }

        public async Task<IActionResult> Details(long id)
        {
            var userName = User.Identity?.Name;
            var response = await _basketService.GetItem(userName, id);
            return View(response.Data);
        }
    }
}
