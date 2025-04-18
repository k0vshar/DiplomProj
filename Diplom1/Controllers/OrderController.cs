using Diplom.Domain.ViewModels.Order;
using Diplom.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StatusCode = Diplom.Domain.Enum.StatusCode;


namespace Diplom.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Create(long goodId)
        {
            var model = new CreateOrderViewModel
            {
                GoodId = goodId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Login = User.Identity.Name;
            var response = await _orderService.Create(model);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return RedirectToAction("Index", "Basket");

            ModelState.AddModelError("", response.Description);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return RedirectToAction("Index", "Basket");

            return View("Error", response.Description);
        }
    }
}
