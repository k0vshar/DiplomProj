using Diplom.Domain.ViewModels.Authorization;
using Diplom.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using StatusCode = Diplom.Domain.Enum.StatusCode;

namespace Diplom.Controllers
{

    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel model = new();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Валидация не прошла — возвращаемся с ошибками
            }

            var response = await _accountService.Register(model);
            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Index", "Home");
            }

            // Ошибка при регистрации, добавляем сообщение валидации
            ModelState.AddModelError(string.Empty, response.Description);
            return View(model);
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            if (model.Name == "Admin" && model.Password == "adminpass")
            {
                return RedirectToAction("Admin", "Roles");
            }
            else if (!string.IsNullOrWhiteSpace(model.Name) && model.Password == "userpass")
            {
                return RedirectToAction("User", "Roles");
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        }
    }
}
