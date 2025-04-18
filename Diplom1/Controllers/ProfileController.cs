using Diplom.Domain.Entities;
using Diplom.Domain.ViewModels.Profile;
using Diplom.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StatusCode = Diplom.Domain.Enum.StatusCode;

namespace Diplom.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _profileService.GetProfile(User.Identity.Name);
            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            var response = await _profileService.Save(model);

            if (response.StatusCode == Diplom.Domain.Enum.StatusCode.OK)
                return RedirectToAction("Index", "Profile");

            ModelState.AddModelError("", response.Description);
            return View("Index", model);
        }
    }
}
