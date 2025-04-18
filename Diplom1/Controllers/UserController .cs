using Diplom.Domain.ViewModels.User;
using Diplom.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StatusCode = Diplom.Domain.Enum.StatusCode;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _userService.GetRoles().Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _userService.GetRoles().Data;
                return View(model);
            }

            var response = await _userService.Create(model);
            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", response.Description);
            ViewBag.Roles = _userService.GetRoles().Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            return BadRequest(response.Description);
        }
    }
}
